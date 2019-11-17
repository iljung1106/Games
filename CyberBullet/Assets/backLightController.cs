using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backLightController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float colorTime = 0;

    public Color c1;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        colorTime += Time.deltaTime;

        if(colorTime < 7)
        {

        }
    }
}
