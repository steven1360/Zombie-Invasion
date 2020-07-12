﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmAttack : MonoBehaviour
{
    [SerializeField] private Firearm firearm;
    [SerializeField] private float speed = 3f;
    [SerializeField] private PlayerAimController aimController;
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private AudioManager aud;
    private Animator anim;
    private Transform bullet;
    private bool reloading;
    private Transform muzzle_flash;

    void Start()
    {
        anim = GetComponent<Animator>();
        bullet = transform.GetChild(0);
        muzzle_flash = transform.GetChild(1);
        bullet.gameObject.SetActive(false);
        muzzle_flash.gameObject.SetActive(false);
        firearm = Instantiate(firearm);
        reloading = false;
    }

    void Update()
    {
        DoAttackOnInput(Input.GetKey(KeyCode.Mouse0));
        ReloadWeaponOnInput(Input.GetKeyDown(KeyCode.R));
        firearm.TickClocks(Time.deltaTime);

        if (!reloading && movementController.IsMoving)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }

        Debug.Log($"Ammo: {firearm.CurrentMagazineCapacity}/{firearm.MaxMagazineCapacity}    Total: {firearm.TotalAmmo}");
    }

    void DoAttackOnInput(bool keycodePressedDown)
    {
        bool needToReload = (firearm.CurrentMagazineCapacity == 0);

        if (keycodePressedDown && !needToReload && !firearm.AttackInTimeout && !reloading)
        {
            Vector2 bulletTravelDirection = aimController.LookDirection;
            Vector3 aimControllerPosition = aimController.transform.position;
            Transform bulletClone = Instantiate(bullet);
            Rigidbody2D rb = bulletClone.GetComponent<Rigidbody2D>();

            anim.SetTrigger("Shoot");
            aud.PlayClip("m4a1_singleshot");
            muzzle_flash.gameObject.SetActive(true);
            firearm.AddToCurrentMagCapacity(-1);
            firearm.AttackTimeoutClock.ResetClock();

            bulletClone.gameObject.SetActive(true);
            bulletClone.GetComponent<IDamageSource>().SetDamageValue(firearm.DamageValue);
            bulletClone.position = aimControllerPosition;
            rb.velocity = bulletTravelDirection * speed;
        }
        else
        {
            muzzle_flash.gameObject.SetActive(false);
        }
    }

    void ReloadWeaponOnInput(bool keycodePressedDown)
    {
        bool magazineIsFull = (firearm.MaxMagazineCapacity - firearm.CurrentMagazineCapacity) == 0;
        bool TotalMagIsEmpty = (firearm.TotalAmmo == 0);

        if (!reloading && keycodePressedDown && !magazineIsFull && !TotalMagIsEmpty)
        {
            anim.SetTrigger("Reload");
            aud.PlayClip("m4a1_reload");
            StartCoroutine(ReloadWeaponOnInput(firearm.ReloadTimeInSeconds));
            reloading = true;
        }

    }

    IEnumerator ReloadWeaponOnInput(float delay)
    {
        yield return new WaitForSeconds(delay);

                int amountToTakeFromTotal = Mathf.Min((firearm.MaxMagazineCapacity - firearm.CurrentMagazineCapacity), firearm.TotalAmmo);
                firearm.AddToCurrentMagCapacity(amountToTakeFromTotal);
                firearm.AddToTotalAmmo(-amountToTakeFromTotal);
                firearm.ReloadClock.ResetClock();


        reloading = false;
    }
}
