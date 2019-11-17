using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public Transform camera;
    public Transform shoulder;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, camera.rotation, Time.deltaTime * 10);
        Vector3 newPosition = new Vector3(Mathf.Lerp(rigidbody.position.x, shoulder.position.x, Time.deltaTime*50), Mathf.Lerp(rigidbody.position.y, shoulder.position.y, Time.deltaTime * 50), Mathf.Lerp(rigidbody.position.z, shoulder.position.z, Time.deltaTime*50));


        rigidbody.position = newPosition;
    }
}
