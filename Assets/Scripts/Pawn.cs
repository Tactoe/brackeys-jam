using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Position
{
    public int x;
    public int y;
}

public class Pawn : MonoBehaviour
{
    [SerializeField] private bool usesStamina;
    [SerializeField] private Position pos;
    [SerializeField] BattleGrid currentGrid;
    [SerializeField] private float refillAmount;
    [SerializeField] private string target;
    private float stamina, maxStamina;
    private float health;
    
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private Image staminaBar, healthBar;
    private Camera _camera;

    private void Awake()
    {
        BattleGrid[] grids = FindObjectsOfType<BattleGrid>();
        foreach (var grid in grids)
        {
            if (grid.CompareTag(gameObject.tag))
                currentGrid = grid;
        }

        transform.parent = currentGrid.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        if (usesStamina)
        {
            maxStamina = 100;
            stamina = maxStamina;
            _camera = Camera.main;
        }
        UpdatePos();
    }
    
    void Update()
    {
        if (usesStamina)
        {
            if (stamina < 100)
            {
                stamina += refillAmount * Time.deltaTime;
                staminaBar.fillAmount = stamina / maxStamina;
            }
        }
    }

    internal void Shoot()
    {
        var tmp = Instantiate(bulletPrefab);
        tmp.transform.position = transform.position;
        tmp.transform.forward = transform.forward;
        tmp.GetComponent<Bullet>().target = target;
    }

    internal void DoAction(KeyCode action)
    {
        if (usesStamina)
        {
            if (stamina < maxStamina) return;
            stamina = 0;
        }
        
        if (action == KeyCode.Space)
            Shoot();
        if (action == KeyCode.LeftArrow || action == KeyCode.RightArrow)
            pos.x += action == KeyCode.RightArrow ? 1 : -1;
        if (action == KeyCode.UpArrow || action == KeyCode.DownArrow)
            pos.y += action == KeyCode.UpArrow ? 1 : -1;
        UpdatePos();
    }

    public void GetHit(int damage)
    {
        if (health - damage > 0)
        {
            health -= damage;
            healthBar.fillAmount = health / maxHealth;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void UpdatePos()
    {
        var transform1 = transform;
        pos.x = Mathf.Clamp(pos.x, 0, currentGrid.x - 1);
        pos.y = Mathf.Clamp(pos.y, 0, currentGrid.y - 1);
        transform1.localPosition = new Vector3(pos.x, transform1.position.y, pos.y);
    }
    
    // Update is called once per frame
}
