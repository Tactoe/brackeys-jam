using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "DialogueNode")]
public class DialogueNode : ScriptableObject
{
    public string speakerName;
    [TextArea(5, 6)]
    public string text;
    public Sprite img, leftSpeaker, rightSpeaker;
}
