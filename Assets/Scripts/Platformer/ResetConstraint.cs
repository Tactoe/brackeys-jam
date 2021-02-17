using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetConstraint : MonoBehaviour
{
    private bool enteredFromBelow;
    private void OnTriggerEnter(Collider other)
    {
        if (!enteredFromBelow)
            enteredFromBelow = true;
        else
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
