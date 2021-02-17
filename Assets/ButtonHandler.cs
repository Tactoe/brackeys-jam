using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void Exit()
    {
        GameManager.Instance.ExitGame();
    }

    public void StartGame()
    {
        GameManager.Instance.NextScene();
    }
}
