using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckMonsters());
    }

    IEnumerator CheckMonsters()
    {
        while (true)
        {
            if (FindObjectsOfType<ShieldedEnemy>().Length == 0)
                GameManager.Instance.NextScene();
            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
}
