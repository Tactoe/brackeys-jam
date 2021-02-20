using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplayer : MonoBehaviour
{
    [SerializeField] private GameObject dopelGO;
    
    public void LaunchReplay(List<TimeNode> _previousTimeline)
    {
        var tmp = Instantiate(dopelGO); 
        tmp.GetComponent<PastSelf>().LaunchReplay(_previousTimeline);
    }

}

