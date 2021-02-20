using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke( nameof(SpawnFire), 10);
    }

    private void OnTriggerExit(Collider other)
    {
        SpawnFire();
    }

    void SpawnFire()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

}
