using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class LaunchedBullet : Bullet
{
    public float speed = 1;
    public float damage;
    public bool isLaunched;

    public string targetTag;

    public Vector3 targetPos;

    private Tween t;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
        if (isLaunched)
        {
            t = transform.DOJump(new Vector3(targetPos.x, 0.5f, targetPos.z), 1.5f, 1, 2).SetAutoKill().SetEase(Ease.Flash);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3.MoveTowards(transform.position, transform.position + transform.forward * 10, Time.deltaTime * speed);
        if (!isLaunched)
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            if (isLaunched)
            {
                t.Kill();
            }
            other.gameObject.GetComponent<Character>().GetHit(damage);
            Destroy(gameObject);
        }
    }
}
