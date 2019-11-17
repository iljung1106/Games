using System.Collections;
using System.Collections.Generic;
using Anima2D;
using UnityEngine;

public class WeaponItemScirpt : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    int savingType;

    float getItemCool = 0;

    bool ItemChanged = false;

    public Sprite weapon0;
    public Sprite weapon1;
    public Sprite weapon2;
    public Sprite weapon3;
    public Sprite weapon4;
    public Sprite weapon5;

    BoxCollider2D collider;
    public int ItemType;
    float movingTime = 0;
    Vector3 movingDirection = new Vector3(0, 1, 0);


    TextMesh textMesh;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<BoxCollider2D>();
        textMesh = gameObject.GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemChanged)
        {
            getItemCool += Time.deltaTime;
        }

        if(getItemCool >= 5)
        {
            ItemChanged = false;
            getItemCool = 0;
        }

        switch (ItemType)
        {
            case 0:
                spriteRenderer.sprite = weapon0;
                textMesh.text = "Normal Arms";
                break;

            case 1:
                spriteRenderer.sprite = weapon1;
                textMesh.text = "Energy Blades";
                break;


        }

        gameObject.transform.SetPositionAndRotation(gameObject.transform.position + movingDirection * Time.deltaTime, gameObject.transform.rotation);

        if (movingDirection == new Vector3(0, 1, 0))
        {
            movingTime += Time.deltaTime;
        }
        else
        {
            movingTime -= Time.deltaTime;
        }

        if(movingTime > 1)
        {
            movingDirection = new Vector3(0, -1, 0);
        }
        if(movingTime < -1)
        {
            movingDirection = new Vector3(0, 1, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !ItemChanged)
        {

            savingType = collision.gameObject.GetComponent<BoyController>().weaponType;

            collision.gameObject.GetComponent<BoyController>().FrontArmAnimation.frame = ItemType + 3;
            collision.gameObject.GetComponent<BoyController>().BackArmAnimation.frame = ItemType + 3;
            collision.gameObject.GetComponent<BoyController>().weaponType = ItemType;

            ItemType = savingType;
            ItemChanged = true;
        }
    }
}
