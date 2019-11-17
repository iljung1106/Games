using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MoneyTextController : MonoBehaviour
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
        MoneyText.text = clickerScript.Money + " E";
    }
}
