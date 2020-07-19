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
        DamageSource nextDamageSource = damageable.GetNextDamageSource();
        if (nextDamageSource != null)
        {
            stats.AddHealth(-nextDamageSource.DamageValue);
            nextDamageSource.Done();
        }

    }

    public bool IsDead()
    {
        return (stats.Health <= 0) ? true : false;
    }

}
