using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFOV : MonoBehaviour
{
    public bool PlayerInRange { get; private set; }
    public Vector2 playerLastSeen { get; private set; }
    private bool PlayerInCollider;
    [SerializeField] private Transform player;

    void Start()
    {
        PlayerInCollider = false;
    }

    void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        LayerMask mask = LayerMask.GetMask("Tilemap", "Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, mask);

        if (PlayerInCollider && hit.collider.tag == "Player")
        {
            PlayerInRange = true;
        }
        else if (!PlayerInCollider)
        {
            PlayerInRange = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" )
        {
            PlayerInCollider = true;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerInCollider = false;
            playerLastSeen = col.transform.position;
        }
    }


}
