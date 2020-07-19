using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Damageable : MonoBehaviour
{
    private Queue<DamageSource> damageSources;

    public delegate void DSDel();
    public event DSDel OnDamageSourceTouched;

    void Start()
    {
        damageSources = new Queue<DamageSource>();
    }

    public void EnqueueDamageSource(DamageSource source)
    {
        damageSources.Enqueue(source);
        if (OnDamageSourceTouched != null)
        {
            OnDamageSourceTouched.Invoke();
        }
    }


    public DamageSource GetNextDamageSource()
    {
        if (damageSources.Count == 0)
        {
            return null;
        }
        else
        {
            DamageSource nextSrc = damageSources.Peek();
            damageSources.Dequeue();
            return nextSrc;
        }
    }

}