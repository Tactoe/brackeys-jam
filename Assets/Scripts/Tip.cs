using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Tip : MonoBehaviour
{
    private CanvasGroup img;
    private void Start()
    {
        img = GetComponent<CanvasGroup>();
        img.alpha = 0;
        img.DOFade(1, 3).SetEase(Ease.InQuad).OnComplete(() => StartCoroutine(FadeOut()));
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(5f);
        img.DOFade(0, 3).OnComplete(() => Destroy(gameObject));
    }
    
    
    
}