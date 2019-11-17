using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster1Script : MonoBehaviour
{
    public float Health = 100f;
    public float Deffence = 1f;
    public float AttackArea = 25f;
    public float MoveSpeed = 10f;

    public Transform character;
    Rigidbody rigidbody;
    public Vector3 direction = new Vector3(0, 0, 0);

    bool shouldSaveDirection;
    // Start is called before the first frame update
    void Start()
    {
        character = FindObjectOfType<CharacterController>().gameObject.transform;
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Health < 0)
        {
            Die();
        }


        if (!shouldSaveDirection)
        {
            Quaternion lookRotation = Quaternion.LookRotation(character.position - transform.position, Vector3.up);

            //transform.LookAt(character, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 2);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }


        if(Vector3.Distance(character.position, transform.position) > AttackArea)
        {
            if (!shouldSaveDirection)
            {
                direction = (character.position - transform.position);
                direction.y = 0;
                direction.Normalize();
            }
            rigidbody.AddForce(direction * MoveSpeed * 10, ForceMode.Force);
            rigidbody.AddForce(Vector3.down * 4000, ForceMode.Force);
        }

    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void Damage(float damage)
    {
        Health -= damage / Deffence;
    }

    public void SaveDirection(bool b)
    {
        shouldSaveDirection = b;
    }
}
