using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBox : MonoBehaviour
{
    Collider2D collider;
    public float gravityScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<GetGravity>())
        {
            if (collision.gameObject.GetComponent<GetGravity>().enabled && collision.enabled)
            {

                Vector3 gravityUp = (collision.transform.position - transform.position).normalized;

                Rigidbody2D rigidbody;
                //Vector2 force = (transform.position - collision.transform.position) * Time.deltaTime * gravityScale / (transform.position - collision.transform.position).magnitude;
                Vector2 force = (-transform.eulerAngles) * Time.deltaTime * gravityScale;
                if (collision.gameObject.GetComponent<Rigidbody2D>() && collision.gameObject.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
                {
                    //Quaternion targetRotation = Quaternion.FromToRotation(collision.transform.up, gravityUp) * collision.transform.rotation;
                    Quaternion targetRotation = transform.rotation;
                    if (Mathf.Abs(targetRotation.eulerAngles.y) > 90)
                    {
                        targetRotation.eulerAngles = new Vector3(0, 180, targetRotation.eulerAngles.z);
                    }
                    else
                    {
                        targetRotation.eulerAngles = new Vector3(0, 0, targetRotation.eulerAngles.z);
                    }
                    rigidbody = collision.GetComponent<Rigidbody2D>();
                    rigidbody.AddForce(rigidbody.mass * force * collision.GetComponent<GetGravity>().mass);
                    collision.transform.rotation = Quaternion.Slerp(collision.transform.rotation, targetRotation, 5f * Time.deltaTime);

                }
            }
        }
    }
}
