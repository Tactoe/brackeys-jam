using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.SetBurn(true);
    }

    private void OnTriggerStay(Collider other)
    {
        GameManager.Instance.SetBurn(true);
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.Instance.SetBurn(false);
    }
}
