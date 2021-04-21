using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ステージセレクトの背景を変える処理
public class StageSelectBackImage : MonoBehaviour
{

    public GameObject Stage1Img;
    public GameObject Stage2Img;
    public GameObject Stage3Img;
    public GameObject Stage4Img;

    public GameObject LockImg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StageBackImgChange();
    }

    void StageBackImgChange()
    {
        switch (StageSelectCameraScript.SelectingStageNum)
        {
            case 1:
                if (GameSystem.StageCleared[0])
                {
                    Stage1Img.SetActive(true);
                    Stage2Img.SetActive(false);
                    Stage3Img.SetActive(false);
                    Stage4Img.SetActive(false);

                    LockImg.SetActive(false);
                }
                else
                {
                    Stage1Img.SetActive(false);
                    Stage2Img.SetActive(false);
                    Stage3Img.SetActive(false);
                    Stage4Img.SetActive(false);

                    LockImg.SetActive(true);
                }
                break;
            case 2:
                if(GameSystem.StageCleared[1])
                {
                    Stage1Img.SetActive(false);
                    Stage2Img.SetActive(true);
                    Stage3Img.SetActive(false);
                    Stage4Img.SetActive(false);

                    LockImg.SetActive(false);
                }
                else
                {
                    Stage1Img.SetActive(false);
                    Stage2Img.SetActive(false);
                    Stage3Img.SetActive(false);
                    Stage4Img.SetActive(false);

                    LockImg.SetActive(true);
                }
                break;
            case 3:
                if (GameSystem.StageCleared[2])
                {
                    Stage1Img.SetActive(false);
                    Stage2Img.SetActive(false);
                    Stage3Img.SetActive(true);
                    Stage4Img.SetActive(false);

                    LockImg.SetActive(false);
                }
                else
                {
                    Stage1Img.SetActive(false);
                    Stage2Img.SetActive(false);
                    Stage3Img.SetActive(false);
                    Stage4Img.SetActive(false);
                    LockImg.SetActive(true);

                }
                break;
            case 4:
                if (GameSystem.StageCleared[3])
                {
                    Stage1Img.SetActive(false);
                    Stage2Img.SetActive(false);
                    Stage3Img.SetActive(false);
                    Stage4Img.SetActive(true);

                    LockImg.SetActive(false);
                }
                else
                {
                    Stage1Img.SetActive(false);
                    Stage2Img.SetActive(false);
                    Stage3Img.SetActive(false);
                    Stage4Img.SetActive(false);

                    LockImg.SetActive(true);
                }

                break;
        }
    }
}
