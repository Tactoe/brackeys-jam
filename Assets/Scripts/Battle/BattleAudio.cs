using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Rendering;

public class BattleAudio : MonoBehaviour
{
    
    public static BattleAudio Instance;
    [SerializeField] private AudioSource main;
    [SerializeField] private AudioSource f1;
    [SerializeField] private AudioSource f2;
    int currentTracks = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name != "Battle")
            Destroy(gameObject);
    }

    public void FadeOut(float duration)
    {
        if (main.isPlaying)
            main.DOFade(0, duration);
        if (f1.isPlaying)
            f1.DOFade(0, duration);
        if (f2.isPlaying)
            f2.DOFade(0, duration);
    }

    private void Update()
    {
        var time = main.timeSamples;
        f1.timeSamples = time;
        f2.timeSamples = time;
    }

    public void EnableSecondaryTrack()
    {
        currentTracks++;
        if (currentTracks == 1 && !f1.isPlaying)
        {
            f1.Play();
            f1.timeSamples = main.timeSamples;
        }
        if (currentTracks == 2 && !f2.isPlaying && GameManager.Instance.battleTimelinesAllowed == 2)
        {
            f2.Play();
            f2.timeSamples = main.timeSamples;
        }

    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
}
