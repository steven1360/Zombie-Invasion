using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatManager : MonoBehaviour, IKillable
{
    [SerializeField] private ZombieStats stats;
    [SerializeField] private Damageable damageable;
    private Rigidbody2D rb;

    public ZombieStats Stats { get { return stats; } }

    void Start()
    {
        stats = Instantiate(stats);
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        DamageSource nextDamageSource = damageable.GetNextDamageSource();
        if (nextDamageSource != null)
        {
            stats.AddHealth(-nextDamageSource.DamageValue);
            transform.parent.position += (Vector3)nextDamageSource.Knockback;
            nextDamageSource.Done();
        }

    }

    public bool IsDead()
    {
        return (stats.Health <= 0) ? true : false;
    }

}
