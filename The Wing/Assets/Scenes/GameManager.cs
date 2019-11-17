using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text TimeText;
    public Text BestTimeText;
    public Camera MenuCamera;
    public GameObject StartUI;
    int TimeCounter = 0;
    int BestTime;
    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        BestTime = PlayerPrefs.GetInt("BestTime", 99999);
    }

    // Update is called once per frame
    void Update()
    {
        TimeText.text = (TimeCounter / 60).ToString() + ":" + (TimeCounter % 60).ToString();
        BestTimeText.text = "BestTime : " + (BestTime / 60).ToString() + ":" + (BestTime % 60).ToString();
    }

    public void LoadGameScene(int sceneNumber)
    {
        if(sceneNumber == 0)
        {
            StartUI.SetActive(true);
            //MenuCamera.enabled = true;
            //메뉴씬
            StopCoroutine("StopWatch");
            if(TimeCounter < BestTime)
            {
                BestTime = TimeCounter;
                Social.ReportScore(BestTime * 1000, TheWIng.RecordClass.leaderboard_clear_time, success => { Debug.Log("report_sore"); });
            }
            TimeCounter = 0;
            //SceneManager.LoadScene("Scenes/StartScene");
            
        }
        else
        {
            StartUI.SetActive(false);
            StartCoroutine("StopWatch");
            //SceneManager.LoadScene("Scenes/FirstScene");
            //MenuCamera.enabled = false;
        }
        SceneManager.LoadScene(sceneNumber);
    }

    IEnumerator StopWatch()
    {
        while(true)
        {
            TimeCounter += 1;
            yield return new WaitForSeconds(1f);
        }
    }
}
