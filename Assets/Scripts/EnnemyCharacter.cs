using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyCharacter : Character
{
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
        base.Start();
        StartCoroutine(EnemyBehavior());
    }


    IEnumerator EnemyBehavior()
    {
        while (true)
        {
            TryAction(Random.Range(0, 1) == 1 ? KeyCode.Space : keyCodes[Random.Range(0, keyCodes.Length)]);
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    new bool TryAction(KeyCode action)
    {
        if (stamina < maxStamina) return false;
        
        stamina = 0;
        base.TryAction(action);
        return true;
    }
    
    public override void DeathFunction()
    {
        Destroy(gameObject);
    }
    
}
