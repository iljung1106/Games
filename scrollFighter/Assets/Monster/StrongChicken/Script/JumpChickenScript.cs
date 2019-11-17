using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpChickenScript : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Collider2D collider;
    public float JumpCoolMax = 2;
    public float JumpPower = 1000;
    public float DashCoolMax = 1;
    public float DashPower = 1000;
    float jumpCool = 0;
    Transform playerTransform;
    public SpriteRenderer sprite;
    public Sprite CrouchSprite;
    public Sprite JumpSprite;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        playerTransform = FindObjectOfType<CharacterController>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        jumpCool += Time.deltaTime;
        rigidbody.AddForce(JumpPower * direction / 10 * Time.deltaTime, ForceMode2D.Force);
        if (jumpCool > JumpCoolMax)
        {
            jumpCool = -10;
            rigidbody.AddForce(transform.up * JumpPower, ForceMode2D.Impulse);
        }
        else if(jumpCool < 0 && jumpCool > -10 + DashCoolMax)
        {
            rigidbody.AddForce(DashPower * 2 * direction, ForceMode2D.Impulse);
            jumpCool = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sprite.sprite = CrouchSprite;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        sprite.sprite = JumpSprite;
    }
}
