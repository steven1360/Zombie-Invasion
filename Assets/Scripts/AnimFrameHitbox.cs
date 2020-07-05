using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFrameHitbox : MonoBehaviour, IDamageSource
{
    private float damage;
    public float DamageValue { get { return damage; } }

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<Damageable>().RaiseFlag(DamageValue, Vector2.zero);
    }

    public void SetDamageValue(float value)
    {
        damage = value;
    }
}
