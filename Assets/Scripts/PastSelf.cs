using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PastSelf : Character
{
    protected readonly KeyCode[] keyCodes = new []
    {
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow, 
        KeyCode.Space
    };

    protected new void Start()
    {
        anim = GetComponentInChildren<Animator>();
        pawn = GetComponent<Pawn>();
    }

    protected new void Update()
    {
    }
    
}
