using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private PlayerStatManager statManager;
    [SerializeField] private PlayerWeaponController weaponController;
    [SerializeField] private ItemCollector itemCollector;

    [SerializeField] private Text ammoDisplay;
    [SerializeField] private Text healthDisplay;
    [SerializeField] private Text supplyItemDisplay;

    void Update()
    {
        ammoDisplay.text = weaponController.EquippedWeapon.GetComponent<Weapon>().GetInfo();
        healthDisplay.text = $"Health: {statManager.Stats.Health}";
        supplyItemDisplay.text = $"{itemCollector.CollectedItemInfo}";
    }
}
