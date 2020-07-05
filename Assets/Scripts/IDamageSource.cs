using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageSource 
{
    float DamageValue { get; }
    void SetDamageValue(float value);
}
