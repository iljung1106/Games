using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothGravity : MonoBehaviour
{
    public Cloth cloth;
    public float DownForce = 2;
    public float BackForce = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cloth.externalAcceleration = -(transform.up * DownForce + transform.forward * BackForce);
    }
}
