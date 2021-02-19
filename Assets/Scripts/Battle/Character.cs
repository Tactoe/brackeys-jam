using System;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private float refillAmount;
    [SerializeField] private bool targetsPlayer; 
    public Pawn pawn;
    protected Animator anim;
    
    [SerializeField] protected string charName;
    [SerializeField] protected float stamina;
    [SerializeField] protected float maxStamina = 100;
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] private float attack;
    [SerializeField] protected GameObject bulletPrefab, UIPanel;
    [SerializeField] protected GameObject deathEffect;
    [SerializeField] protected Sprite charSprite; 

    [SerializeField] protected Image staminaBar;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected Image shieldIcon;
    [SerializeField] protected Image uiSprite;
    protected bool dead, hurtAnimationPlaying;
    protected TextMeshProUGUI nameText, healthText;

    protected readonly KeyCode[] keyCodes = new []
    {
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow, 
        KeyCode.Space
    };

    public GameObject uiPan;
    private static readonly int Attack = Animator.StringToHash("attack");

    protected void Start()
    {
        stamina = 0;
        health = maxHealth;
        anim = GetComponentInChildren<Animator>();
        pawn = GetComponent<Pawn>();
        var ok = FindObjectsOfType<Canvas>();
        foreach (var canvas in ok)
        {
            if (canvas.CompareTag("Player"))
            {
                if (UIPanel.name == "MonsterPanel")
                {
                    uiPan = Instantiate(UIPanel, canvas.transform.Find("MonsterGrid"));
                }
                else
                    uiPan = Instantiate(UIPanel, canvas.transform);
            }
        }
        staminaBar = uiPan.transform.Find("StaminaBarGO").Find("StaminaBar").GetComponent<Image>();
        healthBar = uiPan.transform.Find("HealthBarGO").Find("HealthBar").GetComponent<Image>(); 
        healthText = uiPan.transform.Find("HealthBarGO").Find("HealthText").GetComponent<TextMeshProUGUI>();
        healthText.text = "Health: " + health + "/" + maxHealth;
        nameText = uiPan.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        uiSprite = uiPan.transform.Find("CharSprite").GetComponent<Image>();
        if (UIPanel.name == "MonsterPanel")
        {
            uiSprite.sprite = charSprite;
            nameText.transform.Find("TextColor").GetComponent<Image>().sprite = charSprite;
            shieldIcon = uiPan.transform.Find("ShieldBG").Find("Shield").GetComponent<Image>();
        }
        nameText.text = charName;
    }

    public bool TryAction(KeyCode action)
    {
        if (dead) return false;
        if (action == KeyCode.Space)
        {
            if (anim != null)
            {
                anim.SetTrigger(Attack);
                //Invoke(nameof(Shoot), attackDelay);
            }
            else
                Shoot();
        }
        else
            pawn.MovePawn(action);

        return true;
    }
    
    public virtual void GetHit(float damage)
    {
        if (dead) return;
        float hurtDuration = 0.3f;
        if (!hurtAnimationPlaying)
        {
            hurtAnimationPlaying = true;
            var spr = uiSprite.DOColor(new Color(200, 0, 0), hurtDuration);
            spr.OnComplete(() => uiSprite.DOColor(Color.white, hurtDuration));
            uiSprite.transform.DOShakePosition(hurtDuration * 2, Vector2.one * 10).OnComplete(() => hurtAnimationPlaying = false);
        }
        health -= damage;
        health = Mathf.Max(0, health);
        healthBar.fillAmount = health / maxHealth;
        healthText.text = "Health: " + health + "/" + maxHealth;
        if (health > 0)
        {
        }
        else
        {
            dead = true;
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

    void SetupBullet()
    {
        
    }

    public void Shoot()
    {
        var tmp = Instantiate(bulletPrefab);
        Bullet b = tmp.GetComponent<Bullet>();
        b.targetTag = targetsPlayer ? "Player" : "Enemy";
        if (b.isLaunched)
            b.targetPos = FindObjectOfType<PlayerCharacter>().transform.position;
        b.damage = attack;
        tmp.transform.position = transform.position;
        tmp.transform.forward = transform.forward;
    }

    public void Launch(Vector3 targetPos)
    {
        var tmp = Instantiate(bulletPrefab);
        Bullet b = tmp.GetComponent<Bullet>();
        b.targetTag = targetsPlayer ? "Player" : "Enemy";
        if (b.isLaunched)
            b.targetPos = targetPos;
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
