using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpHelpStart : MonoBehaviour
{
    private bool hasEntered, hasReachedEnd;
    // Start is called before the first frame update

    public void SendTriggerMessage(bool isStart)
    {
        if (isStart && !hasEntered)
        {
            StartCoroutine(ShowTip());
            hasEntered = true;
        }
        else
        {
            hasReachedEnd = true;
            StopCoroutine(ShowTip());
        }
    }

    IEnumerator ShowTip()
    {
        yield return new WaitForSeconds(5);
        if (!hasReachedEnd)
            GameManager.Instance.JumpTip();
    }
}
