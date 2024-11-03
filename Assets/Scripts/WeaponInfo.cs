using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public Weapontype WeaponType;
    public SwitchWeapontype SwitchWeapontype;
    public HoldType HoldType;
    public Transform GunPoint;
    public Transform LeftHandIk;
    public GameObject Laser;
}

public enum SwitchWeapontype
{
    sidegrab, backgrab
}
public enum HoldType
{
    commonHold = 1,LowHold,HighHold
}