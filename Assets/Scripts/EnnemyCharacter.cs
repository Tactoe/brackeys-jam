using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyCharacter : MonoBehaviour
{
    private Pawn pawn;
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
            pawn.DoAction(KeyCode.LeftArrow);
            yield return new WaitForSeconds(0.5f);
            pawn.DoAction(KeyCode.RightArrow);
            yield return new WaitForSeconds(0.5f);
            pawn.DoAction(KeyCode.Space);
        }
    }
}
