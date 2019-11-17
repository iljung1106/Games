using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    float angle;
    bool bulletColor = true;
    Rigidbody2D rigidBody;
    PlayerScript playerScript;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        playerScript = GetComponentInParent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(playerScript.GetBulletColor());
        bulletColor = playerScript.GetBulletColor();
        Vector2 direction = rigidBody.velocity.normalized;
        if(direction != new Vector2(0,0))
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        }
    }

    public bool GetBulletColor()
    {
        return bulletColor;
    }
}
