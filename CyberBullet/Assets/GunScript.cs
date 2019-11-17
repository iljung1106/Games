using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bulletType1;
    public GameObject bulletType0;
    float shootTime = 0;
    public float shootCool = 0.2f;
    ImageController iController;
    bool BulletType = true;

    SpriteRenderer spriteRenderer;

    public Sprite sprite0;
    public Sprite sprite1;

    // Start is called before the first frame update
    void Start()
    {
        iController = GetComponentInParent<ImageController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        BulletType = iController.GetBulletColor();
        shootTime += Time.deltaTime;
        if(shootTime > shootCool)
        {
            if(BulletType)
            {
                Instantiate(bulletType1, gameObject.transform.position, gameObject.transform.rotation);
                spriteRenderer.sprite = sprite0;

            }
            else
            {
                Instantiate(bulletType0, gameObject.transform.position, gameObject.transform.rotation);
                spriteRenderer.sprite = sprite1;

            }
            shootTime = 0;
        }
    }
}
