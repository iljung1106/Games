using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class customizingChanger : MonoBehaviour
{
    public SkinnedMeshRenderer CharacterMesh;
    public Transform Vast;
    // Start is called before the first frame update
    void Start()
    {
       // CharacterMesh = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVast(Slider slider)
    {
        if (slider.value >= 50)
        {
            CharacterMesh.SetBlendShapeWeight(5, (slider.value - 50) * 2);
            CharacterMesh.SetBlendShapeWeight(1, 0);
        }
        else
        {
            CharacterMesh.SetBlendShapeWeight(1, (100 - (slider.value * 2)) * 10 / 13);
            CharacterMesh.SetBlendShapeWeight(5, 0);
        }
        Vast.localScale = new Vector3(1f, 0.7f + slider.value / 200f, 0.7f + slider.value / 200f);
    }

    public void ChangeEyeLine(Slider slider)
    {
        if (slider.value >= 50)
        {
            CharacterMesh.SetBlendShapeWeight(2, (slider.value - 50f) * 2);
            CharacterMesh.SetBlendShapeWeight(0, 0);
        }
        else
        {
            CharacterMesh.SetBlendShapeWeight(0, 100 - (slider.value * 2));
            CharacterMesh.SetBlendShapeWeight(2, 0);
        }
    }

    public void ChangeChin(Slider slider)
    {
        if (slider.value >= 50)
        {
            CharacterMesh.SetBlendShapeWeight(3, (slider.value - 50f) * 2);
            CharacterMesh.SetBlendShapeWeight(4, 0);
        }
        else
        {
            CharacterMesh.SetBlendShapeWeight(4, 150 - (slider.value * 3));
            CharacterMesh.SetBlendShapeWeight(3, 0);
        }
    }

    public void ChangeNoseHorizontal(Slider slider)
    {
        CharacterMesh.SetBlendShapeWeight(7, slider.value);
    }

    public void ChangeNoseVertical(Slider slider)
    {
        CharacterMesh.SetBlendShapeWeight(6, slider.value);
    }

    public void ChangeEyeSize(Slider slider)
    {
        CharacterMesh.SetBlendShapeWeight(8, 100 - slider.value);
    }



    public void OnEndButtonClicked(Canvas canvas)
    {
        canvas.enabled = false;
    }
}
