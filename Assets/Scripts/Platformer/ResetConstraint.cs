using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetConstraint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
