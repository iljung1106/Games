using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject room1;
    Collider collider;

    bool isFirst = true;

    bool isOverLapped = false;
    float age = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //Physics.IgnoreLayerCollision(9, 9);
    }

    // Update is called once per frame
    void Update()
    {
        collider = gameObject.GetComponent<Collider>();
        Debug.Log(collider);
        age += Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (!isFirst)
        {


        }
        else if (!isOverLapped)
        {
            float leftTime = Random.Range(0f, 0.5f);
            if (age < leftTime)
            {
                age += Time.deltaTime;
                isFirst = true;

            }
            else if (!isOverLapped && isFirst)
            {
                if (!isOverLapped)
                {
                    Instantiate(room1, this.transform.position, this.transform.rotation);
                    collider.enabled = false;
                    this.enabled = false;
                }
                else
                {

                }
                isFirst = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9 && other.gameObject.GetComponent<RoomSpawner>().age < age)
        {
            other.gameObject.GetComponent<RoomSpawner>().enabled = false;
            Instantiate(room1, this.transform.position, this.transform.rotation);
            collider.enabled = false;
            this.enabled = false;
            Destroy(collider);
        }
        else if(other.gameObject.layer == 9)
        {
        }
    }
}
