using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFrameHitbox : MonoBehaviour
{
    public bool Hit { get; private set; }

    void Start()
    {
        Hit = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Hit = true;
    }

    public void AcknowledgeHit()
    {
        Hit = false;
    }
}
