using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1;

    public string target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3.MoveTowards(transform.position, transform.position + transform.forward * 10, Time.deltaTime * speed);
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(target))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
