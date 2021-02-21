using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject pauseMenu;
    [SerializeField] private GameObject battleTip, platformTip, jumpTip;
    public float GhostRecordSpeed = 0.1f, defaultDuration = 5, burnSpeed;
    public int fireplaceDialogueIndex = 0, monsterWaveIndex = 0, battleTimelinesAllowed = 1;
    public bool doDialogueOnDeath;

    public Image fadeImg;
    public CanvasGroup fadeImgCG, burnFadeCG, textCG1, textCG2;
    private float burnAmount = 0;
    public int diedInFinalPlat = 0;
    private bool isBurning;

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
        if (mode == LoadSceneMode.Additive) return;
        DOTween.KillAll();
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name == "Battle" && fireplaceDialogueIndex == 0 && fadeImgCG != null)
        {
            Instantiate(battleTip, fadeImgCG.transform.parent);
        }
        if (SceneManager.GetActiveScene().name == "PlatformAdditiveBase")
        {
            SceneManager.LoadScene("Scenes/PlatformAdditiveSetup" + (fireplaceDialogueIndex + 1), LoadSceneMode.Additive);
        }
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            fireplaceDialogueIndex = 0;
            monsterWaveIndex = 0;
            diedInFinalPlat = 0;
            battleTimelinesAllowed = 1;
            doDialogueOnDeath = true;
        }
        pauseMenu.SetActive(false);
    }

    public void PlatformTip()
    {
        StartCoroutine(PlatformTipDelayed());
    }

    IEnumerator PlatformTipDelayed()
    {
        yield return new WaitForSeconds(3);
        Instantiate(platformTip, fadeImgCG.transform.parent);
    }
    
    public void JumpTip()
    {
        Instantiate(jumpTip, fadeImgCG.transform.parent);
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
        fireplaceDialogueIndex = 0;
        monsterWaveIndex = 0;
        diedInFinalPlat = 0;
        battleTimelinesAllowed = 1;
        doDialogueOnDeath = true;
        RemoveAllInstances();
        SceneManager.LoadScene(0);
        FadeIn(3, Color.black);
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

    public void SetBurn(bool b)
    {
        isBurning = b;
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

        if (isBurning)
            burnAmount += Time.deltaTime * burnSpeed;
        else if (burnAmount > 0)
            burnAmount -= Time.deltaTime;
        
        if (isBurning && burnAmount > 1)
        {
            isBurning = false;
            if (SceneManager.GetActiveScene().name == "FinalPlat" && fireplaceDialogueIndex == 4 &&
                diedInFinalPlat == 2)
            {
                diedInFinalPlat++; 
                LoadScene("Fireplace");
                fadeImgCG.alpha = 1;
                fadeImg.color = burnFadeCG.GetComponent<Image>().color;
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "FinalPlat")
                    diedInFinalPlat++;
                ReloadScene();
            }
        }
        burnFadeCG.alpha = burnAmount;
    }

}
