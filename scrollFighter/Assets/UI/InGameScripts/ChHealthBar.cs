using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChHealthBar : MonoBehaviour
{
    public bool shouldIgnoreMob = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        SetShouldCollideMob(shouldIgnoreMob);
    }
    public void SetShouldCollideMob(bool ignore)
    {
        Physics2D.IgnoreLayerCollision(8, 9, ignore);
        Physics2D.IgnoreLayerCollision(8, 11, ignore);
    }
}
