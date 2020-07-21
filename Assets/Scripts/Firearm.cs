using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firearm : Weapon
{
    [SerializeField] protected FirearmStats firearmStats;
    [SerializeField] protected float speed = 100f;
    [SerializeField] protected PlayerAimController aimController;
    [SerializeField] protected PlayerMovementController movementController;
    [SerializeField] protected AudioManager audioManager;
    [SerializeField] protected string attackAudioClipName;
    [SerializeField] protected string reloadAudioClipName;
    protected Animator anim;
    protected Transform bullet;
    protected Transform muzzle_flash;
    protected bool reloading;
    public bool Reloading { get { return reloading; } }

    void Start()
    {
        anim = GetComponent<Animator>();
        bullet = transform.GetChild(0);
        muzzle_flash = transform.GetChild(1);
        bullet.gameObject.SetActive(false);
        muzzle_flash.gameObject.SetActive(false);
        reloading = false;
    }

    void Awake()
    {
        firearmStats = Instantiate(firearmStats);
    }

    void Update()
    {
        DoAttackOnInput(Input.GetKey(KeyCode.Mouse0));
        ReloadWeaponOnInput(Input.GetKeyDown(KeyCode.R));
        firearmStats.TickClocks(Time.deltaTime);

        if (!reloading && movementController.IsMoving)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }

    }

    public override FirearmStats GetFirearmData()
    {
        return firearmStats;
    }

    virtual protected void DoAttackOnInput(bool keycodePressedDown)
    {
        bool needToReload = (firearmStats.CurrentMagazineCapacity == 0);

        if (keycodePressedDown && !needToReload && !firearmStats.AttackInTimeout && !reloading)
        {
            Vector2 bulletTravelDirection = aimController.LookDirection;
            Vector3 aimControllerPosition = aimController.transform.position;
            Transform bulletClone = Instantiate(bullet);
            Rigidbody2D rb = bulletClone.GetComponent<Rigidbody2D>();
            DamageSource damageSource = bulletClone.GetComponent<DamageSource>();

            anim.SetTrigger("Shoot");
            audioManager.PlayClip(attackAudioClipName);
            muzzle_flash.gameObject.SetActive(true);
            firearmStats.AddToCurrentMagCapacity(-1);
            firearmStats.AttackTimeoutClock.ResetClock();

            bulletClone.gameObject.SetActive(true);
            damageSource.SetDamageValue(firearmStats.DamageValue);
            damageSource.SetKnockback(aimController.LookDirection * 0.65f);
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
        bool magazineIsFull = (firearmStats.MaxMagazineCapacity - firearmStats.CurrentMagazineCapacity) == 0;
        bool TotalMagIsEmpty = (firearmStats.TotalAmmo == 0);

        if (!reloading && keycodePressedDown && !magazineIsFull && !TotalMagIsEmpty)
        {
            anim.SetTrigger("Reload");
            audioManager.PlayClip(reloadAudioClipName);
            StartCoroutine(ReloadWeaponOnInput(firearmStats.ReloadTimeInSeconds));
            reloading = true;
        }

    }

    IEnumerator ReloadWeaponOnInput(float delay)
    {
        yield return new WaitForSeconds(delay);
        int amountToTakeFromTotal = Mathf.Min((firearmStats.MaxMagazineCapacity - firearmStats.CurrentMagazineCapacity), firearmStats.TotalAmmo);
        firearmStats.AddToCurrentMagCapacity(amountToTakeFromTotal);
        firearmStats.AddToTotalAmmo(-amountToTakeFromTotal);
        firearmStats.ReloadClock.ResetClock();
        reloading = false;
    }

    public override string GetInfo()
    {
        return $"{transform.name}: {firearmStats.CurrentMagazineCapacity}/{firearmStats.TotalAmmo}";
    }

}
