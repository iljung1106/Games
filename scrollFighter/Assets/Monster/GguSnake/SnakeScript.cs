using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Collider2D collider;
    public float Speed = 1000;
    Transform playerTransform;
    public float MoveCoolMax = 1.5f;
    float MoveCool = 10;
    public float RotatingSpeed = 1;
    public float disableTracingArea = 3;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        playerTransform = FindObjectOfType<CharacterController>().GetComponent<Transform>();


        direction = (playerTransform.position - transform.position).normalized;
        float RAD2DEG = (float)180 / (float)Mathf.PI;
        float x = direction.x;
        float y = direction.y;
        float angle = Mathf.Atan2(y, x) * RAD2DEG;
        transform.eulerAngles = new Vector3(0, 0, angle + 180);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCool += Time.deltaTime;
        if (MoveCool > MoveCoolMax)
        {
            MoveCool = 0;
            rigidbody.AddForce(-transform.right * Speed, ForceMode2D.Impulse);
        }
        rigidbody.AddForce(-transform.right * Speed / 10, ForceMode2D.Force);

    }

    private void LateUpdate()
    {
        if (Mathf.Abs((playerTransform.position - transform.position).x * (playerTransform.position - transform.position).y) > disableTracingArea * disableTracingArea)
        {
            direction = (playerTransform.position - transform.position).normalized;
            float RAD2DEG = (float)180 / Mathf.PI;
            float x = direction.x;
            float y = direction.y;
            float angle = Mathf.Atan2(y, x) * RAD2DEG;
            //transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(transform.rotation.z, angle + 180, Time.deltaTime * RotatingSpeed));
            transform.eulerAngles = new Vector3(0, 0, angle + 180);
        }

    }
}

