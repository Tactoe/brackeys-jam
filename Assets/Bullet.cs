using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1;
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
}
