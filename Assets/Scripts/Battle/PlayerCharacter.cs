using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerCharacter : Character
{
    private ActionRecorder rec;

    new void Start()
    {
        base.Start();
        rec = FindObjectOfType<ActionRecorder>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        ActionKeyPressed();
    }

    new bool TryAction(KeyCode action)
    {
        if (stamina < maxStamina) return false;
        
        stamina -= action == KeyCode.Space ? 100 : 50;
        base.TryAction(action);
        return true;
    }

    void ActionKeyPressed()
    {
        foreach (KeyCode keyCode in keyCodes)
        {
            if (Input.GetKeyDown(keyCode)) {
                if (TryAction(keyCode))
                    rec.AddAction(keyCode);
            }
        }
    }

    public override void DeathFunction()
    {
        if (GameManager.Instance.doDialogueOnDeath)
        {
            FindObjectOfType<BattleAudio>().FadeOut(3);
            GameManager.Instance.fadeImg.color = Color.black;
            GameManager.Instance.fadeImgCG.DOFade(1, 4).OnComplete(() =>
            {
                GameManager.Instance.doDialogueOnDeath = false; 
                GameManager.Instance.LoadScene("Fireplace");
            });
        }
        else
        {
            BattleAudio.Instance.EnableSecondaryTrack(1);
            GameManager.Instance.ReloadScene();
        }
    }
}
