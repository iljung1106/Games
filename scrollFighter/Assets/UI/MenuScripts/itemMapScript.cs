using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemMapScript : MonoBehaviour
{
    public int price;
    public Text stateText;
    GameManager gameManager;
    public int MapNumber;
    int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        state = PlayerPrefs.GetInt("havingMap" + MapNumber, 0);
        if (state == 1)
        {
            state = 1;
            stateText.text = "선택하기";
        }
        else if (state == 0)
        {
            state = 0;
            stateText.text = price.ToString() + "D로 구입";
        }
        if (PlayerPrefs.GetInt("selectedMap", 0) == MapNumber)
        {
            state = 2;
            stateText.text = "선택됨";
            gameManager.SelectedMap = MapNumber;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select()
    {
        if (state == 1)
        {
            itemMapScript[] Others = FindObjectsOfType<itemMapScript>();
            for (int i = 0; i < Others.Length; i++)
            {
                Others[i].Deselect();
            }
            state = 2;
            PlayerPrefs.SetInt("havingMap" + MapNumber, 1);
            PlayerPrefs.SetInt("selectedMap", MapNumber);
            stateText.text = "선택됨";
            gameManager.SelectedMap = MapNumber;
            gameManager.SaveMoney();
        }
        else if (gameManager.coins >= price && state == 0)
        {
            itemMapScript[] Others = FindObjectsOfType<itemMapScript>();
            for (int i = 0; i < Others.Length; i++)
            {
                Others[i].Deselect();
            }
            gameManager.coins -= price;
            state = 2;
            PlayerPrefs.SetInt("havingMap" + MapNumber, 1);
            PlayerPrefs.SetInt("selectedMap", MapNumber);
            stateText.text = "선택됨";
            gameManager.SelectedMap = MapNumber;
            gameManager.SaveMoney();
        }
    }

    public void Deselect()
    {
        if(state == 2)
        {
            state = 1;
            stateText.text = "선택하기";
        }
        else if (state == 1)
        {
            state = 1;
            stateText.text = "선택하기";
        }
        else if (state == 0)
        {
            state = 0;
            stateText.text = price.ToString() + "D로 구입";
        }
    }
}