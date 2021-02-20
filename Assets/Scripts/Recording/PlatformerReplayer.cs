using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerReplayer : MonoBehaviour
{
    [SerializeField] private GameObject dopelGO, bouncePrefab;

    public void LaunchReplay(List<List<Vector3>> _previousTimelines, List<float> bounceTimeline, List<Vector3> bouncePositions)
    {
        for (int i = 0; i < _previousTimelines.Count; i++)
        {
            GameObject tmp = Instantiate(dopelGO);
            if (bounceTimeline[i] > 0)
                StartCoroutine(SpawnBounce(bounceTimeline[i] / 1.7f, bouncePositions[i]));
            GhostPlatformer g = tmp.GetComponent<GhostPlatformer>();
            g.previousTimeline = _previousTimelines[i].ToArray();
            g.canDoReplay = true;//(i + 1 == _previousTimelines.Count);
        }
    }
    
    IEnumerator SpawnBounce(float bounceTimer, Vector3 bouncePos)
    {
        yield return new WaitForSeconds(bounceTimer);
        Instantiate(bouncePrefab, bouncePos, Quaternion.identity);
    }

}

