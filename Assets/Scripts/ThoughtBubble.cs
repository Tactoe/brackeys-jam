using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ThoughtBubble : MonoBehaviour
{
    public string sentence;
    public float fadeDuration, lifespan;
    public float yOffset = 5;
    private CanvasGroup cg;
    private RectTransform rt;
    public bool followsHero;
    private Transform heroTransform;
    private TextMeshProUGUI txt;
    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
        rt = GetComponent<RectTransform>();
        txt = GetComponentInChildren<TextMeshProUGUI>();
        txt.text = sentence;
        cg.alpha = 0;
        if (followsHero)
            StartCoroutine(WaitForHero());
        cg.DOFade(1, fadeDuration).OnComplete(() => StartCoroutine(FadeOut()));
    }
    
    private void LateUpdate()
    {
        if (followsHero && heroTransform != null)
        {
            Vector2 tmp =Camera.main.WorldToScreenPoint(heroTransform.position);
            tmp.x -= rt.sizeDelta.x;
            tmp.y -= yOffset;
            rt.anchoredPosition = tmp;
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(lifespan);
        cg.DOFade(0, fadeDuration).SetAutoKill();
    }
    
    IEnumerator WaitForHero()
    {
        while (heroTransform == null)
        {
            heroTransform = FindObjectOfType<GroundedCharacterController>()?.transform;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
