using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGravity : MonoBehaviour
{
    public Vector2 center;
    public float mass = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().centerOfMass = center;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
