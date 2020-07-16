using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract string GetInfo();
    public abstract FirearmData GetFirearmData();
}
