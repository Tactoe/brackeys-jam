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
            StartCoroutine(SpawnBounce(bounceTimeline[i], bouncePositions[i]));
            GhostPlatformer g = tmp.GetComponent<GhostPlatformer>();
            g.previousTimeline = _previousTimelines[i].ToArray();
        }
    }
    
    IEnumerator SpawnBounce(float bounceTimer, Vector3 bouncePos)
    {
        yield return new WaitForSeconds(bounceTimer);
        print("spawned bounce at:" + Time.timeSinceLevelLoad);
        var tmp = Instantiate(bouncePrefab);
        tmp.transform.position = bouncePos;
    }

}

