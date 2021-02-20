using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        while (FindObjectsOfType<ShieldedEnemy>().Length != 0)
        {
            yield return new WaitForSeconds(1f);
        }

        FindObjectOfType<PlayerCharacter>().canAct = false;
        GameManager.Instance.monsterWaveIndex++;
        GameManager.Instance.doDialogueOnDeath = true;
        FindObjectOfType<BattleAudio>().FadeOut(3);
        if (GameManager.Instance.fireplaceDialogueIndex == 4)
            GameManager.Instance.LoadSceneFade("FinalPlat", 3, Color.black);
        else
            GameManager.Instance.LoadSceneFade("PlatformAdditiveBase", 3, Color.black);
    }

    // Update is called once per frame
}
