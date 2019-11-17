using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSoundScript : MonoBehaviour
{
    float Life = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Life += Time.deltaTime;

        if(Life > 2)
        {
            Destroy(gameObject);
        }
        
    }
}
