using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PastSelf : Character
{
    private int i = 0;
    public bool damageOverTime;
    protected new void Start()
    {
        anim = GetComponentInChildren<Animator>();
        pawn = GetComponent<Pawn>();
        pawn.pos = FindObjectOfType<PlayerCharacter>().pawn.pos;
        pawn.UpdatePos();
        if (GameManager.Instance.monsterWaveIndex == 3)
        {
            targetsAll = true;
        }

        /*if (GameManager.Instance.monsterWaveIndex >= 3)
        {
            damageOverTime = true;
        }*/
    }

    public void LaunchReplay(List<TimeNode> previousTimeline)
    {
        StartCoroutine(Replay(previousTimeline));
        /*if (damageOverTime)
            StartCoroutine(DamageEnemies());*/
    }

    /*IEnumerator DamageEnemies()
    {
        GameObject[] eGO =  GameObject.FindGameObjectsWithTag("Enemy");
        List<ShieldedEnemy> enemies = new List<ShieldedEnemy>();
        foreach (var e in eGO)
        {
            enemies.Add(e.GetComponent<ShieldedEnemy>());
        }
        while (true)
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    if (enemy.shield >= enemy.maxShield) continue;
                    enemy.GetHit(1);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }

    }*/
    
    IEnumerator Replay(List<TimeNode> previousTimeline)
    {
        while (i < previousTimeline.Count)
        {
            yield return new WaitForSeconds(previousTimeline[i].nextActionTimer);
                TryAction(previousTimeline[i].action);
            i++;
        }
        yield return new WaitForSeconds(1f);
        DeathFunction();
    }

    protected new void Update()
    {
    }
    
}
