using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    GameManager gameManager;
    
    public bool LeftButtonClicked = false;
    public bool RightButtonClicked = false;

    public magicWandScript wand;
    public Collider2D footCollider;
    public Transform moveTransform;
    public Transform camera;
    public Camera CharacterCamera;
    Animator animator;
    public Rigidbody2D rigidBody;
    public float Speed = 800f;
    Vector3 keyDIrection = new Vector3(0, 0, 0);
    float animationTime = 0;
    Vector3 direction = new Vector3(0, 0, 0);
    float RLDirection = 0; // 1은 >   -1은 <
    public Transform armature;
    public Transform MeshTransform;
    public Transform Swordtransform;

    bool isFirstTouch = true;

    bool onGround = false;
    GetGravity gravity;
    public float DefaultMass = 4;

    float health = 400;
    bool isHitIgnored = false;
    public float maxHealth = 400;
    public Animator HitEffect;

    public GameObject DieEffect;

    public GameObject DarkEffect;

    public RectTransform HealthBarTransform;

    public Animator HealthBarAnimator;

    public float reloadingTime = 1;

    float reloading = 0f;

    public float magicLife = 3f;

    public float magicSpeed = 20f;
    float magicAngle = 0;

    public GameObject explosion;


    public float givingNuckBack = 1;

    public GameObject fireBall;

    public float Armor = 1f;

    public float attack = 1f;

    public float dashCool = 5f;

    float dashTime = 0f;
  
    int magicType = 0;

    public AudioSource hitSound;
    //public ProgressBar healthBar;
    //public RectTransform bar;
    //public RectTransform barBackground;

    bool doubleJumpUsable = true;

    Vector2 shotDirection = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponentInChildren<Animator>();
        //rigidBody = GetComponentInParent<Rigidbody2D>();
        Physics.IgnoreLayerCollision(8, 8);
        //anim = GetComponentInChildren<Animation>();
        //rigidBody.centerOfMass = new Vector2(0, -1.5f);
        transform.eulerAngles = new Vector3(0, 180, 0);
        switch (gameManager.selectedCharacter)
        {
            case 0:
                maxHealth = 500 + gameManager.healthLevel * 10;
                attack = 30 + gameManager.attackLevel * 3;
                break;

            case 1:
                maxHealth = 400 + gameManager.healthLevel * 8;
                attack = 50 + gameManager.attackLevel * 5;
                break;

        }
        health = maxHealth;
        gravity = GetComponent<GetGravity>();
    }

    private void Update()
    {
        HealthBarTransform.localScale = new Vector2(health, 1);

        if(health <= 0)
        {
            onGround = false;
            isFirstTouch = false;
            LeftButtonClicked = false;
            RightButtonClicked = false;
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    Die();
                }
            }
            else
            {
                animator.Play("Death");
                Instantiate(DieEffect, transform.position, transform.rotation);
            }
            return;

        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Slash") && (Input.GetKey(KeyCode.A) || LeftButtonClicked) || (Input.GetKey(KeyCode.D) || RightButtonClicked))
        {
            Move();
            //if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            if (isFirstTouch)
            {
                if (onGround)
                {
                    animator.CrossFade("Run", 0.05f, 0, 0);
                    //animator.Play("Run");
                }
                isFirstTouch = false;
            }
            animationTime += Time.deltaTime;

        }
        else //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
        {
            //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            if (!isFirstTouch)
            {
                if (onGround)
                {
                    //animator.Play("Idle");
                    animator.CrossFade("Idle", 0.1f, 0, 0, 0.1f);
                }
                isFirstTouch = true;
            }
            animationTime += Time.deltaTime;

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

    }


    // Update is called once per frame
    void LateUpdate()
    {
        //Debug.Log(Input.mousePosition);
        //healthBar.BarValue = Mathf.Round(health / maxHealth * 100);
        //bar.localScale = new Vector3(maxHealth / 100, bar.localScale.y, bar.localScale.z);
        //barBackground.localScale = new Vector3(maxHealth / 100, barBackground.localScale.y, barBackground.localScale.z);

        reloading += Time.deltaTime;
        if(reloading < 0 && reloading > -0.9f)
        {
            //bulletScript bullet = Instantiate(fireBall, transform.position, transform.rotation, transform).GetComponent<bulletScript>();
            bulletScript bullet = Instantiate(fireBall, Swordtransform.position, transform.rotation, transform).GetComponent<bulletScript>();
            bullet.firstDamage = attack;
            Vector3 bulletEulerAngles = bullet.transform.eulerAngles;
            bullet.transform.SetParent(transform);
            bullet.direction = shotDirection;
            bullet.character = this;
            //bullet.rigidbody.AddRelativeForce(shotDirection.normalized * magicSpeed, ForceMode2D.Impulse);
            bulletEulerAngles += new Vector3(0, 0, magicAngle);
            bullet.transform.eulerAngles = bulletEulerAngles;

            if (magicAngle == 180f)
            {
                bullet.transform.localScale = -bullet.transform.localScale;
            }
            if (magicAngle == 360f)
            {
                bullet.transform.localScale = new Vector3(-bullet.transform.localScale.x, bullet.transform.localScale.y, -bullet.transform.localScale.z);
            }
            //bullet.transform.localScale = new Vector3(RLDirection, 1, 1);

            bullet.transform.position = Swordtransform.position;
            reloading = 0;
        }
        else if(reloading > 0.2f)
        {
            SetShouldCollideMob(false);
        }
        /*
        if(reloading >= reloadingTime)
        {
            

            for(int i =0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).position.y > Screen.height / 4 && health > 0)
                {
                    reloading = -1;
                    shotDirection = (Vector2)(Input.GetTouch(i).position) - new Vector2(Screen.width, Screen.height) / 2;
                    float RAD2DEG = (float)180 / (float)Math.PI;
                    float x = shotDirection.x;
                    float y = shotDirection.y;
                    float angle = Mathf.Atan2(y, x) * RAD2DEG;
                    shotDirection = new Vector2(shotDirection.x, shotDirection.y);
                    Debug.Log(RLDirection);
                    if (shotDirection.x < 0 && RLDirection == 1)
                    {
                        shotDirection.x = 0;
                    }
                    else if (shotDirection.x > 0 && RLDirection == -1)
                    {
                        shotDirection.x = 0;
                    }
                    //rigidBody.AddForce((Vector2)(transform.right) * Speed * -0.4f * RLDirection, ForceMode2D.Force);
                    rigidBody.AddRelativeForce(shotDirection.normalized * Speed * 0.5f, ForceMode2D.Force);
                    if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && onGround)
                    {
                        animator.CrossFade("SlashOrigin", 0.1f, 0, 0.0f);
                    }
                    else if (!onGround)
                    {
                        animator.Play("HighJump");
                    }
                    animator.Play("Slash", 1);
                }
            }

            //여기부터
            if (Input.GetMouseButton(0) && Input.mousePosition.y > Screen.height / 4 && health > 0)
            {
                reloading = -1;
                shotDirection = (Vector2)(Input.mousePosition) - new Vector2(Screen.width, Screen.height) / 2; 
                float RAD2DEG = (float)180 / (float)Math.PI;
                float x = shotDirection.x;
                float y = shotDirection.y;
                float angle = Mathf.Atan2(y, x) * RAD2DEG;
                shotDirection = new Vector2(shotDirection.x, shotDirection.y);
                Debug.Log(RLDirection);
                if (shotDirection.x < 0 && RLDirection == 1)
                {
                    shotDirection.x = 0;
                }
                else if (shotDirection.x > 0 && RLDirection == -1)
                {
                    shotDirection.x = 0;
                }
                //rigidBody.AddForce((Vector2)(transform.right) * Speed * -0.4f * RLDirection, ForceMode2D.Force);
                rigidBody.AddRelativeForce(shotDirection.normalized * Speed * 0.5f, ForceMode2D.Force);
                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && onGround)
                {
                    animator.CrossFade("SlashOrigin", 0.1f, 0, 0.0f);
                }
                else if (!onGround)
                {
                    animator.Play("HighJump");
                }
                animator.Play("Slash", 1);
            }// 여기까지 테스트용
        }*/

        dashTime += Time.deltaTime;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Dash();
        }

        //rigidBody.AddForce(downForce);








        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
        }
        else
        {
            if (rigidBody.velocity.magnitude > 0.1f && !animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
            {
                //armature.LookAt(new Vector3(transform.position.x + rigidBody.velocity.x, transform.position.y), transform.up);
            }
        }

    }

    public void AddMoveRight(bool d)
    {
        RightButtonClicked = d;
    }
    public void AddMoveLeft(bool d)
    {
        LeftButtonClicked = d;
    }



    public void Move()
    {

        direction = new Vector3(0, 0, 0);
        /*
        if (Input.GetKey(KeyCode.A))
            direction += new Vector3(-1,0);
        if (Input.GetKey(KeyCode.D))
            direction += new Vector3(1,0);*/
        if (Input.GetKey(KeyCode.A) || LeftButtonClicked)
        {
            RLDirection = -1;
            direction += transform.right;
            armature.localEulerAngles = Vector3.LerpUnclamped(armature.localEulerAngles - new Vector3(0, 90, 0), new Vector3(0, 180, 0), Time.deltaTime * 3) + new Vector3(0,90,0);
        }
        if (Input.GetKey(KeyCode.D) || RightButtonClicked)
        {
            RLDirection = 1;
            direction -= transform.right;
            // armature.localEulerAngles = new Vector3(0, -90, 0);
            armature.localEulerAngles = Vector3.LerpUnclamped(armature.localEulerAngles - new Vector3(0, 90, 0), new Vector3(0, 0, 0), Time.deltaTime * 3) + new Vector3(0, 90, 0); ;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
        {
            if(onGround)
            {
                rigidBody.AddForce(Time.deltaTime * Speed * direction.normalized, ForceMode2D.Force);
            }
            else
            {
                rigidBody.AddForce(Time.deltaTime * Speed * direction.normalized * 0.5f, ForceMode2D.Force);
            }
        }

        //Debug.Log(new Vector3(camera.forward.x, camera.forward.y, 0));
    }
    

   /* public void LateUpdate()
    {


    }
    */
    public void Jump()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
        {
            if (onGround)
            {
                rigidBody.AddForce((Vector2)(transform.up) * Speed * 1f, ForceMode2D.Force);
                animator.Play("HighJump");
            }
            /*else if (doubleJumpUsable)
            {
                rigidBody.AddForce((Vector2)(transform.up) * Speed * 0.4f, ForceMode2D.Force);
                //rigidBody.AddForce(new Vector2(0, Speed * 0.4f), ForceMode2D.Force);
                //animator.CrossFade("HighJump", 0.1f, 0, 0);
                animator.Play("DoubleJump");
                doubleJumpUsable = false;
            }*/
        }
    }

    public void Dash()
    {
        //animator.CrossFade("Jump", 0.1f, 0, 0);
        if (dashTime > dashCool && health > 0)
        {
            animator.Play("Jump");
            rigidBody.AddForce(direction * Speed * 0.01f, ForceMode2D.Impulse);
            dashTime = 0;

        }
    }

    public float GetDashTime()
    {
        return dashTime/dashCool;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            onGround = true;

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Falling")) ;
            {
                //animator.CrossFade("Idle", 0.1f, 0, 0, 0f);
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || LeftButtonClicked || RightButtonClicked)
                {
                    //animator.CrossFade("Run", 0.0f, 0, 0.0f);
                    animator.Play("Run");
                }
                else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                {
                    //animator.CrossFade("Idle", 0.5f, 0, 0, 0.1f);
                    animator.Play("Idle");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            onGround = false;
            doubleJumpUsable = true;
            //animator.CrossFade("Falling", 0.5f, 0, 0.1f);
            gravity.mass = DefaultMass;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            animator.CrossFade("Falling", 0.5f, 0, 0, 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Ground"))
        {
            gravity.mass = DefaultMass / 4;
            /*
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                animator.CrossFade("Run", 0.1f, 0, 0, 0.1f);
            else
                animator.CrossFade("Idle", 0.1f, 0, 0, 0.1f);*/
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
            {
                //animator.CrossFade("Idle", 0.1f, 0, 0, 0f);
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || LeftButtonClicked || RightButtonClicked)
                    animator.CrossFade("Run", 0.5f, 0, 0.1f);
                else
                    animator.CrossFade("Idle", 0.5f, 0, 0, 0.1f);
            }

        }
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
        if (!HealthBarAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hurt") && !isHitIgnored)
        {
            hitSound.Play();
            HitEffect.Play("HitEffect");
            health -= naturalDamage / Armor;
            HealthBarAnimator.Play("Hurt");
            if (health / maxHealth <= 0.3f)
            {
                Debug.Log("health < 30% cha" + gameManager.selectedCharacter);
                switch (gameManager.selectedCharacter)
                {
                    case 0:
                        Instantiate(explosion, transform.position, transform.rotation);
                        Speed = 20000f;
                        break;

                    case 1:
                        Debug.Log("Darkunterst Abillity");
                        DarkEffect.transform.localScale = new Vector3(1, 1, 1);
                        Speed = 25000;
                        attack = 100 + gameManager.attackLevel * 10;
                        break;

                }
            }
        }
    }

    public float GetHealthRatio()
    {
        return health / maxHealth;
    }

    public void Die()
    {
        gameManager.LoadMenu();
        Destroy(gameObject);
    }

    public void SetShouldCollideMob(bool ignore)
    {
        isHitIgnored = ignore;
        Physics2D.IgnoreLayerCollision(8, 9, ignore);
        Physics2D.IgnoreLayerCollision(8, 11, ignore);
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void Fire()
    {
        wand.Fire(magicType, shotDirection.normalized);
    }

    public void AttackInput(float angle)
    {
        if (reloading >= reloadingTime && health > 0)
        {
            SetShouldCollideMob(true);
            magicAngle = angle + 90f;
            reloading = -1;
            shotDirection = RotateV2(transform.up, angle);
            //rigidBody.AddRelativeForce(shotDirection * Speed * 0.02f, ForceMode2D.Impulse);
            //rigidBody.AddForce(shotDirection * Speed * 0.02f, ForceMode2D.Impulse);
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && onGround)
            {
                animator.CrossFade("SlashOrigin", 0.1f, 0, 0.0f);
            }
            else if (!onGround)
            {
                animator.Play("HighJump");
            }
            animator.Play("Slash", 1);
        }
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
