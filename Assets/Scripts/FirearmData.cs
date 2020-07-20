using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Firearm", menuName = "Firearm")]
public class FirearmData : ScriptableObject
{
    [SerializeField] private int currentMagazineCapacity;
    [SerializeField] private int maxMagazineCapacity;
    [SerializeField] private int totalAmmo;
    [SerializeField] private float reloadTimeInSeconds;
    [SerializeField] private float fireRateInSeconds;
    [SerializeField] private float damage;

    public int CurrentMagazineCapacity { get { return currentMagazineCapacity; } }
    public int MaxMagazineCapacity { get { return maxMagazineCapacity; } }
    public int TotalAmmo { get { return totalAmmo; } }
    public float ReloadTimeInSeconds { get { return reloadTimeInSeconds; } }
    public float FireRateInSeconds { get { return fireRateInSeconds; } }
    public float DamageValue { get { return damage; } }

    public bool Reloading { get; private set; }
    public bool AttackInTimeout { get; private set; }
    public Clock ReloadClock { get; private set; }
    public Clock AttackTimeoutClock { get; private set; }

    void Awake()
    {
        Reloading = false;
        AttackInTimeout = false;
        ReloadClock = new Clock(reloadTimeInSeconds, reloadTimeInSeconds);
        AttackTimeoutClock = new Clock(fireRateInSeconds, fireRateInSeconds);
    }

    public void AddToCurrentMagCapacity(int amount)
    {
        currentMagazineCapacity += amount;
    }

    public void AddToTotalAmmo(int amount)
    {
        totalAmmo += amount;
    }

    public void TickClocks(float dt)
    {
        ReloadClock.Tick(dt);
        AttackTimeoutClock.Tick(dt);

        if (ReloadClock.ReachedDesiredWaitTime())
        {
            Reloading = false;
        }
        else
        {
            Reloading = true;
        }

        if (AttackTimeoutClock.ReachedDesiredWaitTime())
        {
            AttackInTimeout = false;
        }
        else
        {
            AttackInTimeout = true;
        }
    }

    public void AddToFireRate(float amount)
    {
        if (fireRateInSeconds > 0.07f)
        {
            fireRateInSeconds += amount;
        }
        else
        {
            fireRateInSeconds = 0.07f;
        }
        AttackTimeoutClock.DesiredWaitTime = fireRateInSeconds;
    }

    public void AddToMaxMagCapacity(int amount)
    {
        maxMagazineCapacity += amount;
    }
}
