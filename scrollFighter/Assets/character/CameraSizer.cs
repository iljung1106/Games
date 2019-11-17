using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CameraSizer : MonoBehaviour
{
    public float DefaultFOV = 25;
    public Camera camera;
    public Scrollbar scrollbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, transform.localPosition.y, scrollbar.value *50 + 2.5f);
        /*
        if (Input.mouseScrollDelta.y < 0 && transform.localPosition.z < 55)
        {
            transform.localPosition = transform.localPosition + new Vector3(0, 0, Time.deltaTime * 200);
        }


        if (Input.mouseScrollDelta.y > 0 && transform.localPosition.z > 5)
        {
            transform.localPosition = transform.localPosition - new Vector3(0, 0, Time.deltaTime * 200);
            if (transform.localPosition.z < 5)
            {
                transform.localPosition =new Vector3(transform.localPosition.x, transform.localPosition.y, 9);
            }
        }
        */



    }
}
