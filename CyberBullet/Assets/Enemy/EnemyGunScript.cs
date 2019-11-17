using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyGunScript : MonoBehaviour
{
    public GameObject bulletType1;
    public GameObject bulletType0;
    float shootTime = 0;
    public float shootCool = 1f;
    public bool BulletType;

    SpriteRenderer spriteRenderer;
    
    

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //if(BulletType  == null)
       // {
            BulletType = new System.Random().Next(0, 2).Equals(1);
        if (UnityEngine.Random.value > 0.5f)
        {
            BulletType = true;
            gameObject.GetComponentInParent<EnemyController>().gameObject.layer = 9;
        }
        else
        {
            BulletType = false;
            gameObject.GetComponentInParent<EnemyController>().gameObject.layer = 8;
        }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        shootTime += Time.deltaTime;
        if (shootTime > shootCool)
        {
            if (BulletType)
            {
                Instantiate(bulletType1, gameObject.transform.position, gameObject.transform.rotation);
                spriteRenderer.color = new Color(0, 0, 1, 1);

            }
            else
            {
                Instantiate(bulletType0, gameObject.transform.position, gameObject.transform.rotation);
                spriteRenderer.color = new Color(1, 0, 0, 1);

            }
            shootTime = 0;
        }
    }
    
}
