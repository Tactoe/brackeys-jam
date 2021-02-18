﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject pauseMenu;
    public float GhostRecordSpeed = 0.1f;
    public int fireplaceDialogueIndex = -1;
    
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

    private void Start()
    {
        pauseMenu.SetActive(false);
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        if (scene.name == "Fireplace")
            fireplaceDialogueIndex++;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ExitGame()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            SceneManager.LoadScene(SceneManager.GetSceneByName("MobileExit").buildIndex);
        }
        else
        {
            Application.Quit();
        }
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        RemoveAllInstances();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        RemoveAllInstances();
        SceneManager.LoadScene(0);
    }

    void RemoveAllInstances()
    {
        if (BattleAudio.Instance)
           Destroy(BattleAudio.Instance.gameObject); 
        if (ActionRecorder.Instance)
           Destroy(ActionRecorder.Instance.gameObject); 
        if (PlatformerRecorder.Instance)
           Destroy(PlatformerRecorder.Instance.gameObject); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu != null)
            {
                pauseMenu?.SetActive(!pauseMenu.activeInHierarchy);
                Time.timeScale = pauseMenu.activeInHierarchy ? 0 : 1;
            }
        }

        /*if (Input.GetKeyDown(KeyCode.N))
        {
            NextScene();
        }*/
    }

}
