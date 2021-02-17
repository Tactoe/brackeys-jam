using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceStrength;
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.constraints = (RigidbodyConstraints) 122;
        other.GetComponent<Rigidbody>().velocity += Vector3.up * bounceStrength;
    }
}
