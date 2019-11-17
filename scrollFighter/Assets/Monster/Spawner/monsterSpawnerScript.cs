using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSpawnerScript : MonoBehaviour
{
    public GameObject[] SpawningMonster = new GameObject[10];
    public float SpawnCoolMax = 5;
    float SpawnCool = 0;
    public GameObject Boss;
    public float BossTime = 60;
    float bossCool = 0;
    bool isBossSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBossSpawned)
        {

        }
        else
        {
            bossCool += Time.deltaTime;
            if(bossCool > BossTime)
            {
                Instantiate(Boss, transform.position, transform.rotation);
                isBossSpawned = true;
            }
        }
        SpawnCool += Time.deltaTime;

        if(SpawnCool > SpawnCoolMax)
        {
            SpawnCool = 0;
            int MonsterNumber = Random.Range(0, SpawningMonster.Length);
            Instantiate(SpawningMonster[MonsterNumber], transform.position, transform.rotation);
        }

    }
}
