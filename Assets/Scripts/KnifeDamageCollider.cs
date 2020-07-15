using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeDamageCollider : MonoBehaviour, IDamageSource
{
    private float damage;
    public float DamageValue { get { return damage; } }

    void OnTriggerEnter2D(Collider2D col)
    {
        Damageable damageable = col.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.RaiseFlag(DamageValue, Vector2.zero); 
        }
    }

    public void SetDamageValue(float value)
    {
        damage = value;
    }
}
