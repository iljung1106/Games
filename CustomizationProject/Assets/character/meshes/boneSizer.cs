using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class boneSizer : MonoBehaviour
{
    public int direction = 1;
   // public CameraRotator camera;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(8, 8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSize(Slider slider)
    {
        gameObject.transform.localScale = new Vector3(1 + direction * slider.value / 500, 1 + direction * slider.value / 500, 1 + direction * slider.value / 500);
    }

    public void SeeCamera(bool enabled)
    {
        if (enabled)
        {
            transform.localRotation = Quaternion.LookRotation(new Vector3(0, 180f, 0));
        }
        else
        {
            transform.localRotation = Quaternion.LookRotation(new Vector3(0, 0, 0));
           //camera.cameraHeight = 0.5f + transform.localScale.x;
        }
    }
}
