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
        print("take 1");
        print(src);
        if (src.playOnAwake)
        {
            PlayWithFade();
        }
    }

    public void PlayWithFade()
    {
        print("take 2");
        print(src);
        src.time = startTime;
        src.volume = 0;
        src.Play();
        src.DOFade(1, fadeTime).SetEase(ease);
    }

}
