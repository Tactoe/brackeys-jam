using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpTrigger : MonoBehaviour
{
    public bool isStart;
    private void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<WallJumpHelpStart>().SendTriggerMessage(isStart);
    }
}
