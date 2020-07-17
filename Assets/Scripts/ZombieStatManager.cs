using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatManager : MonoBehaviour, IKillable
{
    [SerializeField] private ZombieStats stats;
    [SerializeField] private Damageable damageable;

    public ZombieStats Stats { get { return stats; } }

    void Start()
    {
        stats = Instantiate(stats);
    }

    void Update()
    {
        if (damageable.TouchedByDamageSource)
        {
            stats.AddHealth(-damageable.DamageValue);
            damageable.LowerFlag();
        }

    }

    public bool IsDead()
    {
        return (stats.Health <= 0) ? true : false;
    }

}
