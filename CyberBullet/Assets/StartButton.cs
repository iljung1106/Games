using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject Player;
    public Transform ShopOpenButton;
    public Sprite Skin0;
    public Sprite Skin1;
    public Sprite Skin2;
    public Sprite Skin3;
    public Sprite Skin4;
    public Sprite Skin5;
    public Sprite Skin6;

    string gameId = "3070397";


    Sprite SelectedSkin;
    // Start is called before the first frame update
    void Start()
    {

        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<EnemySpawner>().enabled = false;
        }


        SelectedSkin = Skin0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        GameObject player = Instantiate(Player);

        player.GetComponentInChildren<SpriteRenderer>().sprite = SelectedSkin;
        
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<EnemySpawner>().enabled = true;
        }
        ShopOpenButton.localScale = new Vector2(0, 0);
    }

    public void SetPlayerSkin(int i)
    {
        switch (i)
        {
            case 0:
                SelectedSkin = Skin0;
                break;
            case 1:
                if(PlayerPrefs.GetInt("HaveSkin1")==1)
                {
                    SelectedSkin = Skin1;
                }
                else
                {
                    Advertisement.Show();
                    PlayerPrefs.SetInt("HaveSkin1", 1);
                }
                break;
            case 2:
                if (PlayerPrefs.GetInt("HaveSkin2") == 1)
                {
                    SelectedSkin = Skin2;
                }
                else
                {
                    Advertisement.Show();
                    PlayerPrefs.SetInt("HaveSkin2", 1);
                }
                break;
            case 3:
                if (PlayerPrefs.GetInt("HaveSkin3") == 1)
                {
                    SelectedSkin = Skin3;
                }
                else
                {
                    Advertisement.Show();
                    PlayerPrefs.SetInt("HaveSkin3", 1);
                }
                break;
            case 4:
                if (PlayerPrefs.GetInt("HaveSkin4") == 1)
                {
                    SelectedSkin = Skin4;
                }
                else
                {
                    Advertisement.Show();
                    PlayerPrefs.SetInt("HaveSkin4", 1);
                }
                break;
            case 5:
                if (PlayerPrefs.GetInt("HaveSkin5") == 1)
                {
                    SelectedSkin = Skin5;
                }
                else
                {
                    Advertisement.Show();
                    PlayerPrefs.SetInt("HaveSkin5", 1);
                }
                break;
            case 6:
                if (PlayerPrefs.GetInt("HaveSkin6") == 1)
                {
                    SelectedSkin = Skin6;
                }
                else
                {
                    Advertisement.Show();
                    PlayerPrefs.SetInt("HaveSkin6", 1);
                }
                break;
        }
    }
}
