using UnityEngine;
using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GhostPlatformer : MonoBehaviour
{
    private int i = 0;
    public Vector3[] previousTimeline;
    public float animationDuration;
    [SerializeField] private GameObject bouncePrefab;
    private LineRenderer lr;
    
    private void Start()
    {
        animationDuration = GameManager.Instance.GhostRecordSpeed;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = previousTimeline.Length;
        lr.SetPositions(previousTimeline);
        DoPath();
    }

    void DoPath()
    {
        i++;
        if (i < previousTimeline.Length)
            transform.DOMove(previousTimeline[i], animationDuration).OnComplete(DoPath);
    }

    void DeathFunction()
    {
        Destroy(gameObject);
    }


}