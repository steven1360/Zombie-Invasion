using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFOV : MonoBehaviour
{
    public bool PlayerInRange { get; private set; }
    public Vector2 playerLastSeen { get; private set; }
    [SerializeField] private Transform player;


    void OnTriggerStay2D(Collider2D col)
    {
        Vector2 direction = (player.position - transform.position).normalized;
        LayerMask mask = LayerMask.GetMask("Tilemap", "Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, mask);

        if (col.tag == "Player" && hit.collider.tag == "Player")
        {
            PlayerInRange = true;
        }
        else 
        {
            PlayerInRange = false;
            playerLastSeen = player.position;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerLastSeen = col.transform.position;
            PlayerInRange = false;
        }
    }


}
