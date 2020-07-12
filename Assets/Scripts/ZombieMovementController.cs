using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WandererBehavior_SM { Start, GoToRandomLocation, Wait, Chase}




public class ZombieMovementController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private ZombieStatManager statController;
    [SerializeField] Path path;
    private Rigidbody2D rb;
    private Animator anim;
    private ZombieVisionRange range;
    private WandererBehavior_SM wanderer_state;
    private Vector2 randomDestination;
    private Clock clock;





    // Start is called before the first frame update
    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody2D>();
        anim = transform.root.GetComponent<Animator>();
        range = transform.GetChild(0).GetComponent<ZombieVisionRange>();
        clock = new Clock(3.5f, 0f);
    }



    void FixedUpdate()
    {
        Tick_WandererSM();
    }
    
    void LookAt(Vector2 targetToLookAt)
    {
        float dx = targetToLookAt.x - transform.position.x;
        float dy = targetToLookAt.y - transform.position.y;
        float angle = Mathf.Atan(Mathf.Abs(dy / dx)) * Mathf.Rad2Deg;
        bool mouseInQuadrant2 = dx < 0f && dy > 0f;
        bool mouseInQuadrant3 = dx < 0 && dy < 0;
        bool mouseInQuadrant4 = dx > 0 && dy < 0;

        if (mouseInQuadrant2)
        {
            angle = 90 + (90 - angle);
        }
        else if (mouseInQuadrant3)
        {
            angle += 180;
        }
        else if (mouseInQuadrant4)
        {
            angle = 270 + (90 - angle);
        }

        transform.parent.eulerAngles = new Vector3(0, 0, angle);
    }


    void Tick_WandererSM()
    {
        Debug.Log(wanderer_state);

        //transitions
        switch (wanderer_state)
        {
            case WandererBehavior_SM.Start:

                randomDestination = path.GetRandomWalkablePosition();
                LookAt(randomDestination);
                path.ComputeAStarPath(transform.position, randomDestination);
                wanderer_state = WandererBehavior_SM.GoToRandomLocation;
                break;
            case WandererBehavior_SM.GoToRandomLocation:

                bool arrivedAtTarget = path.MoveRigidbodyAlongPath(rb, statController.Stats.Speed);
                wanderer_state = (arrivedAtTarget) ? WandererBehavior_SM.Wait : wanderer_state;
                wanderer_state = (range.PlayerInRange) ? WandererBehavior_SM.Chase : wanderer_state;
                break;

            case WandererBehavior_SM.Wait:

                if (clock.ReachedDesiredWaitTime())
                {
                    wanderer_state = WandererBehavior_SM.GoToRandomLocation;

                    randomDestination = path.GetRandomWalkablePosition();
                    LookAt(randomDestination);
                    clock.ResetClock();
                    path.ComputeAStarPath(transform.position, randomDestination);
                }
                else
                {
                    clock.Tick(Time.fixedDeltaTime);
                }

                if (range.PlayerInRange)
                {
                    wanderer_state = WandererBehavior_SM.Chase;
                }
                break;
            case WandererBehavior_SM.Chase:
                if (!range.PlayerInRange)
                {
                    wanderer_state = WandererBehavior_SM.Wait;
                }
                else
                {
                    LookAt(player.position);
                    GoToTarget(player.position, 2);
                }

                break;
        }

        //outputs
        switch (wanderer_state)
        {
            case WandererBehavior_SM.Start:

                break;
            case WandererBehavior_SM.GoToRandomLocation:
                anim.SetBool("Moving", true);
                break;
            case WandererBehavior_SM.Wait:
                anim.SetBool("Moving", false);
                break;
            case WandererBehavior_SM.Chase:
                anim.SetBool("Moving", true);
                break;
        }

        void GoToTarget(Vector3 target, float stoppingDistance)
        {
            float dy = Mathf.Abs(target.y - transform.position.y);
            float dx = Mathf.Abs(target.x - transform.position.x);
            bool farFromplayer = Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2) > Mathf.Pow(stoppingDistance, 2);
            Vector2 zombieToTarget = (target - transform.position).normalized;

            if (farFromplayer && range.PlayerInRange)
            {
                LookAt(target);
                rb.MovePosition(transform.root.position + (Vector3)zombieToTarget * statController.Stats.Speed * Time.fixedDeltaTime);
            }


        }
    }

}
