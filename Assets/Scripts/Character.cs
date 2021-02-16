using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private float refillAmount, attackDelay = 0;
    [SerializeField] private bool targetsPlayer;
    protected Pawn pawn;
    protected Animator anim;
    
    [SerializeField] protected float stamina;
    [SerializeField] protected float maxStamina = 100;
    [SerializeField] private float health, maxHealth = 100;
    [SerializeField] private float attack;
    [SerializeField] private GameObject bulletPrefab, UIPanel;
    [SerializeField] protected GameObject deathEffect;

    private Image staminaBar, healthBar;
    private Camera _camera;

    protected readonly KeyCode[] keyCodes = new []
    {
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow, 
        KeyCode.Space
    };

    protected void Start()
    {
        stamina = maxStamina;
        health = maxHealth;
        anim = GetComponentInChildren<Animator>();
        pawn = GetComponent<Pawn>();
        GameObject uiPan = Instantiate(UIPanel, FindObjectOfType<Canvas>().transform);
        staminaBar = uiPan.transform.Find("StaminaBarGO").Find("StaminaBar").GetComponent<Image>();
        healthBar = uiPan.transform.Find("HealthBarGO").Find("HealthBar").GetComponent<Image>();
    }

    public bool TryAction(KeyCode action)
    {
        if (action == KeyCode.Space)
        {
            if (anim != null)
            {
                anim.SetTrigger("attack");
                //Invoke(nameof(Shoot), attackDelay);
            }
            else
                Shoot();
        }
        else
            pawn.MovePawn(action);

        return true;
    }
    
    public void GetHit(float damage)
    {
        health -= damage;
        health = Mathf.Max(0, health);
        healthBar.fillAmount = health / maxHealth;
        if (health > 0)
        {
        }
        else
        {
            DeathFunction();
        }
    }

    public virtual void DeathFunction()
    {
        if (deathEffect != null)
        {
            var tmp = Instantiate(deathEffect);
            tmp.transform.position = transform.position;
        }
        Destroy(gameObject);
    }

    public void Shoot()
    {
        var tmp = Instantiate(bulletPrefab);
        Bullet b = tmp.GetComponent<Bullet>();
        b.target = targetsPlayer ? "Player" : "Enemy";
        b.damage = attack;
        tmp.transform.position = transform.position;
        tmp.transform.forward = transform.forward;
    }
    
    protected void Update()
    {
        if (stamina < 100)
        {
            stamina += refillAmount * Time.deltaTime;
            staminaBar.fillAmount = stamina / maxStamina;
        }
    }
}
