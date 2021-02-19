using UnityEngine;
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
