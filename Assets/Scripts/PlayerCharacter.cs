using System;
using System.Collections;
using System.Collections.Generic;
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
        
        stamina = 0;
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
       GameManager.Instance.ReloadScene();
    }
}
