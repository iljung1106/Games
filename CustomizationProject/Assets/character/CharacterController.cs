using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public magicWandScript wand;
    public Collider footCollider;
    public Transform moveTransform;
    public Transform camera;
    Animator animator;
    Rigidbody rigidBody;
    float Speed = 400000f;
    Vector3 keyDIrection = new Vector3(0, 0, 0);
    float animationTime = 0;
    public Joystick joystick;
    Vector3 direction = new Vector3(0, 0, 0);

    bool isFirstTouch = true;

    bool onGround = false;

    float health = 100;

    public float maxHealth = 100;

    public float reloadingTime = 1;

    float reloading = 0f;

    public float magicLife = 3f;

    public float magicSpeed = 20f;

    public float Armor = 1f;

    public float attack = 1f;

    public float dashCool = 5f;

    float dashTime = 0f;

    Vector3 downForce = new Vector3(0, -8000, 0);

    int magicType = 0;

    public ProgressBar healthBar;
    public RectTransform bar;
    public RectTransform barBackground;

    bool doubleJumpUsable = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(8, 8);
        //anim = GetComponentInChildren<Animation>();
        
    }


    // Update is called once per frame
    void LateUpdate()
    {
        healthBar.BarValue = Mathf.Round(health / maxHealth * 100);
        bar.localScale = new Vector3(maxHealth / 100, bar.localScale.y, bar.localScale.z);
        barBackground.localScale = new Vector3(maxHealth / 100, barBackground.localScale.y, barBackground.localScale.z);

        reloading += Time.deltaTime;
        if(reloading >= reloadingTime)
        {
            if (Input.GetMouseButton(0))
            {
                wand.Fire(magicType);
                reloading = 0;
                animator.CrossFade("Slash", 0.1f, 0, 0);
            }
        }

        dashTime += Time.deltaTime;
        if(Input.GetKey(KeyCode.LeftShift) && dashTime > dashCool)
        {
            Dash();
            dashTime = 0;
        }

        //rigidBody.AddForce(downForce);


        if (Input.touchSupported)
        {
            if (joystick.Direction != new Vector2(0, 0))
            {
                MobileMove();
                if(isFirstTouch)
                {
                    animator.CrossFade("Run", 0.2f, 0, 0);
                    isFirstTouch = false;
                }

            }
            else
            {
                if (!isFirstTouch)
                {
                    animator.CrossFade("Idle", 1f, 0, 0, 0.5f);
                    isFirstTouch = true;
                }
            }
        }




        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Move();
            //if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            if(isFirstTouch)
            {
                if(onGround)
                    animator.CrossFade("Run", 0.1f, 0, 0);
                isFirstTouch = false;
            }
            animationTime += Time.deltaTime;
            
        }
        else
        {
                //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            if (!isFirstTouch)
            {
                if (onGround)
                    animator.CrossFade("Idle", 0.1f, 0, 0, 0.1f);
                isFirstTouch = true;
            }
            animationTime += Time.deltaTime;
            
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }





        if (rigidBody.velocity.magnitude > 1.5)
        {
            transform.LookAt(new Vector3(transform.position.x + rigidBody.velocity.x, transform.position.y, transform.position.z + rigidBody.velocity.z), transform.up);
        }
    }


    public void Move()
    {
        direction = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
            direction += new Vector3(camera.forward.x, 0, camera.forward.z);
        
        if (Input.GetKey(KeyCode.A))
            direction += new Vector3(-camera.right.x, 0, -camera.right.z);

        if (Input.GetKey(KeyCode.S))
            direction += new Vector3(-camera.forward.x, 0, -camera.forward.z);

        if (Input.GetKey(KeyCode.D))
            direction += new Vector3(camera.right.x, 0, camera.right.z);

        if(onGround)
        rigidBody.AddForce(Time.deltaTime * Speed * direction.normalized, ForceMode.Force);

        //Debug.Log(new Vector3(camera.forward.x, camera.forward.y, 0));
    }

    public void MobileMove()
    {
        rigidBody.AddForce(Time.deltaTime * Speed * (new Vector3(camera.forward.x * joystick.Vertical, 0, camera.forward.z * joystick.Vertical) + new Vector3(camera.right.x * joystick.Horizontal, 0, camera.right.z * joystick.Horizontal)).normalized, ForceMode.Force);
    }

   /* public void LateUpdate()
    {


    }
    */
    public void Jump()
    {
        if (onGround)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                rigidBody.AddForce(0, Speed * 0.4f, 0, ForceMode.Force);
                rigidBody.AddForce(transform.forward * Speed * 0.3f, ForceMode.Force);
                animator.CrossFade("Jump", 0.1f, 0, 0);
            }
            else
            {
                rigidBody.AddForce(0, Speed * 0.4f, 0, ForceMode.Force);
                animator.Play("HighJump");
            }
        }
    }

    public void Dash()
    {
        rigidBody.AddForce(direction * Speed * 0.03f, ForceMode.Impulse);
        animator.CrossFade("Jump", 0.1f, 0, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        onGround = true;
        downForce = new Vector3(0, 400, 0);
    }

    private void OnTriggerExit(Collider other)
    {
        onGround = false;
        downForce = new Vector3(0, -8000, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            animator.CrossFade("Run", 0.1f, 0, 0, 0.1f);
        else
            animator.CrossFade("Idle", 0.1f, 0, 0, 0.1f);
    }

    public void SetMagicType(int type)
    {
        magicType = type;
    }
    
    public void AddReloadingSpeed(float extra)
    {
        if (reloadingTime - extra >= 0.1f)
            reloadingTime -= extra;
        else
            reloadingTime = 0.1f;
    }

    public void AddMagicSpeed(float extra)
    {
        magicSpeed += extra;
    }

    public void Damage(float naturalDamage)
    {
        health -= naturalDamage / Armor;
    }
}
