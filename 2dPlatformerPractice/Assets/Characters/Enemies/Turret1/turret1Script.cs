using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret1Script : MonoBehaviour
{
    float health = 100;

    SpriteRenderer spriteRenderer;

    HealthController healthCon;

    GameObject other1;
    Rigidbody2D rigidbody;
    float time = 0f;
    bool isattacking = false;
    public Transform bulletType;
    GameObject bullet;
    GameObject light;
    float angle;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        other1 = GameObject.Find("Boy");
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        healthCon = gameObject.GetComponent<HealthController>();
    }

// Update is called once per frame
void Update()
    {
        health = healthCon.BHealth;
        if (health <= 0)
        {

        }
        else
        {

            time += Time.deltaTime;

            Vector3 otherVector = other1.transform.position;
            otherVector.z = 0f;
            Vector3 thisVector = transform.position;
            thisVector.z = 0f;

            Vector3 relativePos = otherVector - thisVector;

            distance = relativePos.magnitude;



            if (distance <= 30)
            {

                angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
                spriteRenderer.color = new Color(1, 1, 1);
                //lightCom.intensity = 1f;

            }
            else
            {
                time = 0f;
                isattacking = false;
                //lightCom.intensity = 0f;
                //lightCom.color = new Color(1.0f, 1.0f, 1.0f);
            }


            if (time >= 2f /*&& distance <= 20*/)
            {
                switch (isattacking)
                {
                    case false:
                        if (time >= 3f)
                        {
                            //Debug.Log("발사");
                            Instantiate(bulletType, transform.position, transform.rotation);
                            isattacking = true;
                            spriteRenderer.color = new Color(255, 255, 128, 255);

                        }
                        else
                        {
                            spriteRenderer.color -= new Color(0, 0, 255 * Time.deltaTime);
                            //lightCom.intensity += 1 * Time.deltaTime;
                            //lightCom.color -= new Color(0.0f, 0.5f, 1f) * Time.deltaTime;
                        }
                        break;
                    case true:
                        if (time >= 4.5f)
                        {
                            spriteRenderer.color = new Color(255, 255, 255, 255);
                            time = 0f;
                            isattacking = false;
                            //Debug.Log("공격끝");
                            //lightCom.intensity = 1f;
                            //lightCom.color = new Color(1.0f, 1.0f, 1.0f);
                        }
                        else
                        {
                            spriteRenderer.color += new Color(0, 0, 128 * Time.deltaTime,0);
                            //lightCom.intensity -= 0.66f * Time.deltaTime;
                            //lightCom.color += new Color(0.0f, 0.33f, 0.66f) * Time.deltaTime;
                        }
                        break;
                }
            }
            else if (distance < 30)
            {
                float movingAngle = angle;
                if (angle > 180)
                {
                    movingAngle -= 180;
                }
                else
                {
                    movingAngle += 180;
                }
                rigidbody.MoveRotation(movingAngle);
            }
        }
    }
}
