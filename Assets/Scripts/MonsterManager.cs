using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private MonsterWave[] _monsterWaves;
    
    [System.Serializable]
    public class MonsterWave
    {
        public GameObject[] monsters;
        public Vector2[] position;
    }
    
    void Start()
    {
        SpawnMonsters(GameManager.Instance.monsterWaveIndex);
        StartCoroutine(CheckMonsters());
    }

    void SpawnMonsters(int waveIndex)
    {
        MonsterWave wave = _monsterWaves[waveIndex];
        for (int i = 0; i < wave.monsters.Length; i++)
        {
            GameObject tmp = wave.monsters[i];
            tmp.GetComponent<Pawn>().pos = wave.position[i];
            Instantiate(wave.monsters[i], Vector3.zero, wave.monsters[i].transform.rotation);
        }
    }

    IEnumerator CheckMonsters()
    {
        while (true)
        {
            if (FindObjectsOfType<ShieldedEnemy>().Length == 0)
            {
                GameManager.Instance.monsterWaveIndex++;
                GameManager.Instance.doDialogueOnDeath = true;
                GameManager.Instance.LoadScene("Platform");
            }
            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
}
