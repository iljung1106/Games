using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasController1 : MonoBehaviour
{
    public static float healthText;
    public static float maxHealthText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealthToTextCon(float health, float maxHealth)
    {
        
        healthText = Mathf.Floor(health);
        maxHealthText = Mathf.Floor(maxHealth);
    }
}
