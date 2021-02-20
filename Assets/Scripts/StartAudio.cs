using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StartAudio : MonoBehaviour
{
    private AudioSource src;

    public float fadeTime, startTime;

    [SerializeField] private Ease ease;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        if (src.playOnAwake)
        {
            PlayWithFade();
        }
    }

    public void PlayWithFade()
    {
        src.time = startTime;
        src.volume = 0;
        src.Play();
        src.DOFade(1, fadeTime).SetEase(ease);
    }

    public void MusicFadeOut(float duration)
    {
        src.DOFade(0, duration).SetEase(ease);
    }

}
