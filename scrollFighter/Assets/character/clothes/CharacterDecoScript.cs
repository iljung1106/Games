using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDecoScript : MonoBehaviour
{
    public int characterType = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<GameManager>().selectedCharacter != characterType)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
