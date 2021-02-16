using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionRecorder : MonoBehaviour
{
    private List<TimeNode> recordingTimeline, previousTimeline;
    
    private float currentTime;
    private float lastTimeSaved;

    private PastSelf pastSelf;
    
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
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (pastSelf != null)
            Destroy(pastSelf);
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

public class TimeNode
{
    internal KeyCode action;
    internal float nextActionTimer;

    public TimeNode(KeyCode _action, float _next)
    {
        action = _action;
        nextActionTimer = _next;
    }
}
