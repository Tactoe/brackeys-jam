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

    [SerializeField] private GameObject thoughtBubble;
    // Start is called before the first frame update
    void Start()
    {
        var tmp = transform.position;
        tmp.y = fallHeight;
        transform.position = tmp; 
        Camera.main.transform.DORotate(Vector3.right * 8.5f, 15);
    }

    // Update is called once per frame
    void Update()
    {
        if (touchedGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hero.SetActive(true);
                Camera.main.GetComponent<BasicCameraTracker>().m_Target = hero;
                Camera.main.GetComponent<BasicCameraTracker>().m_UseFixedUpdate = false;
                StopCoroutine(GetupBubble());
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
            StartCoroutine(GetupBubble());
        }
    }

    IEnumerator GetupBubble()
    {
        yield return new WaitForSeconds(7);
        Instantiate(thoughtBubble, FindObjectOfType<Canvas>().transform);
    }
}
