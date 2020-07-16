using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamageSource
{
    [SerializeField] private PlayerAimController aimController;
    private float damage;
    public float DamageValue { get { return damage; } }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (transform.tag != col.transform.tag)
        {
            Damageable damageable = col.gameObject.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.RaiseFlag(damage, aimController.LookDirection * 0.35f);
            }
            Destroy(gameObject);
        }

    }

    void IDamageSource.SetDamageValue(float value)
    {
        damage = value;
    }
}
