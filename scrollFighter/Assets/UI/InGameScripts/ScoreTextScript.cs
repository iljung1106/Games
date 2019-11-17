using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour
{
    GameManager gameManager;
    public Text text;
    public string addingText = "";
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        text.text = addingText + gameManager.GetScore().ToString("D7");
    }
}
