using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBulletSpawner : MonoBehaviour
{
    public GameObject bulletType1;
    public GameObject bulletType0;

    float a = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBullet(bool bType , float speed, float MaxAngle, float movingAngle)
    {
        for(float i = 0; i < MaxAngle; i+=movingAngle)
        {
            if (bType)
            {
                GameObject bullet;
                bullet = Instantiate(bulletType1, gameObject.transform.position, new Quaternion(0, 0, i, 0));

                bullet.GetComponent<Rigidbody2D>().rotation = i;
                bullet.GetComponent<BulletScript>().speed = speed;
                Debug.Log("Spawn");

            }
            else
            {
                GameObject bullet;
                bullet = Instantiate(bulletType0, gameObject.transform.position, new Quaternion(0, 0, i, 0));

                bullet.GetComponent<Rigidbody2D>().rotation = i;
                bullet.GetComponent<BulletScript>().speed = speed;
                Debug.Log("Spawn");

            }
        }
        Destroy(gameObject);
    }

}
