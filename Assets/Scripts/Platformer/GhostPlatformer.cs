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
    public bool canDoReplay;
    [SerializeField] private GameObject bouncePrefab;
    private LineRenderer lr;
    private MeshRenderer mr;
    
    private void Start()
    {
        //lr = GetComponent<LineRenderer>();
        //lr.positionCount = previousTimeline.Length;
        //lr.SetPositions(previousTimeline);
        
        mr = GetComponent<MeshRenderer>();
        if (canDoReplay)
        {
            mr.enabled = true;
            animationDuration = GameManager.Instance.GhostRecordSpeed;
            DoPath();
        }
    }

    void DoPath()
    {
        i++;
        if (i < previousTimeline.Length)
            transform.DOMove(previousTimeline[i], animationDuration / 1.1f).OnComplete(DoPath);
        else
            DeathFunction();
    }

    void DeathFunction()
    {
        Destroy(gameObject);
    }

}