using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{ 
    private Rigidbody rb;
    private void OnCollisionEnter(Collision collision)
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
