using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullets : MonoBehaviour
{ 
    [SerializeField]private GameObject hitEffect;
    private Rigidbody rb;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];
            GameObject newImapactFx = Instantiate(hitEffect, contact.point, Quaternion.LookRotation(contact.normal));
            Destroy(newImapactFx,1.0f);
        }
        Destroy(gameObject);
    }
}
