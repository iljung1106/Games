using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using GooglePlayGames.BasicApi;

public class GPGSRecorder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {/*
        PlayGamesPlatform.Activate();
        ConectarGoogle();*/
#if UNITY_ANDROID

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .EnableSavedGames()
            .Build();
        

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesPlatform.Activate();

        ConectarGoogle();

#elif UNITY_IOS
 
        GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
 
#endif
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConectarGoogle()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (true == success)
            {
                Debug.Log("Login");
            }
            else
            {
                Debug.Log("Login Fail !!");
            }
        });
    }

    public void ShowBoard()
    {
        Social.ShowLeaderboardUI();
    }

    public void ShowAchievement()
    {
        Social.ShowAchievementsUI();
    }
}
