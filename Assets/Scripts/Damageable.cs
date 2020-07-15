using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Damageable : MonoBehaviour
{
    public bool TouchedByDamageSource { get; private set; }
    public float DamageValue { get; private set; }

    public delegate void DSDel();
    public event DSDel OnDamageSourceTouched;

    void Start()
    {
        TouchedByDamageSource = false;
        DamageValue = 0;
    }


    public void RaiseFlag(float damageAmount, Vector2 knockbackForce)
    {
        TouchedByDamageSource = true;

        if (OnDamageSourceTouched != null)
        {
            OnDamageSourceTouched.Invoke();
        }
        DamageValue = damageAmount;
        KnockbackEffect(knockbackForce);
    }

    public void LowerFlag()
    {
        TouchedByDamageSource = false;
        DamageValue = 0;
    }

    void KnockbackEffect(Vector2 knockbackForce)
    {
        Debug.Log($"{transform.parent.name} was knocked back with a force of {knockbackForce.magnitude}");
        transform.root.position += (Vector3)knockbackForce;
    }
}