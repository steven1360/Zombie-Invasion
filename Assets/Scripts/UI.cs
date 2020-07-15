using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private PlayerStatManager playerStats;
    [SerializeField] private PlayerWeaponController weaponController;

    [SerializeField] private Text ammoDisplay;
    [SerializeField] private Text healthDisplay;

    void Update()
    {
        ammoDisplay.text = weaponController.EquippedWeapon.GetComponent<Weapon>().GetInfo();
        healthDisplay.text = $"Health: {playerStats.Health}";
    }
}
