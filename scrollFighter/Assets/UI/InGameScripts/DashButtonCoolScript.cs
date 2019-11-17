using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashButtonCoolScript : MonoBehaviour
{
    Image image;
    public CharacterController character;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();   
    }

    // Update is called once per frame
    void Update()
    {
        image.color = new Color(1, 1, 1, character.GetDashTime() * 0.8f + 0.2f);
    }
}
