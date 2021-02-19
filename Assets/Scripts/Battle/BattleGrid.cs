using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGrid : MonoBehaviour
{
    [SerializeField] private GameObject cell;
    public int x;
    public int y;
    
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.monsterWaveIndex > 1)
        {
            x = 5;
        }
        if (x >= 5)
        {
            transform.position -= Vector3.right * 0.5f;
        }
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                var tmp = Instantiate(cell, transform.Find("Cells"));
                tmp.transform.localPosition = new Vector3(i, transform.position.y, j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
