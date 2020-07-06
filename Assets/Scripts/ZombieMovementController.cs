using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ZombieMovementController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private ZombieStatManager statController;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float dy = Mathf.Abs(player.position.y - transform.position.y);
        float dx = Mathf.Abs(player.position.x - transform.position.x);
        bool farEnoughAwayFromPlayer = Mathf.Pow(dx, 2) + Mathf.Pow(dy, 2) > 3.9f;


        Vector2 zombieToPlayer = (player.position - transform.position).normalized;
        LookAt(player.position);

        if (farEnoughAwayFromPlayer)
        {
            rb.MovePosition(transform.root.position + (Vector3)zombieToPlayer * statController.Stats.Speed * Time.deltaTime);
        }

        
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

}
