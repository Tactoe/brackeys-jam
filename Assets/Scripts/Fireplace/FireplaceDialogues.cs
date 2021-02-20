using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class FireplaceDialogues : MonoBehaviour
{
    [SerializeField] private DialogueReader reader;
    
    [SerializeField] private float blackoutDuration = 8, canvasFadeInDuration;

    [SerializeField] private Dialogue[] dialogues;

    
    [SerializeField] private StartAudio src;
    // Start is called before the first frame update
    private void Awake()
    {
        if (GameManager.Instance.fireplaceDialogueIndex == 0)
        {
            FindObjectOfType<StartAudio>().startTime = 0;
        }
    }

    void Start()
    {
        src = FindObjectOfType<StartAudio>();
        //StartCoroutine(LaunchDialogue());
        GameManager.Instance.fadeImgCG.DOFade(0, blackoutDuration).OnComplete(() => StartCoroutine(LaunchDialogue()));
    }


    IEnumerator LaunchDialogue()
    {
        yield return new WaitForSeconds(1);
        reader.StartDialogue(dialogues[GameManager.Instance.fireplaceDialogueIndex]);
        reader.gameObject.GetComponent<CanvasGroup>().DOFade(1, canvasFadeInDuration);
    }

    public void CloseDialogue()
    {
        GameManager.Instance.fireplaceDialogueIndex++;
        src.MusicFadeOut(blackoutDuration - 0.5f);
        if (GameManager.Instance.fireplaceDialogueIndex == 5)
            GameManager.Instance.LoadSceneFade("FinalPlat", blackoutDuration, Color.black);
        else
            GameManager.Instance.LoadSceneFade("Battle", blackoutDuration, Color.black);
    }
}
