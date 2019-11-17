using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret1bulletScript : MonoBehaviour
{
    GameObject BoyObj;
    float time = 0f;
    BoxCollider2D collider;
    BoyController boyController;

    // Start is called before the first frame update
    void Start()
    {
        BoyObj = GameObject.Find("Boy");
        boyController = BoyObj.GetComponent<BoyController>();
          
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time>2.5f)
        {
            Destroy(this.gameObject, 0f);
        }
        else if (time > 1)
        {
            transform.localScale -= new Vector3(0, Time.deltaTime/3*2, 0);
        }

    }
   /* public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == BoyObj)
        {
            boyController.Damaged(2000f * Time.deltaTime);
        }
    }*/
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthController>().BHealth -= (50f * Time.deltaTime);
        }
    }
}
