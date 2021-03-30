using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDiaScoreUI : MonoBehaviour
{
    //text
    public Text NumGetMaxDiaText1;
    public Text NumGetMaxDiaText2;
    public Text NumGetMaxDiaText3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NumGetMaxDiaText1.text = "現在の最高取得数　：　"+GameSystem.NumMaxGetJewelStage1;
        NumGetMaxDiaText2.text = "現在の最高取得数　：　" + GameSystem.NumMaxGetJewelStage2;
        NumGetMaxDiaText3.text = "現在の最高取得数　：　" + GameSystem.NumMaxGetJewelStage3;
    }
}
