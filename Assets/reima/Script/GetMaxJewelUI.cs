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
                    int GettedJuwel = 0;
                    for(int i = 0; i < GameSystem.Stage1JuwelCollection.Length; i++)
                    {
                        if (GameSystem.GetJuwelCollection(i) == true)
                        {
                            GettedJuwel += 1;
                        }
                    }
                    MaxJewelUI.text = "宝石 : "+ GettedJuwel + "/"+ GameSystem.Stage1JuwelCollection.Length;
                    break;
                }
            case 2:
                {
                    int GettedJuwel = 0;
                    for (int i = 0; i < GameSystem.Stage2JuwelCollection.Length; i++)
                    {
                        if (GameSystem.GetJuwelCollection(i) == true)
                        {
                            GettedJuwel += 1;
                        }
                    }
                    MaxJewelUI.text = "宝石 : " + GettedJuwel + "/" + GameSystem.Stage2JuwelCollection.Length;
                    break;
                }
            case 3:
                {
                    int GettedJuwel = 0;
                    for (int i = 0; i < GameSystem.Stage3JuwelCollection.Length; i++)
                    {
                        if (GameSystem.GetJuwelCollection(i) == true)
                        {
                            GettedJuwel += 1;
                        }
                    }
                    MaxJewelUI.text = "宝石 : " + GettedJuwel + "/" + GameSystem.Stage3JuwelCollection.Length;
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
