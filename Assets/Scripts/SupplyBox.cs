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
        SupplyBoxItem = CreateRandomItem();
    }


    Item CreateRandomItem()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int n = Random.Range(0, 4);
        switch(n)
        {
            case 0: return new HealthPack(statManager.Stats);
            case 1: return new PistolAmmo(weaponController.GetWeapon("Pistol").GetFirearmData());
            case 2: return new RifleAmmo(weaponController.GetWeapon("Rifle").GetFirearmData());
            case 3: return new ShotgunAmmo(weaponController.GetWeapon("Shotgun").GetFirearmData());
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
            Random.InitState(System.DateTime.Now.Millisecond);
            float healAmount = Random.Range(10, 30);
            stats.AddHealth(healAmount);
            return $"Recovered {healAmount} health";
        }
    }

    public class PistolAmmo : Item
    {
        private FirearmData stats;
        public PistolAmmo(FirearmData stats) { this.stats = stats; }
        public override string UseItem()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            int ammoReceived = Random.Range(4, 15);
            stats.AddToTotalAmmo(ammoReceived);
            return $"Found {ammoReceived} pistol ammunition in the supply box";
        }
    }

    public class RifleAmmo : Item
    {
        private FirearmData stats;
        public RifleAmmo(FirearmData stats) { this.stats = stats; }
        public override string UseItem()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            int ammoReceived = Random.Range(20, 40);
            stats.AddToTotalAmmo(ammoReceived);
            return $"Found {ammoReceived} rifle ammunition in the supply box";
        }
    }

    public class ShotgunAmmo : Item
    {
        private FirearmData stats;
        public ShotgunAmmo(FirearmData stats) { this.stats = stats; }
        public override string UseItem()
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            int ammoReceived = Random.Range(10, 20);
            stats.AddToTotalAmmo(ammoReceived);
            return $"Found {ammoReceived} shotgun shells in the supply box";
        }
    }

}
