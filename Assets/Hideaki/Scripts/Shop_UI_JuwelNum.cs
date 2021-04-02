using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI_JuwelNum : MonoBehaviour
{
    public GameObject shopManager;
    ShopManager shopManagerScript;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        shopManagerScript = shopManager.GetComponent<ShopManager>();
        text = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "現在所持宝石数：" + shopManagerScript.nowJuwelNum;
    }
}
