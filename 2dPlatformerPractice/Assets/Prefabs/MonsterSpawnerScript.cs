using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MonsterSpawnerScript : MonoBehaviour
{
    float DestroyTime = 0;

    bool shouldSpawn = true;

    bool shouldDestroyMonster = false;

    bool updated = false;

    public GameObject Monster0;
    public GameObject Monster1;
    public GameObject Monster2;
    public GameObject Monster3;

    BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        updated = false;
    }

    private void LateUpdate()
    {
        updated = true;
        if(shouldDestroyMonster)
        {
            DestroyTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && shouldSpawn)
        {

            DestroyTime = 0;

            shouldDestroyMonster = false;
            shouldSpawn = false;
            System.Random random = new System.Random();

            int monsterCount = random.Next(5, 7);

            for (int i = 0; i <= monsterCount; i++)
            {
                switch (random.Next(0, 5))
                {
                    case 0:
                        Instantiate(Monster0, new Vector3(random.Next(-25, 25), random.Next(-25, 25), 0) + gameObject.transform.position, gameObject.transform.rotation);
                        break;
                    case 1:
                        Instantiate(Monster1, new Vector3(random.Next(-25, 25), random.Next(-25, 25), 0) + gameObject.transform.position, gameObject.transform.rotation);
                        break;
                    case 2:
                        Instantiate(Monster2, new Vector3(random.Next(-25, 25), random.Next(-25, 25), 0) + gameObject.transform.position, gameObject.transform.rotation);
                        break;
                    case 3:
                        Instantiate(Monster1, new Vector3(random.Next(-25, 25), random.Next(-25, 25), 0) + gameObject.transform.position, gameObject.transform.rotation);
                        break;
                    case 4:
                        Instantiate(Monster1, new Vector3(random.Next(-25, 25), random.Next(-25, 25), 0) + gameObject.transform.position, gameObject.transform.rotation);
                        break;
                }
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            shouldSpawn = true;
            shouldDestroyMonster = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("enemies") && shouldDestroyMonster && updated && DestroyTime >= 0.5)
        {
            Destroy(collision.gameObject);
        }
    }
}
