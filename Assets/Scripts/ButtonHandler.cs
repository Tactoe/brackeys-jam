using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    private Canvas c;
    private void Start()
    {
        c = transform.parent.GetComponent<Canvas>();
    }

    public void Exit()
    {
        c.sortingOrder = 0;
        GameManager.Instance.ExitGame();
    }

    public void StartGame()
    {
        c.sortingOrder = 0;
        GameManager.Instance.LoadSceneFade("PlatformAdditiveBase");
    }
}
