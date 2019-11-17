using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    bool isTouchPad = false;

    Vector2 lastPosition = new Vector2(0, 0);
    Vector2 Position;
    public Transform character;


    private float x = 0.0f;
    private float y = 0.0f;

    Vector2 cursorCenter;

    public float cameraHeight = 1.5f;

    Collider collider;

    bool isOverLapped = false;

    public Transform Camera;

    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cursorCenter = Input.mousePosition; //Cursor.lockState = CursorLockMode.Locked; //커서 고정
        Vector3 angles = transform.eulerAngles;

        x = angles.y;
        y = angles.x;
    }

    // Update is called once per frame
    //void Update()
    //{   //Cursor.lockState = CursorLockMode.None;
    //}

    private void Update()
    {

        x = Input.GetAxis("Mouse X") * 1 * 1f;
        y = Input.GetAxis("Mouse Y") * 1 * 1f;


        Vector3 lerpPosition = Vector3.Lerp(gameObject.transform.position, character.position + new Vector3(0, cameraHeight, 0), Time.deltaTime * 10);
        //transform.position = lerpPosition;
        transform.position = character.position + new Vector3(0, cameraHeight, 0);

        if (Input.touchSupported)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).rawPosition.x > Screen.width / 2)
                {
                    Position = Input.GetTouch(i).position / 10;
                    if (!Input.GetTouch(i).phase.Equals(TouchPhase.Began))
                    {
                        rotateCamera(false);
                    }
                    lastPosition = Input.GetTouch(i).position / 10;
                }
            }
        }
        else if (lastPosition != cursorCenter)
        {
            Position = Input.mousePosition / 10;
            lastPosition = Input.mousePosition / 10;
            Vector3 lookingRotation = new Vector3(transform.rotation.eulerAngles.x - y, transform.rotation.eulerAngles.y + x, 0);

            transform.eulerAngles = lookingRotation;
        }

    }

    public void rotateCamera(bool shouldMove)
    {
        if (Input.touchCount > 0 || shouldMove)
        {
            Vector3 lookingRotation = new Vector3(transform.rotation.eulerAngles.x - Position.y + lastPosition.y, transform.rotation.eulerAngles.y + Position.x - lastPosition.x, 0);
            
            Debug.Log(Position);
            //transform.rotation.SetEulerAngles(lookingRotation);
            // transform.eulerAngles = lookingRotation;
           // transform.localEulerAngles = lookingRotation;
            transform.eulerAngles = lookingRotation;
            //transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x + Position.y - lastPosition.y, transform.rotation.eulerAngles.y + Position.x - lastPosition.x, 0);
        }
        //transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        //Cursor.
    }

    public void setIstouchpad(bool istouchpad)
    {
        isTouchPad = istouchpad;
    }


}
