using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public static int[] productAmount = new int[] {3, 1};
    public static int[] productPrice = new int[] {2, 1};
    public float nowJuwelNum = GameSystem.HavingNumJuwel;
    public GameObject HavingJuwel;
    Text HavingJuwelText;

    public int SelectingProductNum = 0;
    public int MaxButtonNum = 2;

    public int SelectingYesNoNum = 0;
    public int MaxYesNoButtonNum = 1;


    public GameObject BuyPanel;
    public Text BuyProductName;
    public Text BuyText;
    bool BuyPanelDisplayFlg;

    public GameObject productName;
    public Text productNameText;
    public GameObject productSentence;
    public Text productSentenceText;


    Button button1;
    Button button2;

    // Start is called before the first frame update
    void Start()
    {
        HavingJuwelText = HavingJuwel.GetComponent<Text>();

        productNameText = productName.GetComponent<Text>();
        productSentenceText = productSentence.GetComponent<Text>();

        button1 = productName.GetComponent<Button>();
        button2 = productSentence.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BuyPanel.gameObject.activeSelf == false)
        {
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

            switch (SelectingProductNum)
            {
                case (int)ProductNumber.ReverseTimeUp:
                    productNameText.text = "リバース時間延長";
                    productSentenceText.text = "リバースを使用する時間が延びる";
                    break;
                case (int)ProductNumber.shield:
                    productNameText.text = "シールド";
                    productSentenceText.text = "ダメージを一度だけ無効化出来る";
                    break;
                case 2:
                    productNameText.text = "";
                    productSentenceText.text = "";
                    if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Return))
                    {
                        SceneManager.LoadScene("StageSelect");
                    }
                    break;
            }

            if (SelectingProductNum != (int)ProductNumber.ToStageSelect)
            {
                if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
                {
                    BuyPanel.gameObject.SetActive(true);
                    BuyProductName.text = productNameText.text;
                    BuyText.text = "を購入しますか？";
                }
            }
        }
        else
        {
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

            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
            {
                switch (SelectingYesNoNum)
                {
                    case 0:
                        BuyPanel.gameObject.SetActive(false);
                        GameSystem.HavingNumJuwel -= productPrice[SelectingProductNum];
                        break;
                    case 1:
                        BuyPanel.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public int GetProductPrice(int productNum)
    {
        return productPrice[productNum];
    }
    
}
