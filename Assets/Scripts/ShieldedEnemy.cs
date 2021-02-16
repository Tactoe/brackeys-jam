using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldedEnemy : Character
{
    public float shield, shieldRechargeSpeed, maxShield = 100f;

    [SerializeField] private Image shieldIcon;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        stamina = maxStamina;
        health = maxHealth;
        anim = GetComponentInChildren<Animator>();
        pawn = GetComponent<Pawn>();
        GameObject uiPan = Instantiate(UIPanel, FindObjectOfType<Canvas>().transform);
        staminaBar = uiPan.transform.Find("StaminaBarGO").Find("StaminaBar").GetComponent<Image>();
        healthBar = uiPan.transform.Find("HealthBarGO").Find("HealthBar").GetComponent<Image>();
        shieldIcon = uiPan.transform.Find("ShieldBG").Find("Shield").GetComponent<Image>();
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
        while (true)
        {
            TryAction(KeyCode.Space);
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    new bool TryAction(KeyCode action)
    {
        if (stamina < maxStamina) return false;
        
        stamina = 0;
        base.TryAction(action);
        return true;
    }

}
