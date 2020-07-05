using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Knife", menuName = "Knife")]
public class Knife : ScriptableObject
{
    [SerializeField]
    private float damageValue;

    public float DamageValue { get { return damageValue;  } }

    public override string ToString()
    {
        return "Knife";
    }
}
