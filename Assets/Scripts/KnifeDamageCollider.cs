using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDamageCollider : DamageSource
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Damageable damageable = col.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.EnqueueDamageSource(this);
        }
    }

}
