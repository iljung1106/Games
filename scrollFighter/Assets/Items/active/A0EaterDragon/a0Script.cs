using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a0Script : MonoBehaviour
{
    ActiveItemScript activeItem;
    Collider collider;
    CharacterController character;
    Animator animator;
    public Transform thisTransform;
    Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        activeItem = gameObject.GetComponent < ActiveItemScript > ();
        collider = gameObject.GetComponent<Collider>();
        character = FindObjectOfType<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
        camera = FindObjectOfType<CameraMover>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeItem.isActived)
        {
            thisTransform.position = character.gameObject.transform.position + new Vector3(0, -3, 0);
            thisTransform.eulerAngles = new Vector3(0, camera.eulerAngles.y, 0);
            //thisTransform.rotation = character.gameObject.transform.rotation;
            activeItem.isActived = false;
            animator.Play("DragonHeadEat");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<monster1Script>())
        {
            other.gameObject.GetComponent<monster1Script>().Damage(character.attack * 100 * Time.deltaTime);
        }
    }
}
