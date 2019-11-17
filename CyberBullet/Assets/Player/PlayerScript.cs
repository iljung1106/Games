using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    static bool Pbulletcolor = true;

    BoxCollider2D collider;

    Rigidbody2D rigidBody;

    LeftPadScript leftPad;

    Vector2 moveDirection;

    AudioSource audioSource;

    public AudioClip DieSound;

    GameControlScript gameControlScript;

    public float speed = 7;
    

    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        leftPad = FindObjectOfType<LeftPadScript>();
        audioSource = gameObject.GetComponent<AudioSource>();

        GameObject.FindGameObjectWithTag("Menu").GetComponent<StartButton>().ShopOpenButton.localScale = new Vector2(0, 0);
        GameObject.FindGameObjectWithTag("Menu").GetComponent<StartButton>().ShopOpenButton.GetComponentInChildren<Button>().enabled = false;

        gameControlScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControlScript>();

        Physics2D.IgnoreLayerCollision(8, 9);
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = leftPad.Axis;

        //rigidBody.AddForce(moveDirection * Time.deltaTime * 1000, ForceMode2D.Force);
        rigidBody.velocity = (moveDirection * Time.deltaTime * 1000 * speed);



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if(collision.gameObject.GetComponent<BulletScript>().isEnemy)
            {
                lose();
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            lose();
        }
    }
    
    public void lose()
    {
        if(gameControlScript.score > gameControlScript.HighScore)
        {
            gameControlScript.SetHighScore();
        }
        gameControlScript.LastScore = gameControlScript.score;
        gameControlScript.score = 0;
        gameControlScript.Boss1Spawned = false;

        Debug.Log("Dead");
        gameObject.transform.position = new Vector3(0, 0, 0);

        audioSource.clip = DieSound;
        audioSource.Play();

        GameObject[] objectsToKill = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i< objectsToKill.Length; i++)
        {
            Destroy(objectsToKill[i]);
        }

        GameObject[] bulletsToKill = GameObject.FindGameObjectsWithTag("Bullet");


        for (int i = 0; i < bulletsToKill.Length; i++)
        {
            Destroy(bulletsToKill[i]);
        }

        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<EnemySpawner>().enabled = false;
        }
        GameObject.FindGameObjectWithTag("Menu").transform.localScale = new Vector3(1, 1, 1);
        GameObject.FindGameObjectWithTag("Menu").GetComponent<StartButton>().ShopOpenButton.localScale = gameObject.transform.lossyScale;
        GameObject.FindGameObjectWithTag("Menu").GetComponent<StartButton>().ShopOpenButton.GetComponentInParent<Button>().enabled = true;
        Destroy(gameObject);

    }

    public void ChangeBulletColor()
    {
        Pbulletcolor = !Pbulletcolor;
    }

    public bool GetBulletColor()
    {
        return Pbulletcolor;
    }
}
