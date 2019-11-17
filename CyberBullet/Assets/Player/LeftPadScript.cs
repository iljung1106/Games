using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LeftPadScript : MonoBehaviour
{
    public Vector2 Axis = new Vector2(0, 0);

    public Vector2 calAxis;

    Touch touch;
    InputField InputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length > 0 || Input.GetMouseButton(0))
        {
            if (Input.touches.Length > 0)
            {
                touch = Input.GetTouch(0);
                calAxis = new Vector2(touch.position.x - Screen.width / 2, touch.position.y - Screen.height / 2);
                //Debug.Log(calAxis);
                if (calAxis.x > -590.0f / 1280 * Screen.width && calAxis.x < 290 / 1280 * Screen.width && calAxis.y > -310.0f / 720 * Screen.height && calAxis.y < -10 / 720 * Screen.height)
                {
                    Axis = new Vector2((touch.position.x + (440.0f / 1280) * Screen.width) / Screen.width, (touch.position.y + (160.0f / 720 * Screen.height)) / Screen.height);

                    Axis -= new Vector2(0.5f, 0.5f);

                }
                else
                {
                    Axis = new Vector2(0, 0);
                }
            }
            else
            {
                Vector2 clickPosition = Input.mousePosition;
                calAxis = new Vector2(clickPosition.x - Screen.width / 2, clickPosition.y - Screen.height / 2);
                //Debug.Log(calAxis);
                if (calAxis.x > -590.0f / 1280 * Screen.width && calAxis.x < 290 / 1280 * Screen.width && calAxis.y > -310.0f / 720 * Screen.height && calAxis.y < -10 / 720 * Screen.height)
                {
                    Axis = new Vector2((clickPosition.x + (440.0f / 1280) * Screen.width) / Screen.width, (clickPosition.y + (160.0f / 720 * Screen.height)) / Screen.height);

                    Axis -= new Vector2(0.5f, 0.5f);

                }
                else
                {
                    Axis = new Vector2(0, 0);
                }
            }
        }
        else
        {
            Axis = new Vector2(0, 0);
        }

    }

    private void LateUpdate()
    {
    }
}
