using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Anima2D;
using UnityEngine;

public class BoyController : MonoBehaviour
{
    public float maxHealth = 100f;
    protected float health = 100f;

    public float movePower = 100f;
    public float jumpPower = 300f;
    public float coolTimeA = 1f;

    public static float DefaultDamageV = 10f;

    static float groundSpeed = 1;

    public int weaponType = 0;
    static float attackCool = 0;
    static int isattacking = 0;
    static bool shouldAttack = false;
    static float attackMoveSpeed = 0;
    static float attackingTime = 0;
    static float attackingDamage = 0;

    Rigidbody2D rigid;
    BoxCollider2D footC;

    Vector3 movement;
    static bool isJumping = false;
    static int JumpButtonClick = 0;
    static int sMDirection = 0;
    static int moveDirection = 0;
    static int toMDirection = 0;
    static int giveDamage = 0;
    

    Animator animator;

    Canvas canvas;

    HealthController CharHealthController;

    AttackColliderScript attackScript;

    AudioSource audio;

    public SpriteMeshAnimation FrontArmAnimation;
    public SpriteMeshAnimation BackArmAnimation;

    public AudioClip FistAttackSound;

    public AudioClip BladeAttackSound;
    public GameObject BladeEffect;

    // Start is called before the first frame update
    void Start()
    {

        rigid = gameObject.GetComponent<Rigidbody2D>();
        footC = gameObject.GetComponent<BoxCollider2D>();
        animator = gameObject.GetComponent<Animator>();
        audio = gameObject.GetComponent<AudioSource>();
        canvas = FindObjectOfType<Canvas>();
        CharHealthController = gameObject.GetComponent<HealthController>();
        attackScript = gameObject.GetComponentInChildren<AttackColliderScript>();

        //Physics2D.IgnoreLayerCollision(13, 13, true);
        Physics2D.IgnoreLayerCollision(11, 12, true);
        Physics2D.IgnoreLayerCollision(11, 13, true);
        Physics2D.IgnoreLayerCollision(12, 13, true);
        //Physics2D.IgnoreLayerCollision(12, 12, true);
    }

