using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Script : MonoBehaviour
{
    Animator animator;

    public float Health = 40;

    public GameObject circleBulletSpawner;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.Play("Pattern1");

        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<EnemySpawner>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0)
        {
            animator.Play("Die");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !collision.gameObject.GetComponent<BulletScript>().isEnemy)
        {
            Health -= 1;
            Destroy(collision.gameObject);
        }
    }

    public void spawnCircleBullet()
    {
        int seed = Random.Range(0, 2);
        CircleBulletSpawner spawner = Instantiate(circleBulletSpawner, gameObject.transform.position, gameObject.transform.rotation).GetComponent<CircleBulletSpawner>();
        spawner.SpawnBullet(seed.Equals(1), Random.Range(5, 41), 360, Random.Range(10, 40));
    }

    public void Kill()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControlScript>().score += 200;

        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<EnemySpawner>().enabled = true;
        }

        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<EnemySpawner>().enabled = true;
        }
        

    }
}
