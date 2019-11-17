using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public CharacterController character;
    public float life;
    public Rigidbody2D rigidbody;
    Collider2D collider;
    public Quaternion quaternion;
    public float firstDamage = 25f;
    public Vector2 direction;
    public GameObject SoundPlay;
    public AudioClip AttackSound;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
        life = character.magicLife;
        //rigidbody.AddForce(direction.normalized * character.magicSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
        //rigidbody.AddForce(direction.normalized * character.magicSpeed, ForceMode2D.Force);
        life -= Time.deltaTime;
        if(life < 0)
        {
            Destroy(gameObject);
        }
        //gameObject.transform.rotation = quaternion;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<monsterBase>().GiveDamage(firstDamage);
            Rigidbody2D eRigidBody = collision.GetComponent<Rigidbody2D>();
            eRigidBody.AddForce((collision.transform.position - transform.position).normalized * character.givingNuckBack, ForceMode2D.Impulse);
            eRigidBody.AddTorque((character.rigidBody.rotation - eRigidBody.rotation) * character.givingNuckBack * 10);
            AudioSource spawnedAudio = Instantiate(SoundPlay, transform.position, transform.rotation).GetComponent<AudioSource>();
            spawnedAudio.clip = AttackSound;
            spawnedAudio.Play();
            //Destroy(gameObject);
        }
    }
    
}
