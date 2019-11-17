using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemUpgradeScript : MonoBehaviour
{
    public int price;
    public Text stateText;
    GameManager gameManager;
    public int itemNumber; // 0 = 체력 , 1 = 공격력
    int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        switch(itemNumber)
        {
            case 0:
                level = gameManager.healthLevel;
                break;
            case 1:
                level = gameManager.attackLevel;
                break;
        }
        price = level * 500;
        stateText.text = "현재 " + level + "레벨\n" + price.ToString() + "D로 레벨업";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Buy()
    {
        if (gameManager.coins >= price)
        {
            gameManager.coins -= price;
            level += 1;
            switch (itemNumber)
            {
                case 0:
                    gameManager.attackLevel = level;
                    PlayerPrefs.SetInt("healthLevel", level);
                    break;
                case 1:
                    gameManager.healthLevel = level;
                    PlayerPrefs.SetInt("attackLevel", level);
                    break;
            }
            price = level * 500;
            stateText.text = "현재 " + level + "레벨\n" + price.ToString() + "D로 레벨업";
            //gameManager.SaveMoney();
            gameManager.SaveOverlapXml();
        }
    }
    
}
