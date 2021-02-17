using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceStrength;
    private void OnTriggerEnter(Collider other)
    {
        print("junop");
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.constraints = (RigidbodyConstraints) 122;
        //rb.velocity = Vector3.zero;
        other.GetComponent<GroundedCharacterController>().LaunchCharacter(Vector2.up * bounceStrength);
        //float boost = rb.velocity.y > bounceStrength ? rb.velocity.y + bounceStrength : bounceStrength;
        //rb.AddForce(0, boost, 0, ForceMode.VelocityChange);
        //var tmp = other.GetComponent<Rigidbody>().velocity;
        //tmp.y = bounceStrength;
        //other.GetComponent<Rigidbody>().velocity = tmp;
    }
}
