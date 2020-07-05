using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStatController : MonoBehaviour
{
    [SerializeField] private ZombieStats stats;
    [SerializeField] private Damageable damageable;

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
            Debug.Log("Zombie was hit");
        }
        Debug.Log($"Zombie Health {stats.Health}");
    }

}
