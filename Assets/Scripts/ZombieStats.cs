using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Zombie Stat", menuName ="Zombie Stat")]
public class ZombieStats : ScriptableObject
{
    [SerializeField] private float health;
    [SerializeField] private float speed;

    public float Health { get { return health; } }
    public float Speed { get { return speed; } }

    public void AddHealth(float amount)
    {
        health += amount;
    }

}
