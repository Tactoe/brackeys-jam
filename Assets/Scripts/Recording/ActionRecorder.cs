using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionRecorder : MonoBehaviour
{
    private List<TimeNode> recordingTimeline;
    private List<List<TimeNode>> allTimelines;
    
    private float lastTimeSaved;
    public bool recordingForFirstTime;

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
        allTimelines = new List<List<TimeNode>>();
        recordingTimeline = new List<TimeNode>();
        lastTimeSaved = Time.time;
        recordingForFirstTime = true;
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name != "Battle") Destroy(gameObject);
        if (!recordingForFirstTime) return;

        allTimelines.Add(recordingTimeline);
        if (allTimelines.Count > GameManager.Instance.battleTimelinesAllowed)
            allTimelines.RemoveAt(0);
        lastTimeSaved = Time.time;
        recordingTimeline = new List<TimeNode>();
        foreach (var timeline in allTimelines)
        {
            
            if (timeline != null && timeline.Count > 0)
            {
                //if (SceneManager.GetActiveScene().name != "Battle") Destroy(gameObject);
                FindObjectOfType<ActionReplayer>()?.LaunchReplay(timeline);
            }
        }
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

