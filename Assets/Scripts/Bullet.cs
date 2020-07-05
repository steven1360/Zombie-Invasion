using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Bullet hit something");
    }

    public void SetDamage(float amount)
    {
        damage = amount;
    }
}
