using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

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
public class ActionRecorder : MonoBehaviour
{
    private List<TimeNode> timeline;
    [SerializeField] private GameObject dopelGO;

    private float currentTime;

    private float lastTimeSaved;
    // Start is called before the first frame update
    void Start()
    {
        timeline = new List<TimeNode>();
        DontDestroyOnLoad(gameObject);
        lastTimeSaved = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            StartCoroutine(Replay());
    }
    
    public void AddAction(KeyCode action)
    {
        timeline.Add(new TimeNode(action, Time.time - lastTimeSaved));
        lastTimeSaved = Time.time;
    }

    IEnumerator Replay()
    {
        int i = 0;
        GameObject dopel = Instantiate(dopelGO);
        Pawn dopelPawn = dopel.GetComponent<Pawn>();
        while (i < timeline.Count)
        {
            yield return new WaitForSeconds(timeline[i].nextActionTimer);
            dopelPawn.DoAction(timeline[i].action);
            i++;
        }
        Destroy(dopel);
    }
}
