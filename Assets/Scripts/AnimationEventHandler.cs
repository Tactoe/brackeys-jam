using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField] private AudioSource left, right;
    [SerializeField] private AudioClip[] leftClips, rightClips;
    public void CallShoot()
    {
        GetComponentInParent<Character>().Shoot();
    }

    public void WalkLeftSound()
    { 
        left.clip = leftClips[Random.Range(0, leftClips.Length)];
        left.pitch += Random.Range(-0.3f, 0.3f);
        left.Play();
        if (left.pitch < 0.75f || left.pitch > 1.55f)
            left.pitch = 1f;
    }

    public void WalkRightSound()
    {
        right.clip = rightClips[Random.Range(0, rightClips.Length)];
        right.pitch += Random.Range(-0.3f, 0.3f);
        right.Play();
        if (right.pitch < 0.75f || right.pitch > 1.55f)
            right.pitch = 1f;
    }
}
