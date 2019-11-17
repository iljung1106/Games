using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameControlScript : MonoBehaviour
{
    public GameObject boss1;

    public int HighScore = 0;

    public int score = 0;

    public int LastScore = 0;

    float SecCounter = 0;

    Canvas canvas;

    Text scoreText;
    Text HighscoreText;

    public bool Boss1Spawned = false;

    bool TimerStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("UI").GetComponent<Canvas>();
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        HighscoreText = GameObject.FindGameObjectWithTag("HighScore").GetComponent<Text>();
        if(PlayerPrefs.HasKey("highScore"))
        {
            HighScore = PlayerPrefs.GetInt("highScore");
            HighscoreText.text = HighScore.ToString();
        }
        else
        {
            PlayerPrefs.SetInt("HaveSkin1", 1);
            PlayerPrefs.SetInt("HaveSkin2", 1);
            PlayerPrefs.SetInt("HaveSkin3", 1);
            PlayerPrefs.SetInt("HaveSkin4", 1);
            PlayerPrefs.SetInt("HaveSkin5", 1);
            PlayerPrefs.SetInt("HaveSkin6", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Menu").transform.localScale == new Vector3(0, 0, 0))
        {
            scoreText.text = "Score  " + score.ToString();
            if(!TimerStarted)
            {
                TimerStarted = true;
                StartCoroutine("Timer");
            }
        }
        else
        {
            StopCoroutine("Timer");
            TimerStarted = false;
        }

        if(score % 100==0 && !Boss1Spawned)
        {
            Boss1Spawned = true;

            Instantiate(boss1);

        }
    }

    public void SetHighScore()
    {
        HighScore = score;
        HighscoreText.text = HighScore.ToString();
        PlayerPrefs.SetInt("highScore", HighScore);
    }

    IEnumerator Timer()
    {
        while(true)
        {
            score += 1;
            yield return new WaitForSeconds(1f);
        }
    }
    
}
