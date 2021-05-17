using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GettedJuwel_UI : MonoBehaviour
{
    //public GameObject SelectCamera;
    //StageSelectCameraScript CameraScript;

    public GameObject Diamond_1;
    Image DiaImage_1;
    public GameObject Diamond_2;
    Image DiaImage_2;
    public GameObject Diamond_3;
    Image DiaImage_3;
    public GameObject Diamond_4;
    Image DiaImage_4;
    public GameObject Diamond_5;
    Image DiaImage_5;

    public GameObject GettedJuwelNum;
    Text GettedJuwelNum_Text;

    public static int HavingJuwelNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        DiaImage_1 = Diamond_1.GetComponent<Image>();
        DiaImage_2 = Diamond_2.GetComponent<Image>();
        DiaImage_3 = Diamond_3.GetComponent<Image>();
        DiaImage_4 = Diamond_4.GetComponent<Image>();
        DiaImage_5 = Diamond_5.GetComponent<Image>();

        GettedJuwelNum_Text = GettedJuwelNum.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            int selectingStageNum = StageSelectCameraScript.SelectingStageNum;
            if (selectingStageNum != 0)
            {
                if (GameSystem.GetJuwelCollection(i, selectingStageNum) == true)
                {
                    SetJuwelGettedUI(i, true);
                }
                else
                {
                    SetJuwelGettedUI(i, false);
                }
            }
        }

        GettedJuwelNum_Text.text = "× " + GameSystem.GetAllStageJuwelNum();
    }

    void SetJuwelGettedUI(int juwelNum, bool gettedTrue)
    {
        Color color;
        Color GettedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        Color UnGettedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        if (gettedTrue == true)
        {
            color = GettedColor;
        }
        else
        {
            color = UnGettedColor;
        }
        switch (juwelNum)
        {
            case 0:
                DiaImage_1.color = color;
                break;
            case 1:
                DiaImage_2.color = color;
                break;
            case 2:
                DiaImage_3.color = color;
                break;
            case 3:
                DiaImage_4.color = color;
                break;
            case 4:
                DiaImage_5.color = color;
                break;
            default:
                break;
        }
    }
}
