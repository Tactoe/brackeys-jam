using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplayer : MonoBehaviour
{
    private List<TimeNode> previousTimeline;
    [SerializeField] private GameObject dopelGO;
    
    private float currentTime;
    private float lastTimeSaved;

    private PastSelf pastSelf;
    private int i = 0;

    public void LaunchReplay(List<TimeNode> _previousTimeline)
    {
        previousTimeline = _previousTimeline;
        StartCoroutine(Replay());
    }

    IEnumerator Replay()
    {
        pastSelf = Instantiate(dopelGO).GetComponent<PastSelf>();
        while (i < previousTimeline.Count && previousTimeline != null && pastSelf != null)
        {
            yield return new WaitForSeconds(previousTimeline[i].nextActionTimer);
            if (pastSelf != null)
            {
                pastSelf.TryAction(previousTimeline[i].action);
            }
            i++;
        }
        yield return new WaitForSeconds(1f);

        if (pastSelf != null)
            pastSelf.DeathFunction();
    }
}

