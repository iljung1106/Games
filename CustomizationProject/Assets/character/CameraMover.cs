using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    Rigidbody rigidbody;
    Collider collider;
    bool isOverLapped = false;
    public Transform character;

    float NotOverlappedTime = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        //Debug.Log(Physics.Raycast(character.position, (character.position - transform.position), 5f));
        //Debug.Log(character.position - transform.position);
        if (Physics.Raycast(character.position, (transform.position - character.position), Vector3.Distance(transform.position, character.position), 1 << 0))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition , transform.localPosition / 3f, Time.deltaTime * 5);
            NotOverlappedTime = 0f;
            isOverLapped = true;
        }
        else
        {
            NotOverlappedTime += Time.deltaTime;
            isOverLapped = false;
        }

        if(!isOverLapped && NotOverlappedTime > 0.2f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0.48f, 1.5f, -2.5f), Time.deltaTime * 2);
        }
    }
    
}
