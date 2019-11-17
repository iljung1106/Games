using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneFollower : MonoBehaviour
{
    public Transform BaseBone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = BaseBone.localPosition * 1.1f;
        transform.localRotation = BaseBone.localRotation;
    }
}
