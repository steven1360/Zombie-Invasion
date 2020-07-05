using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmAttack : MonoBehaviour
{
    [SerializeField] private Firearm firearm;
    [SerializeField] private float speed = 3f;
    [SerializeField] private PlayerAimController aimController;
    private Animator anim;
    private Transform bullet;

    void Start()
    {
        anim = GetComponent<Animator>();
        bullet = transform.GetChild(0);
        bullet.gameObject.SetActive(false);
        firearm = Instantiate(firearm);
    }

    void Update()
    {
        DoAttackOnInput(Input.GetKeyDown(KeyCode.Mouse0));
        ReloadWeaponOnInput(Input.GetKeyDown(KeyCode.R));
        firearm.TickClocks(Time.deltaTime);

        Debug.Log($"Ammo: {firearm.CurrentMagazineCapacity}/{firearm.MaxMagazineCapacity}    Total: {firearm.TotalAmmo}");
    }

    void DoAttackOnInput(bool keycodePressedDown)
    {
        bool needToReload = (firearm.CurrentMagazineCapacity == 0);

        if (keycodePressedDown && !needToReload && !firearm.AttackInTimeout && !firearm.Reloading)
        {
            Vector2 bulletTravelDirection = aimController.LookDirection;
            Vector3 aimControllerPosition = aimController.transform.position;
            Transform bulletClone = Instantiate(bullet);
            Rigidbody2D rb = bulletClone.GetComponent<Rigidbody2D>();

            anim.SetTrigger("Shoot");
            firearm.AddToCurrentMagCapacity(-1);
            firearm.AttackTimeoutClock.ResetClock();

            bulletClone.gameObject.SetActive(true);
            bulletClone.GetComponent<IDamageSource>().SetDamageValue(firearm.DamageValue);
            bulletClone.position = aimControllerPosition;
            rb.velocity = bulletTravelDirection * speed;
        }
    }

    void ReloadWeaponOnInput(bool keycodePressedDown)
    {
        if (keycodePressedDown)
        {
            bool magazineIsFull = (firearm.MaxMagazineCapacity - firearm.CurrentMagazineCapacity) == 0;
            bool TotalMagIsEmpty = (firearm.TotalAmmo == 0);

            if (keycodePressedDown && !magazineIsFull && !TotalMagIsEmpty && !firearm.Reloading)
            {
                anim.SetTrigger("Reload");
                int amountToTakeFromTotal = Mathf.Min((firearm.MaxMagazineCapacity - firearm.CurrentMagazineCapacity), firearm.TotalAmmo);
                firearm.AddToCurrentMagCapacity(amountToTakeFromTotal);
                firearm.AddToTotalAmmo(-amountToTakeFromTotal);
                firearm.ReloadClock.ResetClock();
            }
        }
    }
}
