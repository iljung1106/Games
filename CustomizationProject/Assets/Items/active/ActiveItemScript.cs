using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemScript : MonoBehaviour
{
    public bool isActived = false;
    public float coolTime = 10f;
    float goingCool = 0f;
    public Collider getItemTrigger;
    public bool gotItem = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goingCool += Time.deltaTime;
        if(Input.GetKey(KeyCode.Mouse1) && goingCool > coolTime && gotItem)
        {
            isActived = true;
            goingCool = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CharacterController>())
        {
            gotItem = true;
        }
    }
}
