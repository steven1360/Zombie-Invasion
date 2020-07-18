using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBox : MonoBehaviour
{
    public Item SupplyBoxItem { get; private set; }
    [SerializeField] private PlayerStatManager statManager;
    [SerializeField] private PlayerWeaponController weaponController;
    [SerializeField] private AudioManager aud;


    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        if (statManager != null && weaponController != null)
        {
            SupplyBoxItem = CreateRandomItem();
        }
    }


    Item CreateRandomItem()
    {
        int n = Random.Range(0, 6);
        switch(n)
        {
            case 0: return new HealthPack(statManager.Stats);
            case 1: return new PistolAmmo(weaponController.GetWeapon("Pistol").GetFirearmData());
            case 2: return new RifleAmmo(weaponController.GetWeapon("Rifle").GetFirearmData());
            case 3: return new ShotgunAmmo(weaponController.GetWeapon("Shotgun").GetFirearmData());
            case 4: return new FasterFireRate(GetRandomWeapon());
            case 5: return new MoreMagCapacity(GetRandomWeapon());
        }
        return null;
    }

    private Weapon GetRandomWeapon()
    {
        int n = Random.Range(0, 3);
        switch (n)
        {
            case 0: return weaponController.GetWeapon("Pistol");
            case 1: return weaponController.GetWeapon("Rifle");
            case 2: return weaponController.GetWeapon("Shotgun");
        }
        return null;
    }

    public abstract class Item 
    {
        public abstract string UseItem();
        public void PlaySoundEffect(AudioManager audManager) { audManager.PlayClip("supplybox_open"); }
    }

    public class HealthPack : Item
    {
        private PlayerStats stats;
        public HealthPack(PlayerStats stats) { this.stats = stats; }
        public override string UseItem()
        {
            if (stats != null)
            {
                Random.InitState(System.DateTime.Now.Millisecond);
                float healAmount = Random.Range(10, 30);
                stats.AddHealth(healAmount);
                return $"+{healAmount} health";
            }
            return "";
        }
    }

    public class PistolAmmo : Item
    {
        private FirearmData stats;
        public PistolAmmo(FirearmData stats) { this.stats = stats; }
        public override string UseItem()
        {
            if (stats != null)
            {
                Random.InitState(System.DateTime.Now.Millisecond);
                int ammoReceived = Random.Range(4, 15);
                stats.AddToTotalAmmo(ammoReceived);
                return $"+{ammoReceived} pistol ammo";
            }
            return "";
        }
    }

    public class RifleAmmo : Item
    {
        private FirearmData stats;
        public RifleAmmo(FirearmData stats) { this.stats = stats; }
        public override string UseItem()
        {
            if (stats != null)
            {
                Random.InitState(System.DateTime.Now.Millisecond);
                int ammoReceived = Random.Range(20, 40);
                stats.AddToTotalAmmo(ammoReceived);
                return $"+{ammoReceived} rifle ammo";
            }
            return "";
        }
    }

    public class ShotgunAmmo : Item
    {
        private FirearmData stats;
        public ShotgunAmmo(FirearmData stats) { this.stats = stats; }
        public override string UseItem()
        {
            if (stats != null)
            {
                Random.InitState(System.DateTime.Now.Millisecond);
                int ammoReceived = Random.Range(10, 20);
                stats.AddToTotalAmmo(ammoReceived);
                return $"+{ammoReceived} shotgun ammo";
            }
            return "";
        }
    }

    public class FasterFireRate : Item
    {
        private Weapon weapon;
        public FasterFireRate(Weapon weapon) { this.weapon = weapon; }
        public override string UseItem()
        {
            if (weapon != null)
            {
                weapon.GetFirearmData().AddToFireRate(-0.02f);
                return $"Upgraded {weapon.name} Fire Rate";
            }
            return "";
        }
    }

    public class MoreMagCapacity : Item
    {
        private Weapon weapon;
        public MoreMagCapacity(Weapon weapon) { this.weapon = weapon; }
        public override string UseItem()
        {
            if (weapon != null)
            {
                Random.InitState(System.DateTime.Now.Millisecond);
                int bonusCapacity = Random.Range(3, 7);
                weapon.GetFirearmData().AddToMaxMagCapacity(bonusCapacity);
                return $"{weapon.name} Magazine Capacity increased by {bonusCapacity}";
            }
            return "";
        }
    }

}
