using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterUpgradeTextScript : MonoBehaviour
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
        MoneyText.text = "날개 레벨 업그레이드 " + (clickerScript.CharacterLevel) + "레벨 / " + clickerScript.CharacterLevelUpCost + "E 필요";
    }
}
