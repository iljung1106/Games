using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clothesSizeChanger : MonoBehaviour
{
    public Transform BaseBone;
    public Vector3 BaseSize;
    public GameObject cloth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Scale(BaseBone.localScale, BaseSize);
    }

    public void changeSize()
    {
        cloth.gameObject.SetActive(false);
        cloth.gameObject.SetActive(true);
    }
}
