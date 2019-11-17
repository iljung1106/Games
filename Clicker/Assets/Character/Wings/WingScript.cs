using System.Collections;
using System.Collections.Generic;
using Anima2D;
using UnityEngine;

public class WingScript : MonoBehaviour
{
    public ClickerScript clicker;
    public SpriteMeshAnimation frameAnimation;
    public SpriteMeshInstance spriteMesh;
    public Transform boneL;
    public Transform boneR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frameAnimation.frame = clicker.CharacterLevel - 1;

        double WScaleA = (float)clicker.Money / (float)clicker.CharacterLevelUpCost * 1.2 + 0.2;

        boneL.localScale = new Vector3((float)WScaleA, (float)WScaleA, 1);
        boneR.localScale = new Vector3((float)WScaleA, (float)WScaleA, 1);
    }
}
