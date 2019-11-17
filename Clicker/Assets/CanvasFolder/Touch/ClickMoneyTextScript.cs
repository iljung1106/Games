using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ClickMoneyTextScript : MonoBehaviour
{
    Text MoneyText;
    ClickerScript clickerScript;

    // Start is called before the first frame update
    void Start()
    {
        MoneyText = gameObject.GetComponent<Text>();
        clickerScript = ClickerScript.FindObjectOfType<ClickerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = ((int)((clickerScript.MoneyPerClick + clickerScript.ClickUpgrade) * clickerScript.ClickSkill) * clickerScript.CharacterLevel * clickerScript.CharacterLevel * clickerScript.AngelLevel * clickerScript.AngelLevel) + " E / 클릭";
    }
}
