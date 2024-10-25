using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private Weapon currentWeapon = null;
   
    private PLayer pLayer;
    private Animator animator;
    private const  float fixedBulletSpeed = 20.0f;
    private WeaponChangerScript weaponChangerScript;
    [Space]
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Transform aim;
    private bool isReloading_ChangeCompete = true;

    public List<Weapon> weaponSlot;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        pLayer = GetComponent<PLayer>();
        AssignInputs();
    }
    private void OnEnable()
    {
        WeaponChangerScript.IsReloading_ChangeCompeted += IsReloading_ChangeCompeted;
        Item_Pickup.PickUpWeapon += PickupWeapon;
    }
    private void OnDisable()
    {
        WeaponChangerScript.IsReloading_ChangeCompeted -= IsReloading_ChangeCompeted;
    }
    private void PickupWeapon(Weapon weapon)
    { 
        if(weaponSlot.Count > 2)
        {
            return;
        }
       weaponSlot.Add(weapon);
       currentWeapon = weapon;
    }
    private void IsReloading_ChangeCompeted(bool obj)
    {
        isReloading_ChangeCompete = obj;
        ReloadWeapon();
    }
    private void EquipWeapon(int num)
    {
        currentWeapon = weaponSlot[num];
    }
    private void DropWeapon()
    { 
        if(weaponSlot.Count <= 1)
        {
            return;
        }
        weaponSlot.Remove(currentWeapon);
        currentWeapon = weaponSlot[0];
    }
    private void shoot(string anim)
    { 
       if( currentWeapon.WeaponCurrentAmmo <= 0 || isReloading_ChangeCompete == false)
        {
            return;
        }
        currentWeapon.WeaponCurrentAmmo--;
        GameObject newBullets = Instantiate(Bullet,gunPoint.position,Quaternion.LookRotation(gunPoint.forward));
        Rigidbody rb = newBullets.GetComponent<Rigidbody>();
        rb.mass =  fixedBulletSpeed / bulletSpeed;
        rb.velocity = BulletDirection()*bulletSpeed;
        animator.SetTrigger(anim);
    }
    public Vector3 BulletDirection()
    {
        Vector3 dir =(aim.position - gunPoint.position).normalized;
        if(pLayer.playerAim.getPreciseAim() == false) {
        dir.y = 0;
        }
        if (isReloading_ChangeCompete == true)
        {
            weaponHolder.LookAt(aim);
            gunPoint.LookAt(aim);
        }
     
        return dir;
    }
    public Transform GunPoint () => gunPoint;
    private void AssignInputs()
    {
        var inputActions = pLayer.playerControls.Character;
        inputActions.Fire.performed += ctx => shoot("Fire");
        inputActions.SwitchWeapon1.performed += ctx => EquipWeapon(0);
        inputActions.SwitchWeapon2.performed += ctx => EquipWeapon(1);
        inputActions.DropWeapon.performed += ctx => DropWeapon();
       
    }

    private void ReloadWeapon()
    {
        currentWeapon.WeaponCurrentAmmo = currentWeapon.WeaponMagzineSize;
    }
}

