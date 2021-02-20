using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class FinalAudio : MonoBehaviour
{
    public static FinalAudio Instance;

    private AudioSource src;
    [SerializeField] private Material skybox;

    [SerializeField] private AudioClip endingSong;

    private static readonly int Property = Shader.PropertyToID("_AtmosphereThickness");

    // Start is called before the first frame update
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
        if (GameManager.Instance.diedInFinalPlat <= 3)
            Destroy(gameObject);
            
    }

    void Start()
    {
        src = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlayEnding()
    {
        float skyStart = RenderSettings.skybox.GetFloat(Property); 
        DOTween.To(x => skyStart = x, 0.66f, 0, 6).OnUpdate(() =>
        {
            RenderSettings.skybox.SetFloat(Property, skyStart);
        });
        
        src.DOFade(0, 5).OnComplete(() =>
        {
            RenderSettings.skybox.SetFloat(Property, 0);
            src.Stop();
            src.clip = endingSong;
            src.time = 0;
            src.Play();
            src.DOFade(1, 20);
            GameManager.Instance.LoadSceneFade("Main Menu", 20, Color.black);
        });
    }
    
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name != "FinalPlat")
            Destroy(gameObject);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
