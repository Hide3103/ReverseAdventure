using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMaxJewelUI : MonoBehaviour
{
    public Text MaxJewelUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (StageSelectCameraScript.SelectingStageNum)
        {
            case 1:
                {
                    MaxJewelUI.text = "宝石 : "+ GameSystem.NumMaxGetJewelStage1 + "/"+ GameSystem.NumStage1MaxJewel;
                    break;
                }
            case 2:
                {
                    MaxJewelUI.text = "宝石 : " + GameSystem.NumMaxGetJewelStage2 + "/" + GameSystem.NumStage2MaxJewel;
                    break;
                }
            case 3:
                {
                    MaxJewelUI.text = "宝石 : " + GameSystem.NumMaxGetJewelStage3 + "/" + GameSystem.NumStage3MaxJewel;
                    break;
                }
            case 4:
                {
                    MaxJewelUI.text = "宝石 : " + GameSystem.NumMaxGetJewelStage4 + "/" + GameSystem.NumStage4MaxJewel;
                    break;
                }
            case 5:
                {
                    MaxJewelUI.text = "宝石 : " + GameSystem.NumMaxGetJewelStage5 + "/" + GameSystem.NumStage5MaxJewel;
                    break;
                }
        }
    }
}
