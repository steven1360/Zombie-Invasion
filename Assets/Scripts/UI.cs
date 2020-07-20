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
    [SerializeField] private Text killsDisplay;
    [SerializeField] private Text timeDisplay;

    private TimeDisplay time;

    void Start()
    {
        time = new TimeDisplay();
    }

    void Update()
    {
        time.Tick(Time.deltaTime);

        ammoDisplay.text = weaponController.EquippedWeapon.GetComponent<Weapon>().GetInfo();
        healthDisplay.text = $"Health: {statManager.Stats.Health}";
        supplyItemDisplay.text = $"{itemCollector.CollectedItemInfo}";
        killsDisplay.text = $"Kills: {statManager.Stats.Kills}";
        timeDisplay.text = time.FormatTime();
    }
}
