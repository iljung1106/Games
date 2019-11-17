using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AngelLevelUpTextScript : MonoBehaviour
{
    Text MoneyText;
    ClickerScript clickerScript;

    // Start is called before the first frame updat
    void Start()
    {
        MoneyText = gameObject.GetComponent<Text>();
        clickerScript = ClickerScript.FindObjectOfType<ClickerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = "캐릭터 레벨 업그레이드 " + (clickerScript.AngelLevel) + "레벨 / " + clickerScript.AngelLevelUpCost + "E 필요";
    }
}
