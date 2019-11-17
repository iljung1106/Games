using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blendCopy : MonoBehaviour
{
    public SkinnedMeshRenderer thisMesh;
    public SkinnedMeshRenderer originalMesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 4 5 6 7 8
        for (int i = 0; i < 13; i++)
        {
            thisMesh.SetBlendShapeWeight(i, originalMesh.GetBlendShapeWeight(i));
        }
    }
}
