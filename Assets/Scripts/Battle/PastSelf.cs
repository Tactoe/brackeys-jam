using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PastSelf : Character
{
    private int i = 0;
    protected new void Start()
    {
        anim = GetComponentInChildren<Animator>();
        pawn = GetComponent<Pawn>();
        pawn.pos = FindObjectOfType<PlayerCharacter>().pawn.pos;
        pawn.UpdatePos();
    }

    public void LaunchReplay(List<TimeNode> previousTimeline)
    {
        StartCoroutine(Replay(previousTimeline));
    }

    IEnumerator Replay(List<TimeNode> previousTimeline)
    {
        while (i < previousTimeline.Count)
        {
            yield return new WaitForSeconds(previousTimeline[i].nextActionTimer);
                TryAction(previousTimeline[i].action);
            i++;
        }
        yield return new WaitForSeconds(1f);
        DeathFunction();
    }

    protected new void Update()
    {
    }
    
}
