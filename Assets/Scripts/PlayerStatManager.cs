using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    private Damageable damageable;
    public float Health { get; private set; } = 100f;
    // Start is called before the first frame update
    void Start()
    {
        damageable = transform.GetChild(0).GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damageable.TouchedByDamageSource)
        {
            Health -= damageable.DamageValue;
            damageable.LowerFlag();
        }
       // Debug.Log($"Player Health   {Health}");
    }
}
