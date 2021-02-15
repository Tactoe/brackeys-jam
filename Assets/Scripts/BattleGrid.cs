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
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                var tmp = Instantiate(cell, transform);
                tmp.transform.localPosition = new Vector3(i, transform.position.y, j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
