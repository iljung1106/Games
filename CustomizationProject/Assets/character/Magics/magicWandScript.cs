using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicWandScript : MonoBehaviour
{
    public GameObject fireBall;
    public Transform cameraTransform;
    public CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(int type)
    {
        switch(type)
        {
            case 0:
                bulletScript bullet = Instantiate(fireBall, gameObject.transform.position, cameraTransform.rotation).GetComponent<bulletScript>();
                bullet.quaternion = cameraTransform.rotation;
                bullet.character = character;
                break;
                
        }
    }
}
