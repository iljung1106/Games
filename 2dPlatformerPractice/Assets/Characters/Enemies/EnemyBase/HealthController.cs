using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float BMaxHealth;
    public float BHealth;
    // Start is called before the first frame update
    void Start()
    {
        BHealth = BMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GiveDamage(float BDamage)
    {
        BHealth -= BDamage;
    }

}
