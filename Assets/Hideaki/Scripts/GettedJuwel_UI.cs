using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GettedJuwel_UI : MonoBehaviour
{
    public GameObject camera;
    StageSelectCameraScript CameraScript;

    public Image Diamond_1;
    public Image Diamond_2;
    public Image Diamond_3;
    public Image Diamond_4;
    public Image Diamond_5;

    // Start is called before the first frame update
    void Start()
    {
        CameraScript = camera.GetComponent<StageSelectCameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; 0 < 5; i++)
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
                Diamond_1.color = color;
                break;
            case 1:
                Diamond_2.color = color;
                break;
            case 2:
                Diamond_3.color = color;
                break;
            case 3:
                Diamond_4.color = color;
                break;
            case 4:
                Diamond_5.color = color;
                break;
            default:
                break;
        }
    }
}
