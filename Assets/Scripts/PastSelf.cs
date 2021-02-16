using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PastSelf : Character
{
    protected new void Start()
    {
        anim = GetComponentInChildren<Animator>();
        pawn = GetComponent<Pawn>();
    }

    protected new void Update()
    {
    }
    
}
