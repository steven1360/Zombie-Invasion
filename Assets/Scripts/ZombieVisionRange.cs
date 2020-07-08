using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVisionRange : MonoBehaviour
{
    public bool PlayerInRange { get; private set; }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            PlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerInRange = false;
        }
    }
}
