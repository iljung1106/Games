using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour
{
    Collider2D collider;
    public float power = 10f;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(40, 40, 40), Time.deltaTime);
        if(transform.localScale.z >= 20)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster") && collision.GetComponent<Rigidbody2D>())
        {
            collision.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position).normalized * power * collision.GetComponent<Rigidbody2D>().mass, ForceMode2D.Impulse);
        }
    }
}
