using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGradation : MonoBehaviour
{
    TextMesh text;
    public Color c1;
    public Color c2;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 1)
        {
            time = 0;
        }
        else if(time > 0.5)
        {
            text.color = (Color.Lerp(text.color, c1, Time.deltaTime * 2));
        }
        else
        {
            text.color = (Color.Lerp(text.color, c2, Time.deltaTime  * 2));
        }
    }
}
