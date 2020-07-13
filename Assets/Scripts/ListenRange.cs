using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenRange : MonoBehaviour
{
    public bool PlayerInRange { get; private set; }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerInRange = true;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerInRange = false;
        }
    }
}
