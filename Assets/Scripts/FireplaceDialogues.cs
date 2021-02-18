using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class FireplaceDialogues : MonoBehaviour
{
    [SerializeField] private DialogueReader reader;
    
    private Image img;
    [SerializeField] private float blackoutDuration = 8, canvasFadeInDuration;

    [SerializeField] private Dialogue[] dialogues;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = Color.black;
        img.DOFade(0, blackoutDuration).OnComplete(() => StartCoroutine(LaunchDialogue()));
    }


    IEnumerator LaunchDialogue()
    {
        yield return new WaitForSeconds(1);
        reader.StartDialogue(dialogues[GameManager.Instance.fireplaceDialogueIndex]);
        reader.gameObject.GetComponent<CanvasGroup>().DOFade(1, canvasFadeInDuration);
    }

    public void CloseDialogue()
    {
        img.DOFade(1, blackoutDuration).OnComplete(GameManager.Instance.NextScene);
    }
}
