using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject pauseMenu;
    public float GhostRecordSpeed = 0.1f, defaultDuration = 5;
    public int fireplaceDialogueIndex = 0, monsterWaveIndex = 0;
    public bool doDialogueOnDeath;

    [SerializeField] private Image fadeImg;
    private bool mustFadeIn;
    private CanvasGroup fadeImgCG;
    
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
        fadeImgCG = fadeImg.gameObject.GetComponent<CanvasGroup>();
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
        if (mustFadeIn)
        {
            FadeIn(defaultDuration, Color.black);
        }
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ExitGame()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            SceneManager.LoadScene("MobileExit");
        }
        else
        {
            Application.Quit();
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void LoadSceneFade(string sceneName)
    {
        fadeImg.color = Color.black;
        fadeImgCG.alpha = 0;
        fadeImgCG.DOFade(1, defaultDuration).OnComplete(() => LoadScene(sceneName));
        mustFadeIn = true;
    }
    
    public void LoadSceneFade(string sceneName, float fadeDuration, Color color)
    {
        fadeImg.color = color;
        fadeImgCG.alpha = 0;
        fadeImgCG.DOFade(1, fadeDuration).OnComplete(() => LoadScene(sceneName));
    }
    
    public void FadeIn(float fadeDuration, Color color)
    {
        fadeImg.color = color;
        fadeImgCG.alpha = 1;
        fadeImgCG.DOFade(0, fadeDuration);
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
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (pauseMenu != null)
            {
                pauseMenu.GetComponent<CanvasGroup>().alpha = 1;
                pauseMenu?.SetActive(!pauseMenu.activeInHierarchy);
                Time.timeScale = pauseMenu.activeInHierarchy ? 0 : 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextScene();
        }
    }

}
