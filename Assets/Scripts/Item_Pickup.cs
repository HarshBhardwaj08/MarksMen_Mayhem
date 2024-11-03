using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    public static event Action<Weapon> PickUpWeapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerWeaponController playerWeaponController = other.GetComponent<PlayerWeaponController>();
            var weaponSlot = playerWeaponController.weaponSlot;
           
           if (weaponSlot.Count >= 2)
            {
                return;
            }
            if (weaponSlot.Count >= 1 && weaponSlot[0].currentWeaponCategory == weapon.currentWeaponCategory)
            {
                return;
            }
            PickUpWeapon?.Invoke(weapon);
            this.gameObject.SetActive(false);
        }
    }
}
