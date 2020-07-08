﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WandererBehavior_SM { Start, GoToRandomLocation, Wait, Chase}




public class ZombieMovementController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private ZombieStatManager statController;
    private Rigidbody2D rb;
    private Animator anim;
    private ZombieVisionRange range;
    private WandererBehavior_SM wanderer_state;
    private Vector2 randomDestination;
    private Clock clock;



    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
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
        switch (wanderer_state)
        {
            case WandererBehavior_SM.Start:
                anim.SetBool("Moving", true);
                randomDestination = GetRandomPosition();
                LookAt(randomDestination);
                wanderer_state = WandererBehavior_SM.GoToRandomLocation;
                break;
            case WandererBehavior_SM.GoToRandomLocation:
                bool arrivedAtTarget = GoToTarget(randomDestination, 0.1f);
                if (arrivedAtTarget)
                {
                    wanderer_state = WandererBehavior_SM.Wait;
                    anim.SetBool("Moving", false);
                }
                if (range.PlayerInRange)
                {
                    wanderer_state = WandererBehavior_SM.Chase;
                }
                break;
            case WandererBehavior_SM.Wait:
                if (clock.ReachedDesiredWaitTime())
                {
                    wanderer_state = WandererBehavior_SM.GoToRandomLocation;
                    randomDestination = GetRandomPosition();
                    LookAt(randomDestination);
                    clock.ResetClock();
                    anim.SetBool("Moving", true);
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
                if (!range.PlayerInRange) wanderer_state = WandererBehavior_SM.Wait;
                ChasePlayer();

                break;
        }
    }

    private void ChasePlayer()
    {
        float dy = Mathf.Abs(player.position.y - transform.position.y);
        float dx = Mathf.Abs(player.position.x - transform.position.x);
        bool farEnoughAwayFromPlayer = Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2) > 3.9f;
        Vector2 zombieToPlayer = (player.position - transform.position).normalized;

        if (farEnoughAwayFromPlayer && range.PlayerInRange)
        {
            rb.MovePosition(transform.root.position + (Vector3)zombieToPlayer * statController.Stats.Speed * Time.deltaTime);
            LookAt(player.position);
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }

    bool GoToTarget(Vector3 target, float stoppingDistance)
    {
        float dy = Mathf.Abs(target.y - transform.position.y);
        float dx = Mathf.Abs(target.x - transform.position.x);
        Vector2 direction = (target - transform.position).normalized;
        bool notWithinStoppingDistance = Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2) >= Mathf.Pow(stoppingDistance, 2);
        //Debug.Log("distance :" + Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2));
        if (notWithinStoppingDistance)
        {
            rb.MovePosition(transform.root.position + (Vector3)direction * statController.Stats.Speed * Time.deltaTime);
            return false;
        }
        return true;

    }

    Vector2 GetRandomPosition()
    {
        float dx = Random.Range(-4f, 4f);
        float dy = Random.Range(-4f, 4f);
        return new Vector2(transform.position.x + dx, transform.position.y + dy);
    }

}
