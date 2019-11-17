using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterBase : MonoBehaviour
{
    public Animator animator;
    Collider2D collider;
    public float Attack = 10;
    public float NuckBack = 10;
    public float Health = 100;
    public Transform HealthBar;
    public Transform HealthBackGround;
    public float Deffence = 1;
    public float LosingHealth = 0;
    public Object DieEffect;
    public Object HitEffect;
    public int KillPoint = 500;
    public bool IsBoss = false;
    public BossScript bossScript;
    Rigidbody2D rigidbody;
    bool canAttack = true;
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        HealthBackGround.localScale = new Vector3(Health / 100, HealthBackGround.localScale.y, HealthBackGround.localScale.z);
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GiveDamage(LosingHealth * Time.deltaTime, true);
        if(Health < 0)
        {
            if (IsBoss)
            {
                Instantiate(DieEffect, transform.position, transform.rotation);
                manager.AddScore(KillPoint);
                bossScript.Die();
                Destroy(this);
            }
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, -0.1f), Time.deltaTime * 4);
            if(transform.localScale.z < 0)
            {
                Instantiate(DieEffect, transform.position, transform.rotation);
                manager.AddScore(KillPoint);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canAttack)
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            CharacterController character = collision.gameObject.GetComponent<CharacterController>();
            character.Damage(Attack);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * NuckBack, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canAttack)
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            CharacterController character = collision.gameObject.GetComponent<CharacterController>();
            character.Damage(Attack);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * NuckBack, ForceMode2D.Impulse);
        }
    }

    public void GiveDamage(float damage, bool IsDOT = false)
    {
        Health -= damage / Deffence;
        HealthBar.localScale = new Vector3(Health / 100, HealthBar.localScale.y, HealthBar.localScale.z);
        if (!IsDOT)
        {
            animator.Play("Hit");
            Instantiate(HitEffect, transform.position, transform.rotation);
            Stun(1f);
        }
    }

    IEnumerator Stun(float time)
    {
        canAttack = false;
        //rigidbody.freezeRotation = true;
        //rigidbody.velocity = new Vector3(0, 0, 0);
        rigidbody.drag *= 100;
        rigidbody.mass *= 100;
        //rigidbody.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(time);
        rigidbody.drag *= 0.01f;
        rigidbody.mass *= 0.01f;
        //rigidbody.freezeRotation = false;
        canAttack = true;
        //rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
