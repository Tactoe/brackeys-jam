using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceStrength, shakeStrength, shakeDuration;
    public int shakeVibrato;
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.constraints = (RigidbodyConstraints) 122;
        //rb.velocity = Vector3.zero;
        Camera.main.GetComponent<BasicCameraTracker>().DoShake(shakeDuration, shakeStrength, shakeVibrato);
        other.GetComponent<GroundedCharacterController>().LaunchCharacter(Vector2.up * bounceStrength);
        //float boost = rb.velocity.y > bounceStrength ? rb.velocity.y + bounceStrength : bounceStrength;
        //rb.AddForce(0, boost, 0, ForceMode.VelocityChange);
        //var tmp = other.GetComponent<Rigidbody>().velocity;
        //tmp.y = bounceStrength;
        //other.GetComponent<Rigidbody>().velocity = tmp;
    }
}
