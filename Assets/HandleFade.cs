using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class HandleFade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.fadeImgCG.DOFade(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
