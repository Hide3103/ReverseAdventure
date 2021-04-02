using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopYesNo : MonoBehaviour
{

    public GameObject shopManager;
    ShopManager shopManagerScript;

    public GameObject BuyPanel;

    public int ThisYesNoNum = 0;
    
    public Text ProductPriceText;
    
    void Start()
    {
        shopManagerScript = shopManager.GetComponent<ShopManager>();

    }
    
    void Update()
    {
        if (BuyPanel.gameObject.activeSelf == true)
        {
            if (ThisYesNoNum == shopManagerScript.SelectingYesNoNum)
            {
                float level = Mathf.Abs(Mathf.Sin(Time.time * 1.0f));
                gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, level);
            }
            else
            {
                gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
