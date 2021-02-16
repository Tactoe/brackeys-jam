using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionRecorder : MonoBehaviour
{
    private List<TimeNode> recordingTimeline, previousTimeline;
    [SerializeField] private GameObject dopelGO;
    
    private float currentTime;
    private float lastTimeSaved;
    
    
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
        print("Restarted, i should be 0");
        StopCoroutine(Replay());
        previousTimeline = recordingTimeline;
        recordingTimeline = new List<TimeNode>();
        if (previousTimeline != null && previousTimeline.Count > 0)
            StartCoroutine(Replay());
    }
    
    public void AddAction(KeyCode action)
    {
        recordingTimeline.Add(new TimeNode(action, Time.time - lastTimeSaved));
        lastTimeSaved = Time.time;
    }

    IEnumerator Replay()
    {
        int i = 0;
        GameObject dopel = Instantiate(dopelGO);
        Character dopelPawn = dopel.GetComponent<PastSelf>();
        while (i < previousTimeline.Count && previousTimeline != null && dopelPawn != null)
        {
            print("looping is " + i);
            yield return new WaitForSeconds(previousTimeline[i].nextActionTimer);
            if (dopelPawn != null)
            {
                print("stuff");
                dopelPawn.TryAction(previousTimeline[i].action);
            }
            i++;
        }
        Destroy(dopel);
    }
}

class TimeNode
{
    internal KeyCode action;
    internal float nextActionTimer;

    public TimeNode(KeyCode _action, float _next)
    {
        action = _action;
        nextActionTimer = _next;
    }
}
