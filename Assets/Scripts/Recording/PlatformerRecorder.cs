using System;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlatformerRecorder : MonoBehaviour
{
    [SerializeField] private int maxTimelines = 5, maxActions = 1000;
    private List<List<Vector3>> allTimelines;
    [SerializeField] private List<float> bounceTimeline;
    [SerializeField] private List<Vector3> bouncePositions;
    private List<Vector3> recordingTimeline;
    private Transform heroTransform;

    private bool isRecording, hasStarted, hasRecordedJump;
    
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
        bounceTimeline = new List<float>();
        bouncePositions = new List<Vector3>();
        allTimelines = new List<List<Vector3>>();
        SetupRecordingTimeline();
        hasStarted = true;
    }

    void SetupRecordingTimeline()
    {
        heroTransform = FindObjectOfType<AbilityModuleManager>().transform;
        bounceTimeline.Add(-1);
        bouncePositions.Add(Vector3.zero);
        recordingTimeline = new List<Vector3>();
        recordingTimeline.Add(heroTransform.position);
        isRecording = true;
        hasRecordedJump = false;
        StartCoroutine(Record());
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!hasStarted) return;
        print(DOTween.KillAll());
        isRecording = false;
        StopAllCoroutines();
        if (recordingTimeline != null && recordingTimeline.Count > 0)
            allTimelines.Add(new List<Vector3>(recordingTimeline));
        SetupRecordingTimeline();
        if (allTimelines.Count > maxTimelines)
            allTimelines.RemoveAt(0);
        if (allTimelines != null && allTimelines.Count > 0)
            FindObjectOfType<PlatformerReplayer>().LaunchReplay(allTimelines, bounceTimeline, bouncePositions);
    }

    public void RecordDoubleJump()
    {
        if (hasRecordedJump) return;

        bounceTimeline[bounceTimeline.Count - 1] = (Time.timeSinceLevelLoad - 0.1f);
        bouncePositions[bouncePositions.Count - 1] = heroTransform.position;
        hasRecordedJump = true;
    }
    
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator Record()
    {
        float delay = GameManager.Instance.GhostRecordSpeed;
        while (isRecording && recordingTimeline.Count < maxActions)
        {
            yield return new WaitForSeconds(delay);
            recordingTimeline.Add(heroTransform.position);
        }
    }

}
