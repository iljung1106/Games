﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollower : MonoBehaviour
{
    public Transform target;
    public float speed;
    public CharacterController character;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(character.GetHealthRatio() <= 0.3f && animator.GetCurrentAnimatorStateInfo(0).IsName("FollowerUpDown"))
        {
            animator.Play("FollowerSpeedUp");
        }
        //transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
    }
}
