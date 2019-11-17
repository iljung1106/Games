using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject wall;
    public GameObject Door;
    private Collider collider;

    bool isFirst = true;
    float age = 0;

    bool isOverLapped = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        collider = gameObject.GetComponent<Collider>();
        Debug.Log(collider);
    }

    private void LateUpdate()
    {
        if (age < 0.1f)
        {
            age += Time.deltaTime;
            isFirst = true;

        }
        else if(!isOverLapped&&isFirst)
        {
            if (!isOverLapped)
            {
                int random = Random.Range(0, 10);
                if (random <= 6)
                {
                    Instantiate(wall, this.transform.position, this.transform.rotation);
                }
                else
                {
                    Instantiate(Door, this.transform.position, this.transform.rotation);
                }
                collider.enabled = false;
                this.enabled = false;
            }
            else
            {
                
            }
            isFirst = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        isOverLapped = true;
    }
    private void OnTriggerStay(Collider other)
    {

        isOverLapped = true;
        collider.enabled = false;
        this.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {

        isOverLapped = true;
    }
}
