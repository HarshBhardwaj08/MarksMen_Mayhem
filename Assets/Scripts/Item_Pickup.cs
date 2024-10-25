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
            PickUpWeapon?.Invoke(weapon);
            this.gameObject.SetActive(false);
        }
    }
}
