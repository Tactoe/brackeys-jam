using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("OOF OW OW");
        GameManager.Instance.SetBurn(true);
    }

    private void OnTriggerExit(Collider other)
    {
        print("aaaa");
        GameManager.Instance.SetBurn(false);
    }
}
