using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    public float DamageValue { get; protected set; }
    public Vector2 Knockback { get; protected set; }
    protected bool destroyOnDone = false;


    public void SetDamageValue(float amount)
    {
        DamageValue = amount;
    }

    public void SetKnockback(Vector2 knockback)
    {
        Knockback = knockback;
    }

    public void Done()
    {
        if (destroyOnDone)
        {
            Destroy(gameObject);
        }
    }
}
