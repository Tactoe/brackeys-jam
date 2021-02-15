using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCell : MonoBehaviour
{
    public bool isWalkable;
    // Start is called before the first frame update
    void Start()
    {
        if (isWalkable)
            GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
