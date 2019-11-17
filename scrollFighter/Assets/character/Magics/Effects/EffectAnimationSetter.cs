using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnimationSetter : MonoBehaviour
{
    public string ClipName;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(ClipName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
