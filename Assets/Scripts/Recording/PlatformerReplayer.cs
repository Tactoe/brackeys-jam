using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerReplayer : MonoBehaviour
{
    private List<Vector3> previousTimeline;
    [SerializeField] private GameObject dopelGO;
    
    private Transform pastSelf;
    private int i = 0;

    public void LaunchReplay(List<Vector3> _previousTimeline)
    {
        previousTimeline = _previousTimeline;
        StartCoroutine(Replay());
    }

    IEnumerator Replay()
    {
        pastSelf = Instantiate(dopelGO).transform;
        while (i < previousTimeline.Count && previousTimeline != null && pastSelf != null)
        {
            yield return new WaitForSeconds(0.1f);
            if (pastSelf != null)
            {
                pastSelf.position = previousTimeline[i];
            }
            i++;
        }
        yield return new WaitForSeconds(1f);

        if (pastSelf != null)
        {
           Destroy(pastSelf.gameObject); 
        }
    }
}

