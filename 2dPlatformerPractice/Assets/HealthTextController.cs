using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthTextController : MonoBehaviour
{
    GameObject BoyObj;
    BoyController boyController;
    Text HealthLabel;
    float usingHealth;
    float usingMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        BoyObj = GameObject.Find("Boy");
        boyController = BoyObj.GetComponent<BoyController>();
        HealthLabel = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        usingHealth = CanvasController1.healthText;
        usingMaxHealth = CanvasController1.maxHealthText;

        setHealthText(usingHealth, usingMaxHealth);
    }

    public void setHealthText(float Health, float MaxHealth)
    {
        HealthLabel.text = Health + "/" + MaxHealth;
    }
}

