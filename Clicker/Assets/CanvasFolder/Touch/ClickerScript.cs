using System.Collections;
using System.Collections.Generic;
using Anima2D;
using UnityEngine.EventSystems;
using UnityEngine;

public class ClickerScript : MonoBehaviour
{
    StandaloneInputModule standaloneInput;

    Vector2 pos;

    bool shouldSpawnEffect = true;

    float SavingTime = 0;

    public int Money = 0;
    public int MoneyPerClick = 1;
    public float ClickSkill = 1.0f;
    public int ClickUpgrade = 0;
    public GameObject ClickSoundObject;
    public GameObject ClickEffectObject;

    public int CharacterLevel = 1;
    public int CharacterLevelUpCost;

    public int AngelLevel = 1;
    public int AngelLevelUpCost;


    public int CostMoney;

    public Animator animator;

    public SpriteMeshAnimation ImageAnimation;


    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Money"))
        {
            Money = PlayerPrefs.GetInt("Money");
            ClickUpgrade = PlayerPrefs.GetInt("ClickUpgrade");
            CharacterLevel = PlayerPrefs.GetInt("CharacterLevel");
            CharacterLevel = PlayerPrefs.GetInt("AngelLevel");
        }
        else
        {
            PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.SetInt("ClickUpgrade", 0);
            PlayerPrefs.SetInt("CharacterLevel", 1);
            PlayerPrefs.SetInt("AngelLevel", 1);
        }

        standaloneInput = gameObject.GetComponent<StandaloneInputModule>();
    }
    /*private void Update()
    {
        if(Input.touchCount > 0)
        {
            pos = Input.GetTouch(0).position;
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        ImageAnimation.frame = AngelLevel - 1;


        SavingTime += Time.deltaTime;
        if(SavingTime >= 1)
        {
            PlayerPrefs.SetInt("Money", Money);
            PlayerPrefs.SetInt("ClickUpgrade", ClickUpgrade);
            PlayerPrefs.SetInt("CharacterLevel", CharacterLevel);
            PlayerPrefs.SetInt("AngelLevel", AngelLevel);
        }


        // 업그레이드 버튼 글씨
            // 클릭당
        CostMoney = (ClickUpgrade + 1);

        for (int i = 0; i <= ClickUpgrade; i++)
        {
            CostMoney += i;
        }

        CostMoney = (int)(CostMoney * 26.5);
        // 클릭당

        ////////////////////////////////

        // 캐릭터 레벨
        CharacterLevelUpCost = CharacterLevel * CharacterLevel * CharacterLevel * CharacterLevel * CharacterLevel * 1000;
        // 캐릭터 레벨
        // 업그레이드 버튼 글씨

        AngelLevelUpCost = AngelLevel * AngelLevel * AngelLevel * AngelLevel * AngelLevel * 100000;
        

        //if (shouldSpawnEffect)
        
        
        for (int t = 0; t < Input.touchCount; ++t)  //멀티 터치를 막아놨음. t < 1 의 뒷부분의 숫자가 최대 멀티터치 숫자임.
        {

            if (Input.GetTouch(t).phase == TouchPhase.Began)
            {
                Vector3 forP = Input.GetTouch(t).position;
                forP.z = -10;
                Vector3 p2 = Camera.main.ScreenToWorldPoint(forP);
                if (Input.GetTouch(t).position.y > Screen.height / 2)
                {
                    animator.Play("Jump");

                    Money += (int)((MoneyPerClick + ClickUpgrade) * ClickSkill) * CharacterLevel * AngelLevel * AngelLevel;
                    //Instantiate(ClickSoundObject);

                    Vector3 p = Camera.main.ScreenToWorldPoint(pos);


                    Instantiate(ClickEffectObject, new Vector3(p2.x, p2.y, 0), gameObject.transform.rotation, gameObject.transform);
                    shouldSpawnEffect = false;
                }

            }
        }

        
        
    }
   

    public void OnTouch()
    {
        shouldSpawnEffect = true;
        if (Input.touchCount > 0)
        {
            pos = Input.GetTouch(0).position;
        }
    }
    

    public void OnMouseDown()
    {
        shouldSpawnEffect = true;
        if (Input.touchCount > 0)
        {
            pos = Input.GetTouch(0).position;
        }
    }

    public void ClearAll()
    {
        CharacterLevel = 1;
        Money = 0;
        MoneyPerClick = 1;
        ClickSkill = 1.0f;
        ClickUpgrade = 0;
        AngelLevel = 1;
    }


    public void ClickMoneyUpgrade()
    {
        if(Money >= CostMoney)
        {
            Money -= CostMoney;
            ClickUpgrade++;
        }
    }


    public void CharacterLevelUpgrade()
    {
        if (Money >= CharacterLevelUpCost)
        {
            Money -= CharacterLevelUpCost;
            CharacterLevel++;
            PlayerPrefs.SetInt("CharacterLevel", CharacterLevel);
        }
    }

    public void AngelLevelUpgrade()
    {
        if (Money >= AngelLevelUpCost)
        {
            Money -= AngelLevelUpCost;
            AngelLevel++;
            PlayerPrefs.SetInt("AngelLevel", AngelLevel);
        }
    }
}
