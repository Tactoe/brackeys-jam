using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyCharacter : MonoBehaviour
{
    private Pawn pawn;
    
    private readonly KeyCode[] keyCodes = new []
    {
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow, 
        KeyCode.Space
    };
    // Start is called before the first frame update
    void Start()
    {
        pawn = GetComponent<Pawn>();
        StartCoroutine(EnemyBehavior());
    }


    IEnumerator EnemyBehavior()
    {
        while (true)
        {
            pawn.DoAction(Random.Range(0, 1) == 1 ? KeyCode.Space : keyCodes[Random.Range(0, keyCodes.Length)]);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
