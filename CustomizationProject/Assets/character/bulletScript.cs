using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public CharacterController character;
    public float life;
    Rigidbody rigidbody;
    Collider collider;
    public Quaternion quaternion;
    public float firstDamage = 25f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        collider = gameObject.GetComponent<SphereCollider>();
        rigidbody.AddForce(transform.forward * character.magicSpeed, ForceMode.Impulse);
        life = character.magicLife;
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if(life < 0)
        {
            Destroy(gameObject);
        }
        //gameObject.transform.rotation = quaternion;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<monster1Script>().Damage(firstDamage);
        }
        Destroy(gameObject);
    }
}
