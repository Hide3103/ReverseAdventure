using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static int[] productAmount = new int[] {5, 1};
    public static int[] productPrice = new int[] {5, 2};
    public float nowJuwelNum = GameSystem.HavingNumJuwel;
    public GameObject HavingJuwel;
    Text HavingJuwelText;

    public int SelectingProductNum = 0;
    public int MaxButtonNum = 2;

    public int SelectingYesNoNum = 0;
    public int MaxYesNoButtonNum = 1;

    // 購入を決定する際に表示するパネル
    public GameObject BuyPanel;
    public Text BuyProductName;
    public Text BuyText;

    // 宝石が足りなかった際に表示するパネル
    public GameObject CantBuyPanel;
    public Text CantBuyProductName;
    public Text CantBuyText;

    //画面右側に商品の説明を表示するパネル
    public GameObject productName;
    public Text productNameText;
    public GameObject productSentence;
    public Text productSentenceText;
    public GameObject panelProductPrice;
    public Text panelProductPriceText;

    Button button1;
    Button button2;

    public bool Test;
    
    void Start()
    {
        HavingJuwelText = HavingJuwel.GetComponent<Text>();

        productNameText = productName.GetComponent<Text>();
        productSentenceText = productSentence.GetComponent<Text>();
        panelProductPriceText = panelProductPrice.GetComponent<Text>();

        button1 = productName.GetComponent<Button>();
        button2 = productSentence.GetComponent<Button>();
    }
    
    void Update()
    {
        // 購入パネルの有無
        // 購入パネルが出ていない場合
        if (BuyPanel.gameObject.activeSelf == false && CantBuyPanel.gameObject.activeSelf == false)
        {
            // 選択項目の変更
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (SelectingProductNum < MaxButtonNum)
                {
                    SelectingProductNum += 1;
                }
                else
                {
                    SelectingProductNum = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (SelectingProductNum == 0)
                {
                    SelectingProductNum = MaxButtonNum;
                }
                else
                {
                    SelectingProductNum -= 1;
                }
            }

            HavingJuwelText.text = "所持宝石数：" + GameSystem.HavingNumJuwel;

            // 項目選択時の商品説明を変更
            switch (SelectingProductNum)
            {
                case (int)ProductNumber.ReverseTimeUp:
                    productNameText.text = "リバース時間延長";
                    if(1 <= productAmount[SelectingProductNum])
                    {
                        productSentenceText.text = "リバースの持続時間を\n１秒追加する \n\nリバース持続時間：" + ChangeWorld.UraActiveTime
                            + "\n\n在庫数" + productAmount[(int)ProductNumber.ReverseTimeUp];
                        panelProductPriceText.text = "必要宝石数 ：" + productPrice[SelectingProductNum];
                    }
                    else
                    {
                        productSentenceText.text = "商品の在庫がありません \nリバース持続時間：" + ChangeWorld.UraActiveTime;
                        panelProductPriceText.text = "";
                    }
                    break;
                case (int)ProductNumber.shield:
                    productNameText.text = "シールド";
                    productSentenceText.text = "ダメージを一度だけ無効化出来る";
                    if(GameSystem.GetArmorUsing() == true)
                    {
                        productSentenceText.text += "\n現在装備中";
                    }
                    panelProductPriceText.text = "必要宝石数 ：" + productPrice[SelectingProductNum];
                    break;
                case 2:
                    // 選択状態で決定を押すとステージセレクトシーンへ移動
                    productNameText.text = "";
                    productSentenceText.text = "";
                    if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Return))
                    {
                        SceneManager.LoadScene("StageSelect");
                    }
                    break;
            }

            // ステージセレクト以外の項目を選択した状態で決定した場合
            if (SelectingProductNum != (int)ProductNumber.ToStageSelect)
            {
                if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
                {

                    // 購入したい商品の値段分宝石を所持しているか
                    // 足りていない場合
                    if(productAmount[SelectingProductNum] <= 0)
                    {
                        SetCantBuyPanelMessage("商品の在庫がありません");
                    }
                    else if(SelectingProductNum == (int)ProductNumber.shield && GameSystem.GetArmorUsing() == true)
                    {
                        SetCantBuyPanelMessage("現在装備中です");
                    }
                    else if(GameSystem.HavingNumJuwel < productPrice[SelectingProductNum])
                    {
                        SetCantBuyPanelMessage("必要な宝石が足りません");
                    }
                    // 足りている場合
                    else
                    {
                        BuyPanel.gameObject.SetActive(true);
                        BuyProductName.text = productNameText.text;
                        BuyText.text = "を購入しますか？";
                    }
                }
            }
        }
        // 購入パネルが出ている場合
        else
        {
            if(BuyPanel.gameObject.activeSelf == true)
            {
                // 選択項目の変更
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (SelectingYesNoNum < MaxYesNoButtonNum)
                    {
                        SelectingYesNoNum += 1;
                    }
                    else
                    {
                        SelectingYesNoNum = 0;
                    }
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (SelectingYesNoNum <= 0)
                    {
                        SelectingYesNoNum = MaxYesNoButtonNum;
                    }
                    else
                    {
                        SelectingYesNoNum -= 1;
                    }
                }
            }

            //決定を押した場合
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
            {
                // 購入不可パネルの有無
                // 購入不可パネルがある場合
                if(CantBuyPanel.gameObject.activeSelf == true)
                {
                    CantBuyPanel.gameObject.SetActive(false);
                }
                // 購入不可パネルが無い場合
                else
                {
                    switch (SelectingYesNoNum)
                    {
                        // リバース時間延長
                        case 0:
                            BuyPanel.gameObject.SetActive(false);
                            GameSystem.HavingNumJuwel -= productPrice[SelectingProductNum];
                            if(SelectingProductNum == (int)ProductNumber.ReverseTimeUp)
                            {
                                productPrice[(int)ProductNumber.ReverseTimeUp] += 1;
                                productAmount[(int)ProductNumber.ReverseTimeUp] -= 1;
                                ChangeWorld.UraActiveTime += 1.0f;
                            }
                            if(SelectingProductNum == (int)ProductNumber.shield)
                            {
                                GameSystem.SetArmorUsing(true);
                            }
                            break;
                        // アーマー
                        case 1:
                            BuyPanel.gameObject.SetActive(false);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        Test = GameSystem.GetArmorUsing();
    }

    public int GetProductPrice(int productNum)
    {
        return productPrice[productNum];
    }

    void SetCantBuyPanelMessage(string message)
    {
        CantBuyPanel.gameObject.SetActive(true);
        CantBuyProductName.text = productNameText.text;
        CantBuyText.text = message;
    }
    
}