    // Update is called once per frame
    void Update()
    {
        health = CharHealthController.BHealth;

        canvas.GetComponent<CanvasController1>().SetHealthToTextCon(health, maxHealth);

        //Debug.Log(health);
        if (health <= 0)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("Base Layer.Die"))
            { 
                animator.Play("Die");
            }
            else if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95)
            {
                animator.speed = 0;
            }
        }
        else
        {


            if (toMDirection == 3)
            {
                moveDirection = toMDirection;

                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95)
                {
                    //moveDirection = 0;
                    isattacking = 0;
                    shouldAttack = false;
                    moveDirection = sMDirection;
                    attackingTime = 0;
                    audio.pitch = 1f;

                }
            }
            else if (toMDirection == 0)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95 && isattacking == 1)
                {
                    moveDirection = 0;
                    isattacking = 0;
                    shouldAttack = false;
                    moveDirection = sMDirection;
                    attackingTime = 0;

                    attackScript.shouldAttack = false;
                }

            }
        
            if (shouldAttack && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95)
            {
                isattacking = 1;
                attackingTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.9)
                {
                    transform.position += Vector3.right *attackMoveSpeed * Time.deltaTime *this.transform.localScale.x;
                }
            }
            else if(shouldAttack)
            {
                isattacking = 0;
            }
            attackCool += Time.deltaTime;


            if (isattacking == 0)
            {
                animator.speed = 1;
                if (moveDirection == 1)
                {
                    sMDirection = 1;
                    rigid.AddForce(Vector3.right * movePower * groundSpeed * Time.deltaTime * 500, ForceMode2D.Force);
                    //transform.position += Vector3.right * movePower * Time.deltaTime;
                    this.transform.localScale = new Vector3(1, 1, 1);
                    if (animator.GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("Base Layer.Run"))
                    {
                        animator.Play("Run");
                    }
                }
                else if (moveDirection == -1)
                {
                    sMDirection = -1;
                    rigid.AddForce(Vector3.left * movePower * groundSpeed * Time.deltaTime * 500, ForceMode2D.Force);
                    //transform.position += Vector3.left * movePower * Time.deltaTime;
                    this.transform.localScale = new Vector3(-1, 1, 1);
                    if (animator.GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("Base Layer.Run"))
                    {
                        animator.Play("Run");
                    }
                }
                else if (moveDirection == 0)
                {
                    sMDirection = 0;
                    if (animator.GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("Base Layer.Idle"))
                    {
                        animator.Play("Idle");
                    }
                }
                else if (moveDirection == 3)
                {
                    if (isattacking == 0 || isattacking == 1)
                    {
                        switch (weaponType)
                        {
                            default:
                                break;

                                // 기본무기
                            case 0:
                                int i = (int)Random.Range(1, 3);
                                animator.speed = 1;
                                if (attackCool >= coolTimeA && animator.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.Run"))
                                {
                                    animator.Play("PunchAttack1");
                                    attackCool = 0;
                                    attackMoveSpeed = 15f;
                                    shouldAttack = true;
                                    rigid.AddForce(new Vector2(this.transform.localScale.x * jumpPower * attackMoveSpeed / 30, 0), ForceMode2D.Impulse);
                                }
                                else if (i == 1)
                                {
                                    animator.Play("PunchAttack2");
                                    //if (animator.GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("PunchAttack2"))
                                    //{
                                        attackCool = 0;
                                        attackMoveSpeed = 5f;
                                        shouldAttack = true;
                                    //}
                                }
                                break;


                                // 플라즈마블레이드
                            case 1:

                                int j = (int)Random.Range(0, 2);
                                animator.speed = 1;
                                if (attackCool >= coolTimeA && animator.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.Run"))
                                {
                                    animator.Play("PunchAttack1");
                                    attackCool = 0;
                                    attackMoveSpeed = 25f;
                                    shouldAttack = true;
                                    rigid.AddForce(new Vector2(this.transform.localScale.x * jumpPower * attackMoveSpeed / 30, 0), ForceMode2D.Impulse);
                                }
                                else if (j == 0)
                                {
                                    animator.Play("BladeAttack1");
                                    //if (animator.GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("PunchAttack2"))
                                    //{
                                    attackCool = 0;
                                    attackMoveSpeed = 10f;
                                    shouldAttack = true;
                                    //}
                                }
                                else if (j == 1)
                                {
                                    animator.Play("BladeAttack2");
                                    //if (animator.GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("PunchAttack2"))
                                    //{
                                    attackCool = 0;
                                    attackMoveSpeed = 2f;
                                    shouldAttack = true;
                                    //}
                                }

                                break;


                        }
                    }
                }



                Jump();
            }
        }
    }

    public void MoveButtonInput(int n)
    {
       // if (attackCool >= coolTimeA)
       // {
            moveDirection = n;
        sMDirection = n;
        //}
    }

    public void AttackButtonInput(int n)
    {
        toMDirection = n;
    }
    
    public void JumpButtonInput(int n)
    {
        JumpButtonClick = n;
    }
    

    public void Jump()
    {
        if (isJumping == true)
        {
            return;
        }

        if(JumpButtonClick != 1)
        {
            return;
        }
        
        //점프하고 있지 않고 점프버튼을 누르고 있다면 실행
        rigid.velocity = Vector2.zero;

        rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        

        //isJumping = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Floor")
        {
            isJumping = false;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Floor")
        {
            isJumping = false;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Floor")
        {
            isJumping = true;
        }
    }

    public void SetHealth()
    {
        //canvas.GetComponentInChildren(GUIText)
        giveHealthV(health, maxHealth);
    }

    public void giveHealthV(float healthV, float maxHealthV)
    {
        canvas.GetComponent<CanvasController1>().SetHealthToTextCon(healthV, maxHealthV);
    }

    public void GiveDamage()
    {

    }
    
    public void SetGivingDamage(float damageDefault)
    {
        FindObjectOfType<AttackColliderScript>().setDamage(damageDefault * DefaultDamageV);
        attackScript.shouldAttack = true;
        switch (weaponType) {
            case 0:
                audio.clip = FistAttackSound;
                audio.Play();
                break;
            case 1:
                audio.clip = BladeAttackSound;
                audio.Play();
                Instantiate(BladeEffect, attackScript.gameObject.transform.position, gameObject.transform.rotation).transform.localScale = this.gameObject.transform.lossyScale;
                break;
        }
    }

}
