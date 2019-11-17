using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BladeEffectScript : MonoBehaviour
{
    public Sprite frame2;
    public Sprite frame3;
    public Sprite frame4;

    SpriteRenderer spriteRenderer;
    float life = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
        spriteRenderer = GetComponent<SpriteRenderer>();

        gameObject.transform.SetPositionAndRotation(new Vector3(gameObject.transform.position.x + random.Next(10, 20)/8 * gameObject.transform.lossyScale.x, gameObject.transform.position.y + random.Next(-5, 10) / 3, gameObject.transform.position.z), gameObject.transform.rotation); ;
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;

        gameObject.transform.SetPositionAndRotation(gameObject.transform.position +new Vector3 (gameObject.transform.lossyScale.x * 10 * Time.deltaTime,0,0), gameObject.transform.rotation);

        if(life >= 0.375)
        {
            spriteRenderer.sprite = frame2;
        }
        else if(life >= 0.25)
        {
            spriteRenderer.sprite = frame3;
        }
        else if(life >= 0.125)
        {
            spriteRenderer.sprite = frame4;
        }
        if (life < 0)
            Destroy(this.gameObject);
    }
}
