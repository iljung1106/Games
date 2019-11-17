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

    public void Fire(int type, Vector3 direction)
    {
        switch(type)
        {
            case 0:
                bulletScript bullet = Instantiate(fireBall, transform.position, transform.rotation).GetComponent<bulletScript>();
                bullet.direction = direction;
                bullet.character = character;
                break;
                
        }
    }
}
