using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1;
    public float damage;
    public bool isLaunched;

    public string targetTag;

    public Vector3 targetPos;
    [SerializeField] private GameObject deathDrop;

    private Tween t;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
        if (isLaunched)
        {
            t = transform.DOJump(new Vector3(targetPos.x, 0.5f, targetPos.z), 1.5f, 1, 2);
            t.OnComplete(Land);
            t.SetEase(Ease.Flash);
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

    void Land()
    {
        if (deathDrop != null)
            Instantiate(deathDrop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isLaunched && other.gameObject.CompareTag(targetTag))
        {
            other.gameObject.GetComponent<Character>().GetHit(damage);
            Destroy(gameObject);
        }
    }
}
