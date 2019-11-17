using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyType0;
    public GameObject EnemyType1;
    public GameObject EnemyType2;

    float spawningTime = 0;

    System.Random random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawningTime += Time.deltaTime;

        if(spawningTime > 6.0f)
        {
            switch (random.Next(0, 2))
            {
                case 0:
                    Instantiate(EnemyType0, gameObject.transform.position, gameObject.transform.rotation);

                    break;

                case 1:
                    Instantiate(EnemyType1, gameObject.transform.position, gameObject.transform.rotation);

                    break;

                case 2:

                    Instantiate(EnemyType2, gameObject.transform.position, gameObject.transform.rotation);
                    break;
            }

            spawningTime = 0;
        }
        
    }
}
