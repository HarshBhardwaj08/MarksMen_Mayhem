using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullets : MonoBehaviour
{ 
    [SerializeField]private GameObject hitEffect;
    private Rigidbody rb;
    private void Start()
    {
        ObjectPool.instance.CreatePool(hitEffect.name,hitEffect);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];
            GameObject newImapactFx = ObjectPool.instance.GetObjectFromPool(hitEffect.name);
            newImapactFx.transform.position = contact.point;
            newImapactFx.transform.rotation = Quaternion.LookRotation(contact.normal);
            //GameObject newImapactFx = Instantiate(hitEffect, contact.point, Quaternion.LookRotation(contact.normal));
           ObjectPool.instance.ReturnObjectToPool(hitEffect.name, newImapactFx, 1.0f);
        }
      ObjectPool.instance.ReturnObjectToPool(name, this.gameObject);
    }
}
