using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeByHealth : MonoBehaviour
{
    public Material ChangeMaterial;
    CharacterController character;
    public float changePercent = 0.3f;
    public Renderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        character = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(character.GetHealthRatio() <= 0.3f)
        {
            mesh.material = ChangeMaterial;
        }
    }
}
