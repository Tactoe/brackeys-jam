using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalAudio : MonoBehaviour
{
    public static FinalAudio Instance;

    [SerializeField] private AudioSource src, fireSound;
    
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
            
    }

    void Start()
    {
        GameManager.Instance.fadeImgCG.DOFade(0, 4);
        if (GameManager.Instance.diedInFinalPlat >= 2)
            src.Play();
        DontDestroyOnLoad(gameObject);
        //PlayEnding();
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
            StartCoroutine(TextDisplay());
            src.DOFade(1, 10).OnComplete(() =>
            {
                src.DOFade(0, 10);
            });
            GameManager.Instance.LoadSceneFade("Main Menu", 20, Color.black);
        });
    }

    IEnumerator TextDisplay()
    {
        GameManager.Instance.textCG1.DOFade(1, 2.5f);
        yield return new WaitForSeconds(7.5f);
        GameManager.Instance.textCG1.DOFade(0, 2.5f);
        yield return new WaitForSeconds(2.5f);
        GameManager.Instance.textCG2.DOFade(1, 2.5f);
        yield return new WaitForSeconds(7.5f);
        GameManager.Instance.textCG2.DOFade(0, 2.5f);
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
