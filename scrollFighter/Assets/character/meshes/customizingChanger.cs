using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class customizingChanger : MonoBehaviour
{
    public SkinnedMeshRenderer CharacterMesh;
    public Transform Vast;
    public int DefaultUpperClosed = 0;
    public int DefaultDownClosed = 0;
    float BlinkTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        // CharacterMesh = GetComponent<SkinnedMeshRenderer>();
        //CloseEyeWait();
    }

    // Update is called once per frame
    void Update()
    {
        BlinkTime += Time.deltaTime;
        if(BlinkTime > 3.5f)
        {
            BlinkTime = 0;
            CharacterMesh.SetBlendShapeWeight(7, DefaultUpperClosed);
            CharacterMesh.SetBlendShapeWeight(8, DefaultDownClosed);
        }
        else if(BlinkTime > 3.3f)
        {
            CharacterMesh.SetBlendShapeWeight(7, CharacterMesh.GetBlendShapeWeight(7) - Time.deltaTime * 500);
            CharacterMesh.SetBlendShapeWeight(8, CharacterMesh.GetBlendShapeWeight(8) - Time.deltaTime * 500);
        }
        else if(BlinkTime > 3.1f)
        {
            CharacterMesh.SetBlendShapeWeight(7, CharacterMesh.GetBlendShapeWeight(7) + Time.deltaTime * 500);
            CharacterMesh.SetBlendShapeWeight(8, CharacterMesh.GetBlendShapeWeight(8) + Time.deltaTime * 500);
        }
    }

   /* IEnumerator CloseEyeWait()
    {
        CharacterMesh.SetBlendShapeWeight(7, DefaultUpperClosed);
        CharacterMesh.SetBlendShapeWeight(8, DefaultDownClosed);
        CloseEye();
        yield return new WaitForSeconds(2.5f);
    }

    IEnumerator CloseEye()
    {
        for(int i = DefaultUpperClosed; i <= 100; i += 5)
        {
            CharacterMesh.SetBlendShapeWeight(7, i);
            CharacterMesh.SetBlendShapeWeight(8, i);
            yield return new WaitForSeconds(0.01f);
        }
        OpenEye();
    }

    IEnumerator OpenEye()
    {
        for (int i = 100; i >= DefaultUpperClosed; i -= 5)
        {
            CharacterMesh.SetBlendShapeWeight(7, i);
            CharacterMesh.SetBlendShapeWeight(8, i);
            yield return new WaitForSeconds(0.01f);
        }
        CloseEyeWait();
    }*/


    public void ChangeVast(Slider slider)
    {
        if (slider.value >= 50)
        {
            CharacterMesh.SetBlendShapeWeight(3, (slider.value - 50) * 2);
            CharacterMesh.SetBlendShapeWeight(2, 0);
        }
        else
        {
            CharacterMesh.SetBlendShapeWeight(2, (100 - (slider.value * 2)) * 10 / 13);
            CharacterMesh.SetBlendShapeWeight(3, 0);
        }
        Vast.localScale = new Vector3(1f, 0.7f + slider.value / 200f, 0.7f + slider.value / 200f);
    }

    public void ChangeEyeLine(Slider slider)
    {
        if (slider.value >= 50)
        {
            CharacterMesh.SetBlendShapeWeight(5, (slider.value - 50f) * 2);
            CharacterMesh.SetBlendShapeWeight(4, 0);
        }
        else
        {
            CharacterMesh.SetBlendShapeWeight(4, 100 - (slider.value * 2));
            CharacterMesh.SetBlendShapeWeight(5, 0);
        }
    }

    public void ChangeChin(Slider slider)
    {
        if (slider.value >= 50)
        {
            CharacterMesh.SetBlendShapeWeight(1, (slider.value - 50f) * 2);
            CharacterMesh.SetBlendShapeWeight(0, 0);
        }
        else
        {
            CharacterMesh.SetBlendShapeWeight(0, 150 - (slider.value * 3));
            CharacterMesh.SetBlendShapeWeight(1, 0);
        }
    }

    public void ChangeNoseHorizontal(Slider slider)
    {
        CharacterMesh.SetBlendShapeWeight(9, slider.value);
    }

    public void ChangeNoseVertical(Slider slider)
    {
        CharacterMesh.SetBlendShapeWeight(10, slider.value);
    }

    public void ChangeEyeSize(Slider slider)
    {
        CharacterMesh.SetBlendShapeWeight(6, 100 - slider.value);
    }



    public void OnEndButtonClicked(Canvas canvas)
    {
        canvas.enabled = false;
    }
}
