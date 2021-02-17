using System;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;

public class PlatformerRecorder : MonoBehaviour
{
    private List<Vector3> recordingTimeline, previousTimeline;
    private Transform heroTransform;
    
    private PastSelf pastSelf;
    private bool isRecording;
    
    public static PlatformerRecorder Instance;

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
        SetupRecordingTimeline();
    }

    void SetupRecordingTimeline()
    {
        heroTransform = FindObjectOfType<AbilityModuleManager>().transform;
        recordingTimeline = new List<Vector3>();
        recordingTimeline.Add(heroTransform.position);
        isRecording = true;
        StartCoroutine(Record());
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isRecording = false;
        StopCoroutine(Record());
        if (pastSelf != null)
            Destroy(pastSelf);
        previousTimeline = recordingTimeline;
        SetupRecordingTimeline();
        if (previousTimeline != null && previousTimeline.Count > 0)
            FindObjectOfType<PlatformerReplayer>().LaunchReplay(previousTimeline);
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator Record()
    {
        while (isRecording)
        {
            yield return new WaitForSeconds(0.1f);
            recordingTimeline.Add(heroTransform.position);
        }
    }
}
