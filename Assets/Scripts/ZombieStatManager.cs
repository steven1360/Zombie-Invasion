using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatManager : MonoBehaviour, IKillable
{
    [SerializeField] private ZombieStats stats;
    [SerializeField] private Damageable damageable;
    [SerializeField] private Path path;
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
            Vector2 positionAfterKnockback = (Vector2)transform.parent.position + nextDamageSource.Knockback;
            if (path.grid[positionAfterKnockback].walkable)
            {
                transform.parent.position = positionAfterKnockback;
            }
            nextDamageSource.Done();
        }

    }

    public bool IsDead()
    {
        return (stats.Health <= 0) ? true : false;
    }

}
