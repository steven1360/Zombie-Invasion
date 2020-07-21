using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerWeaponController : MonoBehaviour
{
    private Dictionary<string, Transform> weaponsDict;
    private List<Transform> weaponsList;
    public Transform EquippedWeapon { get; private set; }

    void Awake()
    {
        Transform[] weapons = GetComponentsInChildren<Transform>();
        weaponsList = new List<Transform>();
        weaponsDict = new Dictionary<string, Transform>();


        for (int i = 1; i < weapons.Length; i++)
        {
            // GetComponentsInChildren includes child objects of all children
            // e.g., includes Pistol's child object called bullet
            // Don't want to include child objects of weapons, so
            // checking if the child object's parent is the weapon controller
            // is necessary, as done below
            if (weapons[i].parent == transform)
            {
                weaponsList.Add(weapons[i]);
            }
        }

        foreach (Transform weapon in weaponsList)
        {
            weaponsDict.Add(weapon.name, weapon);
        }
        EquippedWeapon = weaponsDict["Knife"];
    }

    void Update()
    {
        Firearm firearm = EquippedWeapon.GetComponent<Firearm>();
        if (firearm != null && firearm.Reloading)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquippedWeapon = weaponsDict["Knife"];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquippedWeapon = weaponsDict["Pistol"];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquippedWeapon = weaponsDict["Rifle"];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquippedWeapon = weaponsDict["Shotgun"];
        }


        UpdateEquippedWeaponInScene();
    }

    void UpdateEquippedWeaponInScene()
    {
        foreach (Transform weapon in weaponsList)
        {
            if (weapon == EquippedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }

    public Weapon GetWeapon(string name)
    {
        return weaponsDict[name].GetComponent<Weapon>();
    }


}
