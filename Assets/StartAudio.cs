using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StartAudio : MonoBehaviour
{
    private AudioSource src;

    [SerializeField] private float fadeTime, startTime;

    [SerializeField] private Ease ease;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        src.time = startTime;
        src.volume = 0;
        src.Play();
        src.DOFade(1, fadeTime).SetEase(ease);
    }

}
