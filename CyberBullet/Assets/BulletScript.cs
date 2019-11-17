using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    BoxCollider2D collider;
    Rigidbody2D rigidBody;

    float life = 10;

    public float speed = 100;

    public bool isEnemy;

    bool isStarted = true;
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStarted)
        { 
            rigidBody.AddForce(gameObject.transform.up * speed / 5, ForceMode2D.Impulse);
            isStarted = false;
        }
        rigidBody.AddForce(gameObject.transform.up * Time.deltaTime * speed, ForceMode2D.Force);
        life -= Time.deltaTime;
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && collision.gameObject.GetComponent<BulletScript>().isEnemy != isEnemy)
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(this.gameObject);
        }
    }
    
}
