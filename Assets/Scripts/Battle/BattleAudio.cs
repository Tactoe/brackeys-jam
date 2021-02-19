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

    public void EnableSecondaryTrack(int amount)
    {
        if (amount == 1 && !f1.isPlaying)
        {
            f1.time = main.time;
            f1.Play();
        }
        if (amount == 2 && !f2.isPlaying)
        {
            f2.time = main.time;
            f2.Play();
        }
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
}
