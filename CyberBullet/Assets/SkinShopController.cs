using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SkinShopController : MonoBehaviour
{
    public Canvas UICanvas;

    public Canvas MenuCanvas;

    public Transform OpenButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenShop()
    {
        UICanvas.enabled = false;
        MenuCanvas.enabled = true;
        OpenButton.localScale = new Vector2(0, 0);
    }

    public void CloseShop()
    {
        UICanvas.enabled = true;
        MenuCanvas.enabled = false;
        OpenButton.localScale = new Vector2(1, 1);
    }

    public void SetSkin(int skinN)
    {
        UICanvas.GetComponentInChildren<StartButton>().SetPlayerSkin(skinN);
    }
}
