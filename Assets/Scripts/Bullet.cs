using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : DamageSource
{
    [SerializeField] private PlayerAimController aimController;

    void Start()
    {
        destroyOnDone = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (transform.tag != col.transform.tag)
        {
            Damageable damageable = col.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.EnqueueDamageSource(this);
            }
        }

        if (col.tag == "Tilemap")
        {
            Destroy(gameObject);
        }

    }

}
