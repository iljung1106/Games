using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ClickUpgradeTextScript : MonoBehaviour
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
        MoneyText.text = "클릭당 E 업그레이드 " + (clickerScript.ClickUpgrade + 1) + "레벨 / " + clickerScript.CostMoney + "E 필요";
    }
}

