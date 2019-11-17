using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoScript : MonoBehaviour
{
    monster1Script monsterScript;
    public float attackCool = 3f;
    public float attackTime = 2f;
    public float attackDamage = 15f;

    float waitingTime = 0.1f;
    float attackingTime = 0f;
    float speed;
    Animator animator;

    public Collider collider;
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        monsterScript = gameObject.GetComponent<monster1Script>();
        animator = gameObject.GetComponent<Animator>();
        speed = monsterScript.MoveSpeed;
        monsterScript.MoveSpeed = 0;
        //collider = gameObject.GetComponent<Collider>();
        rigid = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(waitingTime > attackCool)
        {
            attackingTime += Time.deltaTime;
            monsterScript.MoveSpeed = speed;
            animator.CrossFade("Run", 0.2f, 0, 0);
            waitingTime = 0f;
            monsterScript.SaveDirection(true);
        }
        else if(waitingTime != 0)
        {
            waitingTime += Time.deltaTime;
        }

        if(attackingTime > attackTime)
        {
            waitingTime += Time.deltaTime;
            monsterScript.MoveSpeed = 0;
            animator.CrossFade("Idle", 0.2f, 0, 0);
            attackingTime = 0f;
            monsterScript.SaveDirection(false);
        }
        else if(attackingTime != 0)
        {
            attackingTime += Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8 && attackingTime != 0)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(monsterScript.direction * 10000 + Vector3.up * 2000, ForceMode.Impulse);
            collision.gameObject.GetComponent<CharacterController>().Damage(attackDamage);
        }
    }
}
