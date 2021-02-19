using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShieldedEnemy : Character
{
    public float shield, shieldRechargeSpeed, maxShield = 100f;

    [SerializeField] private bool isMoving, isBoss;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        StartCoroutine(EnemyBehavior());
    }
    
    protected new void Update()
    {
        base.Update();
        if (shield < maxShield)
        {
            shield += shieldRechargeSpeed * Time.deltaTime;
            shieldIcon.fillAmount = shield / maxShield;
        }
    }

    public override void GetHit(float damage)
    {
        if (shield < 100)
        {
            base.GetHit(damage);
            shield = 0;
        }
        else
        {
            shield = 0;
        }
    }
    
    IEnumerator EnemyBehavior()
    {
        if (isMoving)
        {
            KeyCode[] actions = new[]
            {
                KeyCode.LeftArrow,
                KeyCode.Space,
                KeyCode.RightArrow,
                KeyCode.RightArrow,
                KeyCode.Space,
                KeyCode.LeftArrow
            };
            int i = 0;
            while (true)
            {
                if (i >= actions.Length)
                    i = 0;
                if (TryAction(actions[i]))
                    i++;
                else
                {
                    yield return new WaitForSeconds(0.25f);
                }
            }
        }
        if (isBoss)
        {
            while (true)
            {
                TryAction(KeyCode.Alpha0);
                yield return new WaitForSeconds(0.25f);
            }
            
        }
        else
        {
            while (true)
            {
                TryAction(KeyCode.Space);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
    
    new bool TryAction(KeyCode action)
    {
        if (stamina < maxStamina) return false;
        
        stamina = 0;
        Transform targetPawnTF = FindObjectOfType<PlayerCharacter>().transform.parent.Find("Cells");
        if (action == KeyCode.Alpha0)
        {
            for (int i = 0; i < targetPawnTF.childCount; i++)
            {
                    Launch(targetPawnTF.GetChild(i).position);
            }
        }
        base.TryAction(action);
        return true;
    }

}
