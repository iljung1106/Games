using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = System.Random;

public class spawnPTCon : MonoBehaviour
{
    public GameObject roomType0;
    public GameObject roomType1;
    public GameObject roomType2;
    public GameObject roomType3;
    public GameObject roomType4;
    public GameObject roomType5;

    BoxCollider2D collider;

    bool OverLapped = false;

    float startTime = 0;

    bool isSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;

        if(startTime >= 5f && !OverLapped && !isSpawned)
        {
            Random ran = new Random();
            switch (ran.Next(0, 3))
            {
                case 0:
                    Instantiate(roomType0, this.transform.position, this.transform.rotation);
                    break;

                case 1:
                    Instantiate(roomType1, this.transform.position, this.transform.rotation);
                    break;

                case 2:
                    Instantiate(roomType2, this.transform.position, this.transform.rotation);
                    break;

                case 3:
                    Instantiate(roomType3, this.transform.position, this.transform.rotation);
                    break;

                case 4:
                    Instantiate(roomType4, this.transform.position, this.transform.rotation);
                    break;

                case 5:
                    Instantiate(roomType5, this.transform.position, this.transform.rotation);
                    break;
            }
            isSpawned = true;
        }
    }
    

  


    public void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("OverLapped");
        OverLapped = true;
    }
    
}
