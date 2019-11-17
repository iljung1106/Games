using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AttackColliderScript : MonoBehaviour
{
    public static float Damage = 0;
    public bool shouldAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDamage(float SDamage)
    {
        Damage = Mathf.Round(SDamage * 100) / 100; ;
        //Debug.Log(Damage);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            if (Damage != 0 && collision.gameObject.tag == "enemies" && shouldAttack)
            {
                collision.gameObject.GetComponent<HealthController>().GiveDamage(Damage);
                Debug.Log(Damage);

                Damage = 0;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(collision.gameObject.transform.lossyScale.x * 1000, 1000, 0), ForceMode2D.Impulse);
            }
            else if (Damage != 0 && collision.gameObject.tag == "enemyColliders" && shouldAttack)
            {
                collision.gameObject.GetComponentInParent<HealthController>().GiveDamage(Damage);
                Debug.Log(Damage);

                Damage = 0;
                collision.gameObject.GetComponentInParent<Rigidbody2D>().AddForce(new Vector3(collision.gameObject.transform.lossyScale.x * 1000, 1000, 0), ForceMode2D.Impulse);
            }
        }
    }
}
