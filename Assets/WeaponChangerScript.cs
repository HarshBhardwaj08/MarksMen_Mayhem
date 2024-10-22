using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponChangerScript : MonoBehaviour
{
    [SerializeField] List<GameObject> guns;
    [SerializeField] List<Transform> LeftHandIKList; 
    [SerializeField] Transform leftHandIK;
    [SerializeField] Animator animator;
    [SerializeField] float ReloadTime ;
    [SerializeField] float RevertBackTime_AfterChangingWeapon;
    [SerializeField] Rig rig;
    [SerializeField] TwoBoneIKConstraint left_handiKConstraint;
    private bool isSwitchWeaponCompleted = false;
    private bool isReloadingCompleted = false;
    private bool isWeaponGrabOver = false;
    public static event Action<bool> IsReloading_ChangeCompeted;

    private void Awake()
    {
        ActivateGuns(0);
    }
    private void Update()
    {
        ChangeWeaponInput();
        ReBalanceRig();
    }

    private void ChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            IsReloading_ChangeCompeted?.Invoke(false);
            isReloadingCompleted = false;
            rig.weight = 0;
            animator.SetTrigger("Reload");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateGuns(0);
            SwitchAnimationLayer(1);
            SwitchWeapon(SwitchWeapontype.sidegrab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateGuns(1);
            SwitchAnimationLayer(1);
            SwitchWeapon(SwitchWeapontype.sidegrab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateGuns(2);
            SwitchAnimationLayer(1);
            SwitchWeapon(SwitchWeapontype.backgrab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateGuns(3);
            SwitchAnimationLayer(2);
            SwitchWeapon(SwitchWeapontype.backgrab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ActivateGuns(4);
            SwitchAnimationLayer(3);
            SwitchWeapon(SwitchWeapontype.backgrab);
        }
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


    public void ActivateGuns(int val)
    {
       
        if (val < guns.Count)
        {
            guns[val].SetActive(true);
            leftHandIK.position = LeftHandIKList[val].position;
            leftHandIK.rotation = LeftHandIKList[val].rotation;
            DeactivateGuns(val);
        }
    }
    public void DeactivateGuns(int val)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if (guns[i] != null && i != val)
            {
                guns[i].SetActive(false);
            }
        }
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
      animator.SetBool("ChangingWeapon", isWeaponGrabOver);
    }
}
public enum SwitchWeapontype
{
    sidegrab,backgrab
}