using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour, IKillable
{
    private Damageable damageable;
    [SerializeField] private PlayerStats stats;

    public PlayerStats Stats { get { return stats; } }


    // Start is called before the first frame update
    void Awake()
    {
        damageable = transform.GetChild(0).GetComponent<Damageable>();
        stats = Instantiate(stats);
    }

    // Update is called once per frame
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
