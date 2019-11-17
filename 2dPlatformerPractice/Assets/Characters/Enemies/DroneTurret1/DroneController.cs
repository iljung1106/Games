using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    HealthController healthCon;

    float health = 100;

    float settingTargetTime = 2;

    public AudioClip hitSound;

    BoxCollider2D collider;
    private GameObject[] targets = new GameObject[200];
    private GameObject selectedTarget;
    private float distance;
    private float angle;
    private Vector3 directionVector;
    private Rigidbody2D rigid;
    float explosionTime = 0;
    bool shouldTimer = false;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
        healthCon = gameObject.GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        health = healthCon.BHealth;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            settingTargetTime += Time.deltaTime;

            health = healthCon.BHealth;

            /*
            if (rigid.velocity.x < 0)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            */
            if (settingTargetTime >= 1)
            {
                settingTargetTime = 0;
                targets = GameObject.FindGameObjectsWithTag("team");
                //distance = Vector2.Distance(transform.position, targets[0].transform.position);
                selectedTarget = GameObject.FindGameObjectWithTag("Player");
                //directionVector = targets[0].transform.position - transform.position;
                directionVector = selectedTarget.transform.position - transform.position;
                distance = Vector2.Distance(transform.position, selectedTarget.transform.position);
                for (int i = 1; i <= targets.Length; i++)
                {


                    if (distance >= Vector2.Distance(transform.position, targets[i - 1].transform.position))
                    {
                        selectedTarget = targets[i - 1];
                        //Debug.Log(selectedTarget.name);
                    }
                    if (i < targets.Length)
                    {
                        break;
                    }

                }
            }
            distance = Vector2.Distance(transform.position, selectedTarget.transform.position);

            directionVector = selectedTarget.transform.position - transform.position;
            directionVector.z = 0;

            directionVector = directionVector.normalized;
            //Debug.Log(selectedTarget.name);
            if (distance <= 30)
            {
                rigid.AddForce((directionVector * 100000 * Time.deltaTime), ForceMode2D.Force);
            }

            if (shouldTimer)
            {
                explosionTime += Time.deltaTime;
            }

            if (explosionTime >= 3.5)
            {
                Destroy(this.gameObject);
            }
            else if (explosionTime >= 3)
            {
                collider.size = new Vector2(10, 10);
            }

        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!shouldTimer)
            {
                shouldTimer = true;
                audioSource.Play();
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (explosionTime >= 3 && explosionTime < 3.1)
            {
                collision.gameObject.GetComponent<HealthController>().BHealth -= (200f * Time.deltaTime);
            }
        }
    }
}
