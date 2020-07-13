using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerWeaponController : MonoBehaviour
{
    private Dictionary<string, Transform> weaponsDict;
    private List<Transform> weaponsList;
    private Transform equippedWeapon;

    [SerializeField] private KnifeAttack knife;
    [SerializeField] private FirearmAttack rifle;

    void Start()
    {
        Transform[] weapons = GetComponentsInChildren<Transform>();
        weaponsList = new List<Transform>();
        weaponsDict = new Dictionary<string, Transform>();

        for (int i = 1; i < weapons.Length; i++)
        {
            if (weapons[i].parent == transform)
            {
                weaponsList.Add(weapons[i]);
            }
        }

        foreach (Transform weapon in weaponsList)
        {
            weaponsDict.Add(weapon.name, weapon);
        }
        equippedWeapon = weaponsDict["Knife"];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equippedWeapon = weaponsDict["Knife"];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equippedWeapon = weaponsDict["Pistol"];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equippedWeapon = weaponsDict["Rifle"];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            equippedWeapon = weaponsDict["Shotgun"];
        }


        UpdateEquippedWeaponInScene();
    }

    void UpdateEquippedWeaponInScene()
    {
        foreach (Transform weapon in weaponsList)
        {
            if (weapon == equippedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }


}
