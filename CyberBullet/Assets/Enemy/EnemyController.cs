using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float Age = 0;
    Vector2 movingDirection;
    BoxCollider2D collider;
    Rigidbody2D rigidBody;
    GameObject player;

    float speed;
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerScript>().gameObject;
        movingDirection = (player.transform.position - gameObject.transform.position).normalized;

        speed = Random.value * 300.0f + 100.0f;
    }

    // Update is called once per frame
    void Update()
    {/*
        Vector3 v = player.transform.position - transform.position;
        v.z = 0;
        transform.LookAt(player.transform.position - v);*/
        Age += Time.deltaTime;

        rigidBody.AddForce(movingDirection * Time.deltaTime * speed, ForceMode2D.Force);

        Vector2 direction = rigidBody.velocity.normalized;
        if (direction != new Vector2(0, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        }

        //rigidBody.AddForce(gameObject.transform.up * Time.deltaTime * -1000, ForceMode2D.Force);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            player.GetComponent<PlayerScript>().lose();
        }
        else if (collision.gameObject.CompareTag("wall") && Age > 3)
        {
            Destroy(this.gameObject);
        }
        else if( collision.gameObject.CompareTag("Bullet") && !collision.gameObject.GetComponent<BulletScript>().isEnemy)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControlScript>().score += 10;
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
}
