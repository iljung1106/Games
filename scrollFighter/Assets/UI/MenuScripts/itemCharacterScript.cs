using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCharacterScript : MonoBehaviour
{
    public int price;
    public Text stateText;
    GameManager gameManager;
    public int CharacterNumber;
    int state = 0;
    public GameObject characterModel;
    GameObject spawnedCharacter;
    public Transform spawnTransform;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        state = gameManager.havingCharacter[CharacterNumber];
        if(state == 1)
        {
            state = 1;
            stateText.text = "선택하기";
        }
        else if (state == 0)
        {
            state = 0;
            stateText.text = price.ToString() + "D로 구입";
        }
        if (gameManager.selectedCharacter == CharacterNumber)
        {
            state = 2;
            stateText.text = "선택됨";
            spawnedCharacter = Instantiate(characterModel, spawnTransform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        if(state == 1)
        {
            itemCharacterScript[] Others = FindObjectsOfType<itemCharacterScript>();
            for (int i = 0; i < Others.Length; i++)
            {
                Others[i].Deselect();
            }
            Destroy(spawnedCharacter);
            state = 2;
            gameManager.selectedCharacter = CharacterNumber;
            stateText.text = "선택됨";
            Destroy(spawnedCharacter);
            spawnedCharacter = Instantiate(characterModel, spawnTransform);
        }
        else if (gameManager.coins >= price && state == 0)
        {
            itemCharacterScript[] Others = FindObjectsOfType<itemCharacterScript>();
            for (int i = 0; i < Others.Length; i++)
            {
                Others[i].Deselect();
            }
            gameManager.coins -= price;
            state = 2;
            gameManager.havingCharacter[CharacterNumber] = 1;
            gameManager.selectedCharacter = CharacterNumber;
            stateText.text = "선택됨";
            gameManager.SaveMoney();
            Destroy(spawnedCharacter);
            spawnedCharacter = Instantiate(characterModel, spawnTransform);
        }
        gameManager.SaveOverlapXml();
    }

    public void Deselect()
    {
        Destroy(spawnedCharacter);
        if (gameManager.havingCharacter[CharacterNumber] == 1)
        {
            state = 1;
            stateText.text = "선택하기";
        }
        else if (gameManager.havingCharacter[CharacterNumber] == 0)
        {
            state = 0;
            stateText.text = price.ToString() + "D로 구입";
        }
    }
}
