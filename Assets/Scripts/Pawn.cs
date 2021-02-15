using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Position
{
    public int x;
    public int y;
}

public class Pawn : MonoBehaviour
{
    [SerializeField] private Position pos;
    [SerializeField] BattleGrid currentGrid;
    [SerializeField] private float refillSpeed, refillAmount;
    [SerializeField] private string target;
    private float stamina;
    [SerializeField] private GameObject bulletPrefab;

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
        UpdatePos();
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
        if (action == KeyCode.Space)
            Shoot();
        if (action == KeyCode.LeftArrow || action == KeyCode.RightArrow)
            pos.x += action == KeyCode.RightArrow ? 1 : -1;
        if (action == KeyCode.UpArrow || action == KeyCode.DownArrow)
            pos.y += action == KeyCode.UpArrow ? 1 : -1;
        UpdatePos();
        
        stamina = 0;
        StartCoroutine(Refill());
    }

    void UpdatePos()
    {
        var transform1 = transform;
        pos.x = Mathf.Clamp(pos.x, 0, currentGrid.x - 1);
        pos.y = Mathf.Clamp(pos.y, 0, currentGrid.y - 1);
        transform1.localPosition = new Vector3(pos.x, transform1.position.y, pos.y);
    }
    
    IEnumerator Refill()
    {
        while (stamina < 100)
        {
            stamina += refillAmount;
            yield return new WaitForSeconds(refillSpeed);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
