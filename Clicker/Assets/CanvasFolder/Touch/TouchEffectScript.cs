using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEffectScript : MonoBehaviour
{
    float age;
    ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        particle = gameObject.GetComponentInChildren<ParticleSystem>();
        age = 0;
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        if(age > 0.6)
        {
            Destroy(gameObject);
        }
    }
}
