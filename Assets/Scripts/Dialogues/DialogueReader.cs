using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueReader : MonoBehaviour
{
    Dialogue currentDialogue;
    DialogueNode currentNode;
    [SerializeField]
    Image leftCharacter, rightCharacter, bgImg;
    [SerializeField]
    TextMeshProUGUI nameText, dialogueText;
    [SerializeField]
    CanvasGroup cg;
    int dialogueIndex;
    public bool inDialogue, isTyping;
    // Start is called before the first frame update
    void Start()
    {
        cg.alpha = 0;
    }

    void Update()
    {
        if (inDialogue && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0)))
        {
            if (isTyping)
            {
                dialogueText.maxVisibleCharacters = dialogueText.text.Length;
            }
            else if (dialogueIndex < currentDialogue.dialogue.Count)
            {
                ReadNewNode(currentDialogue.dialogue[dialogueIndex]);
            }
            else
            {
                CloseDialogue();
            }
        }
    }

    public void StartDialogue(Dialogue newDial)
    {
        inDialogue = true;
        currentDialogue = newDial;
        ReadNewNode(currentDialogue.dialogue[dialogueIndex]);
        //cg.alpha = 1;
    }

    IEnumerator ReadText()
    {
        isTyping = true;
        while (dialogueText.maxVisibleCharacters < dialogueText.text.Length)
        {
            if (dialogueText.text[dialogueText.maxVisibleCharacters] == '.')
                yield return new WaitForSeconds(0.12f);
            else if (dialogueText.text[dialogueText.maxVisibleCharacters] == ',')
                yield return new WaitForSeconds(0.08f);
            else
                yield return new WaitForSeconds(0.02f);
            dialogueText.maxVisibleCharacters++;
        }

        isTyping = false;
    }
    
    void ReadNewNode(DialogueNode node)
    {
        if (node.text != "")
        {
            dialogueText.transform.parent.gameObject.SetActive(true);
            dialogueText.text = node.text;
            dialogueText.maxVisibleCharacters = 0;
            StartCoroutine(ReadText());
        }
        else
            dialogueText.transform.parent.gameObject.SetActive(false);
        if (node.speakerName != "")
        {
            Color color;
            nameText.transform.parent.gameObject.SetActive(true);
            nameText.text = node.speakerName;
            if (node.speakerName == "Id")
                nameText.transform.parent.GetComponent<Image>().color = new Color(0.38f, 0.09f, 0.55f);
            if (node.speakerName == "Id (past)")
                nameText.transform.parent.GetComponent<Image>().color = new Color(0.1f, 0.8f, 0.75f);
        }
        else
            nameText.transform.parent.gameObject.SetActive(false);
        HandleImg(node.img, bgImg);
        HandleImg(node.leftSpeaker, leftCharacter);
        HandleImg(node.rightSpeaker, rightCharacter);
        dialogueIndex++;
    }

    void HandleImg(Sprite img, Image target)
    {
        if (target == null) return;
        if (img != null)
        {
            target.sprite = img;
            if (img.name == "Id hurt")
                target.rectTransform.sizeDelta = new Vector2(637, 849);
            else if (img.name == "Id down")
                target.rectTransform.sizeDelta =new Vector2(394, 820);
            else
                target.rectTransform.sizeDelta = new Vector2(499, 937);
            target.gameObject.SetActive(true);
        }
        else
            target.gameObject.SetActive(false);
    }

    void CloseDialogue()
    {
        inDialogue = false;
        FindObjectOfType<FireplaceDialogues>().CloseDialogue();
    }
}
