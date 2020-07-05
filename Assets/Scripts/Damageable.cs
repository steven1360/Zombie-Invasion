using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Damageable : MonoBehaviour
{
    public bool TouchedByDamageSource { get; private set; }
    public float DamageValue { get; private set; }

    void Start()
    {
        TouchedByDamageSource = false;
        DamageValue = 0;
    }

    public void RaiseFlag(float damageAmount, Vector2 knockbackForce)
    {
        TouchedByDamageSource = true;
        DamageValue = damageAmount;
        //KnockbackEffect(knockbackForce);
    }

    public void LowerFlag()
    {
        TouchedByDamageSource = false;
        DamageValue = 0;
    }

    void KnockbackEffect(Vector2 knockbackForce)
    {
        Debug.Log($"{transform.parent.name} was knocked back with a force of {knockbackForce.magnitude}");
        Rigidbody2D rb = transform.parent.GetComponent<Rigidbody2D>();
        rb.velocity = knockbackForce;
    }
}
