using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum ProductNumber
{
    ReverseTimeUp,
    shield,
    ToStageSelect
}

public class ShopProducts : MonoBehaviour
{

    public GameObject shopManager;
    ShopManager shopManagerScript;

    public static int ReverseTimeAmount;
    public int ThisProductNum = 0;

    public GameObject BuyPanel;

    //public GameObject ProductPriceText;
    public Text ProductPriceText;

    // Start is called before the first frame update
    void Start()
    {
        shopManagerScript = shopManager.GetComponent<ShopManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (BuyPanel.gameObject.activeSelf == false)
        {
            if (ThisProductNum == shopManagerScript.SelectingProductNum)
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

        if (ThisProductNum != (int)ProductNumber.ToStageSelect)
        {
            ProductPriceText.text = "" + shopManagerScript.GetProductPrice(ThisProductNum);
        }

    }

    //public void OnClick()
    //{
    //    switch (ThisProductNum)
    //    {
    //        case (int)ProductNumber.ReverseTimeUp:
    //            shopManagerScript.SelectingProductNum = (int)ProductNumber.ReverseTimeUp;
    //            break;
    //        case (int)ProductNumber.shield:
    //            shopManagerScript.SelectingProductNum = (int)ProductNumber.shield;
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //public void OnSelect()
    //{
    //    switch (shopManagerScript.SelectingProductNum)
    //    {
    //        case (int)ProductNumber.ReverseTimeUp:
    //            shopManagerScript.productNameText.text = "リバース時間延長";
    //            break;
    //        case (int)ProductNumber.shield:
    //            break;
    //    }
    //}


}
