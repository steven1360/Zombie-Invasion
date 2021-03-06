﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Player Data", menuName ="Player Data")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float health;
    [SerializeField] private int kills;

    public float Health { get { return health; } }
    public float Kills { get { return kills; } }

    public void AddHealth(float amount)
    {
        if (health + amount > 100)
        {
            health = 100;
        }
        else if (health + amount < 0)
        {
            health = 0;
        }
        else
        {
            health += amount;
        }
    }

    public void AddKills(int amount)
    {
        kills += amount;
    }
}
