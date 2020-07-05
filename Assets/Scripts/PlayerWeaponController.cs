using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private Knife knife;
    [SerializeField] private Firearm[] firearms;

    private Transform[] weapons;

    public ScriptableObject EquippedWeapon { get; private set; }

    void Start()
    {
        EquippedWeapon = firearms[0];
        weapons = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon("Knife");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon("Rifle");
        }
        UpdateEquippedWeaponInScene();
    }

    void DoAttack()
    {

    }

    void KnifeAttack(Knife knife)
    {

    }

    void FirearmAttack(Firearm firearm)
    {

    }

    void UpdateEquippedWeaponInScene()
    {
        foreach (Transform child in weapons)
        {
            if (child.name == transform.name)
            {
                continue;
            }
            else if (child.name == EquippedWeapon.name)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    void ChangeWeapon(string name)
    {
        EquippedWeapon = GetWeapon(name);
    }

    ScriptableObject GetWeapon(string name)
    {
        ScriptableObject weapon = null;

        if (name == "Knife")
        {
            weapon = knife;
        }

        foreach (Firearm firearm in firearms)
        {
            if (firearm.name == name)
            {
                weapon = firearm;
            }
        }

        return weapon;
    }

}
