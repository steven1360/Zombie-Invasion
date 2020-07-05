using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamageSource
{
    public float damage;
    public float DamageValue { get { return damage; } }

    void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.GetComponent<Damageable>().RaiseFlag(damage, Vector2.zero);
    }

    public void SetDamageValue(float value)
    {
        damage = value;
    }
}
