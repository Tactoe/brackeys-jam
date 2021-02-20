using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FallCharacter : MonoBehaviour
{
    private bool touchedGround;
    public float fallHeight;
    public float fallHeightReminder = 72.5f;

    [SerializeField] private GameObject hero, impactParticle;

    void Start()
    {
        GameManager.Instance.fadeImgCG.DOFade(0, 8);
        var tmp = transform.position;
        tmp.y = fallHeight;
        transform.position = tmp; 
        Camera.main.transform.DORotate(Vector3.right * 8.5f, 15);
    }

    void Update()
    {
        if (touchedGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hero.SetActive(true);
                Camera.main.GetComponent<BasicCameraTracker>().m_Target = hero;
                Camera.main.GetComponent<BasicCameraTracker>().m_UseFixedUpdate = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!touchedGround)
        {
            touchedGround = true;
            var tmp = Instantiate(impactParticle, transform.position, Quaternion.identity);
            tmp.GetComponent<ParticleSystem>().Play();
            GameManager.Instance.PlatformTip();
        }
    }
}
