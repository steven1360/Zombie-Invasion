using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Player Data", menuName ="Player Data")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float health;
    public float Health { get { return health; } }

    public void AddHealth(float amount)
    {
        health += amount;
    }
   
}
