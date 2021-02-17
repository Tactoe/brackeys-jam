﻿using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionRecorder : MonoBehaviour
{
    private List<TimeNode> recordingTimeline, previousTimeline;
    
    private float lastTimeSaved;
    private bool hasStarted;

    public static ActionRecorder Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        recordingTimeline = new List<TimeNode>();
        lastTimeSaved = Time.time;
        hasStarted = true;
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!hasStarted) return;
        
        
        previousTimeline = recordingTimeline;
        lastTimeSaved = Time.time;
        recordingTimeline = new List<TimeNode>();
        if (previousTimeline != null && previousTimeline.Count > 0)
            FindObjectOfType<ActionReplayer>().LaunchReplay(previousTimeline);
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void AddAction(KeyCode action)
    {
        recordingTimeline.Add(new TimeNode(action, Time.time - lastTimeSaved));
        lastTimeSaved = Time.time;
    }
}
