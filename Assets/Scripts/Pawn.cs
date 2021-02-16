using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[Serializable]
public class Pawn : MonoBehaviour
{
    [SerializeField] private bool usesStamina;
    public Vector2 pos;
    [SerializeField] BattleGrid currentGrid;
    
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
    


    internal void MovePawn(KeyCode action)
    {
        if (action == KeyCode.LeftArrow || action == KeyCode.RightArrow)
            pos.x += action == KeyCode.RightArrow ? 1 : -1;
        if (action == KeyCode.UpArrow || action == KeyCode.DownArrow)
            pos.y += action == KeyCode.UpArrow ? 1 : -1;
        UpdatePos();
    }


    void UpdatePos()
    {
        var transform1 = transform;
        pos.x = Mathf.Clamp(pos.x, 0, currentGrid.x - 1);
        pos.y = Mathf.Clamp(pos.y, 0, currentGrid.y - 1);
        transform1?.DOLocalJump(new Vector3(pos.x, 1, pos.y), 0.2f, 1, 0.1f).SetEase(Ease.Flash);
    }
}
