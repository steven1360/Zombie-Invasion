using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerWeaponController : MonoBehaviour
{
    private Dictionary<string, Transform> weaponsDict;
    Transform[] weaponsArr;
    private Transform equippedWeapon;
    private Animator anim;
    private KnifeAttack knife;

    void Start()
    {
        weaponsArr = GetComponentsInChildren<Transform>();
        weaponsDict = new Dictionary<string, Transform>();

        foreach (Transform weapon in weaponsArr)
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
            equippedWeapon = weaponsDict["Rifle"];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equippedWeapon = weaponsDict["Shotgun"];
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            knife.Attack();
        }
        UpdateEquippedWeaponInScene();
    }

    void UpdateEquippedWeaponInScene()
    {

        for (int i = 1; i < weaponsArr.Length; i++)
        {
            if (weaponsArr[i].name == equippedWeapon.name)
            {
                weaponsArr[i].gameObject.SetActive(true);
            }
            else
            {
                weaponsArr[i].gameObject.SetActive(false);
            }
        }
    }




}
