using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceStrength;
    private void OnTriggerEnter(Collider other)
    {
        print("QHQHHQ");
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.constraints = (RigidbodyConstraints) 122;
        other.GetComponent<Rigidbody>().velocity += Vector3.up * bounceStrength;
        //other.GetComponent<Rigidbody>().AddExplosionForce(10, other.transform.position, 10);
        //other.GetComponent<Rigidbody>().
    }
}
