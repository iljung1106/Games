using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBotController : MonoBehaviour
{
    float settingTargetTime = 0;

    CapsuleCollider2D collider;
    private GameObject[] targets = new GameObject[200];
    private GameObject selectedTarget;
    private HealthController healthCon;
    private float health = 200;
    private float distance;
    private float angle;
    private Vector3 directionVector;
    private Rigidbody2D rigid;
    float attackTime = 0;
    bool shouldAttack = false;

    private int moveD;
    Animator animator;
    AudioSource audioSource;

    public AudioClip attackSound;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        healthCon = gameObject.GetComponent<HealthController>();
        collider = gameObject.GetComponent<CapsuleCollider2D>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        settingTargetTime += Time.deltaTime;

        if(health != healthCon.BHealth)
        {
            audioSource.clip = hitSound;
            audioSource.Play();
            animator.Play("Hit");
        }
        else if (health <= 0)
        {
            animator.Play("Die");
        }
        else
        {

            health = healthCon.BHealth;
            attackTime += Time.deltaTime;

            /*
            if (rigid.velocity.x < 0)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            */
            if (settingTargetTime >= 2.5)
            {
                settingTargetTime = 0;
                targets = GameObject.FindGameObjectsWithTag("team");
                //distance = Vector2.Distance(transform.position, targets[0].transform.position);
                selectedTarget = GameObject.FindGameObjectWithTag("Player");
                //directionVector = targets[0].transform.position - transform.position;
                directionVector = selectedTarget.transform.position - transform.position;
                distance = Vector2.Distance(transform.position, selectedTarget.transform.position);
                for (int i = 1; i <= targets.Length; i++)
                {


                    if (distance >= Vector2.Distance(transform.position, targets[i - 1].transform.position))
                    {
                        selectedTarget = targets[i - 1];
                        //Debug.Log(selectedTarget.name);
                    }
                    if (i < targets.Length)
                    {
                        break;
                    }

                }
            }
            distance = Vector2.Distance(transform.position, selectedTarget.transform.position);
            directionVector = selectedTarget.transform.position - transform.position;
            directionVector.z = 0;
            directionVector.y = 0;

            directionVector = directionVector.normalized;
            //Debug.Log(directionVector);

            //Debug.Log(selectedTarget.name);

            this.transform.localScale = new Vector3(-directionVector.x, 1, 1);


            if (attackTime >= 1.5 && distance <= 7)
            {
                //Debug.Log("attack");
                //shouldAttack = true;
                if (animator.GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("Base Layer.Hit"))
                    animator.Play("Attack");
            }
            else if (distance <= 7)
            {
                shouldAttack = false;
                if (animator.GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("Base Layer.Hit"))
                    animator.Play("Idle");
            }
            else if (distance <= 40)
            {
                if(animator.GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("Base Layer.Hit") && animator.GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("Base Layer.Attack"))
                animator.Play("Run");
                shouldAttack = false;
                //Debug.Log("move");
                rigid.AddForce((directionVector * 75000 * Time.deltaTime), ForceMode2D.Force);
            }

            if (attackTime > 2.5)
            {
                attackTime = 0;
                animator.Play("Run");
            }
        }
        health = healthCon.BHealth;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (shouldAttack)
            {
                collision.gameObject.GetComponent<HealthController>().BHealth -= 15;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(directionVector.x * 10000 * Time.deltaTime, 5000 * Time.deltaTime, 0), ForceMode2D.Impulse);
                shouldAttack = false;
                audioSource.clip = attackSound;
                audioSource.Play();
            }
        }
    }

    private void die()
    {
        Destroy(this.gameObject);
    }

    private void attack()
    {
        shouldAttack = true;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
