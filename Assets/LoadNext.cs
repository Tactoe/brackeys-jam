using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNext : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       GameManager.Instance.NextScene(); 
    }
}
