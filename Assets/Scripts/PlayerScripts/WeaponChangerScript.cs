using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponChangerScript : MonoBehaviour
{
    [SerializeField] Transform leftHandIK;
    [SerializeField] Animator animator;
    [SerializeField] float ReloadTime ;
    [SerializeField] float RevertBackTime_AfterChangingWeapon;
    [SerializeField] Rig rig;
    [SerializeField] TwoBoneIKConstraint left_handiKConstraint;
    [SerializeField] private WeaponInfo[] weaponInfos;
    [SerializeField] private Back_Weapons[] back_weapons;
    private bool isSwitchWeaponCompleted = false;
    private bool isReloadingCompleted = false;
    private bool isWeaponGrabOver = false;
    public static event Action<bool> IsReloading_ChangeCompeted;

    private WeaponInfo weaponInfo;
    private void Start()
    {
        var inputActions = PLayer.playerControls.Character;
        inputActions.Reload.performed += ctx => ReloadWeapon();
        weaponInfos = GetComponentsInChildren<WeaponInfo>(true);
        back_weapons = GetComponentsInChildren<Back_Weapons>(true);
    }
    private void OnEnable()
    {
        Item_Pickup.PickUpWeapon += EnableWeapon;
        PlayerWeaponController.CurrentWeaponEnable += EnableWeapon;
        PlayerWeaponController.SecondaryWeapon += EnableSecondryWeapon;
        PlayerWeaponController.OnReload += ReloadWeapon;
    }
    private void OnDisable()
    {
        PlayerWeaponController.CurrentWeaponEnable -= EnableWeapon;
        PlayerWeaponController.SecondaryWeapon -= EnableSecondryWeapon;
        Item_Pickup.PickUpWeapon -= EnableWeapon;
        PlayerWeaponController.OnReload -= ReloadWeapon;
    }
    private void EnableWeapon(Weapon weapon)
    {
       for (int i = 0; i < weaponInfos.Length; i++)
        {
            if (weaponInfos[i].WeaponType == weapon.currentWeaponType)
            {   
                weapon.gunPoint = weaponInfos[i].GunPoint;
                weaponInfos[i].gameObject.SetActive(true);
                SetWeaponPlacement(weaponInfos[i].LeftHandIk);
                SwitchAnimationLayer(((int)weaponInfos[i].HoldType));
                SwitchWeapon(weaponInfos[i].SwitchWeapontype);
                weaponInfos[i].Laser.SetActive(true);
                weaponInfo = weaponInfos[i];
            }
            else
            {
                weaponInfos[i].gameObject.SetActive(false);
                weaponInfos[i].Laser.SetActive(false);
            }
        }
    }
    private void EnableSecondryWeapon(Weapontype weapontype)
    {
        for(int i = 0;i < back_weapons.Length; i++)
        {
            if (back_weapons[i].weapontype == weapontype)
            {
                back_weapons[i].gameObject.SetActive(true);
            }
            else
            {
                back_weapons[i].gameObject.SetActive(false);
            }
        }
    }
    private void SetWeaponPlacement(Transform weaponTransform)
    {
        leftHandIK.position =  weaponTransform.position;
        leftHandIK.rotation =  weaponTransform.rotation;
    }
 
    private void ReloadWeapon()
    { 
        weaponInfo?.Laser.SetActive(true);
        IsReloading_ChangeCompeted?.Invoke(false);
        isReloadingCompleted = false;
        rig.weight = 0;
        animator.SetTrigger("Reload");
    }

    private void Update()
    {
        ReBalanceRig();
    }

    private void ReBalanceRig()
    { 
        if (isReloadingCompleted == true)
        {
            rig.weight += ReloadTime * Time.deltaTime;
        }
        if (isSwitchWeaponCompleted == true)
        {
            left_handiKConstraint.weight += RevertBackTime_AfterChangingWeapon * Time.deltaTime;
        }
    }
    public void SwitchWeapon(SwitchWeapontype weapontype)
    {
        IsReloading_ChangeCompeted?.Invoke(false);
        isReloadingCompleted = false;
        rig.weight = 0;
        isSwitchWeaponCompleted = false;
        left_handiKConstraint.weight = 0;
        isWeaponGrabOver = true;
        animator.SetBool("ChangingWeapon", isWeaponGrabOver);
        animator.SetFloat("CurrentWeapon", ((float)weapontype));
        animator.SetTrigger("SwitchWeapon");
    }

    public void SwitchAnimationLayer(int layerIndex)
    {
        for(int i = 1; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }
        animator.SetLayerWeight(layerIndex, 1);
    }
    public void ResetRig()
    {
        IsReloading_ChangeCompeted?.Invoke(true);
        isReloadingCompleted = true;
        weaponInfo?.Laser.SetActive(true);
    }
    public void ResetLeftHandIKRig()
    {
        IsReloading_ChangeCompeted?.Invoke(true);
        isReloadingCompleted = true;
        isSwitchWeaponCompleted = true;
    }
    public void WeponGrabOver()
    {
      isWeaponGrabOver = false;
      weaponInfo?.Laser.SetActive(true);
      animator.SetBool("ChangingWeapon", isWeaponGrabOver);
    }
}
