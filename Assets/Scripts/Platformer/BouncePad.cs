using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] private AudioClip[] accords;
    private AudioSource src;
    public float bounceStrength, shakeStrength, shakeDuration, bounceBoost = 8;
    public int shakeVibrato;

    [SerializeField] private int height;
    [SerializeField] private List<float> tiers;
    [SerializeField] private float startmaxHeight = 10;

    private void Start()
    {
        if (height == 0)
        {
            for (int i = 0; i < accords.Length; i++)
            {
                if (transform.position.y >= tiers[i])
                    height = i;
            }
        }
        src = GetComponent<AudioSource>();
        if (height == accords.Length - 1)
            bounceStrength *= 2;
        bounceStrength += height;
        height = Mathf.Clamp(height, 0, accords.Length);
        src.clip = accords[Mathf.FloorToInt(height)];
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.constraints = (RigidbodyConstraints) 122;
        //rb.velocity = Vector3.zero;
        Camera.main.GetComponent<BasicCameraTracker>().DoShake(shakeDuration, shakeStrength, shakeVibrato);
        other.GetComponent<GroundedCharacterController>().LaunchCharacter(Vector2.up * bounceStrength);
        StartCoroutine(Bye());
        //float boost = rb.velocity.y > bounceStrength ? rb.velocity.y + bounceStrength : bounceStrength;
        if (height == 7)
        {
            other.GetComponent<GroundedCharacterController>().enabled = false;
            other.GetComponent<PlayerInput>().enabled = false;
            other.GetComponent<CapsuleMovingColliderSolver>().enabled = false;
            other.GetComponent<ControlledCapsuleCollider>().enabled = false;
            other.GetComponent<CapsuleVolumeIntegrity>().enabled = false;
            rb.AddForce(0, 100, 0, ForceMode.VelocityChange);
            FindObjectOfType<FinalAudio>().PlayEnding();
        }
        //var tmp = other.GetComponent<Rigidbody>().velocity;
        //tmp.y = bounceStrength;
        //other.GetComponent<Rigidbody>().velocity = tmp;
    }

    IEnumerator Bye()
    {
        src.Play();
        yield return new WaitForSeconds(Mathf.Min(src.clip.length, 2.1f));
        Destroy(gameObject);
    }
}
