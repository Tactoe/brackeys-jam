using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
// ReSharper disable All


public class PlayerCharacter : MonoBehaviour
{
    private ActionRecorder rec;
    private Pawn pawn;
    private readonly KeyCode[] keyCodes = new []
    {
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow, 
        KeyCode.Space
    };

    [SerializeField] private GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponent<Pawn>();
        rec = FindObjectOfType<ActionRecorder>();
    }

    // Update is called once per frame
    void Update()
    {
        ActionKeyPressed();
    }

    void ActionKeyPressed()
    {
        foreach (KeyCode keyCode in keyCodes)
        {
            if (Input.GetKeyDown(keyCode)) {
                pawn.DoAction(keyCode);
                rec.AddAction(keyCode);
            }
        }
    }

    private void OnDestroy()
    {
        DeathFunction();
        Invoke("DeathFunction", 2);
    }

    private void DeathFunction()
    {
       GameManager.Instance.ReloadScene();
    }
}
