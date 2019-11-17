using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunMonsterScript : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Collider2D collider;
    public float Speed = 10f;
    Transform playerTransform;
    public SpriteRenderer sprite;
    float old = 0;
    Vector2 direction;
    float RAD2DEG = (float)180 / (float)Mathf.PI;
    float angle = 0;
    float age = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        playerTransform = FindObjectOfType<CharacterController>().GetComponent<Transform>();
        StartCoroutine(CheckTargetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if (angle < 0)
        {
            Move(Speed, transform.right, Time.deltaTime);
        }
        else
        {
            Move(Speed, -transform.right, Time.deltaTime);
        }
    }

    IEnumerator CheckTargetPosition()
    {
        while (true)
        {
            direction = playerTransform.position - transform.position;
            float x = RotateV2(direction, transform.eulerAngles.z).x;
            float y = RotateV2(direction, transform.eulerAngles.z).y;
            angle = Mathf.Atan2(y, x) * RAD2DEG;
            angle += 90;
            if (angle > 180)
            {
                angle -= 360;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Move(float speed, Vector2 localDirection, float deltaTime)
    {
        rigidbody.AddForce(localDirection * speed * deltaTime, ForceMode2D.Force);
        //transform.eulerAngles = new Vector3(0, 180, transform.eulerAngles.z);
       
    }
    

    public Vector2 RotateV2(Vector2 v, float degrees)
    {
        float sin2 = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos2 = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos2 * tx) - (sin2 * ty);
        v.y = (sin2 * tx) + (cos2 * ty);
        return v;
    }

}
