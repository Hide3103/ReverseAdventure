using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectCameraScript : MonoBehaviour
{
    [SerializeField]
    public static int SelectingStageNum = 1;
    public static int StageMaxNum = 5;
    int StageMinNum = -1;
    static bool BackButtonSelecting = false;
    int beforeStageNum = 1;

    public GameObject RightCursor;
    public GameObject LeftCursor;
    CursorScript RightCursorScript;
    CursorScript LeftCursorScript;

    AudioSource stageSelectAudio;
    public AudioClip SE_Enter;
    public AudioClip SE_ItemChange;
    public AudioClip SE_Cancel;

    [SerializeField]
    bool MovingFlg = false;

    string stageName;

    public bool test;

    public float WaitTime = 0;
    float SetWaitTime = GameSystem.SetWaitTime;
    // Start is called before the first frame update
    void Start()
    {
        RightCursorScript = RightCursor.GetComponent<CursorScript>();
        LeftCursorScript = LeftCursor.GetComponent<CursorScript>();

        stageSelectAudio = GetComponent<AudioSource>();

        //if(1 <= GameSystem.WasPlayStage && GameSystem.WasPlayStage <= 5)
        //{
        //    SelectingStageNum = GameSystem.WasPlayStage;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("SelectingStageNum : " + SelectingStageNum);

        if (WaitTime > 0)
        {
            WaitTime -= Time.unscaledDeltaTime;
        }
        float ver = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");
        if (BackButtonSelecting == false)
        {
            if (SelectingStageNum < StageMaxNum)
            {
                if ((Input.GetKeyDown(KeyCode.RightArrow))||(hori>0.3f && WaitTime <= 0))
                {
                    transform.position += new Vector3(5.0f, 0.0f, 0.0f);
                    SelectingStageNum += 1;
                    stageSelectAudio.PlayOneShot(SE_ItemChange);
                    RightCursorScript.ButtonPressed = true;
                    RightCursorScript.flashDelta = 0.0f;
                    WaitTime = SetWaitTime;
                }
            }
            if (1 < SelectingStageNum)
            {
                if ((Input.GetKeyDown(KeyCode.LeftArrow))||(hori<-0.3f && WaitTime <= 0))
                {
                    transform.position += new Vector3(-5.0f, 0.0f, 0.0f);
                    SelectingStageNum -= 1;
                    stageSelectAudio.PlayOneShot(SE_ItemChange);
                    LeftCursorScript.ButtonPressed = true;
                    LeftCursorScript.flashDelta = 0.0f;
                    WaitTime = SetWaitTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || (ver < 0 && WaitTime <= 0))
            {
                stageSelectAudio.PlayOneShot(SE_ItemChange);
                beforeStageNum = SelectingStageNum;
                SelectingStageNum = 0;
                BackButtonSelecting = true;
                WaitTime = SetWaitTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || (ver > 0 && WaitTime <= 0))
            {
                stageSelectAudio.PlayOneShot(SE_ItemChange);
                SelectingStageNum = beforeStageNum;
                BackButtonSelecting = false;
                WaitTime = SetWaitTime;
            }
        }

        if (SelectingStageNum != 0)
        {
            test = GameSystem.GetStageCleared(SelectingStageNum);
        }

        //Debug.Log("選択しているステージの必要宝石数" + GameSystem.ClearJuwel[SelectingStageNum - 1]);
        //Debug.Log("獲得済みの合計宝石数" + GameSystem.GetAllStageJuwelNum());
        //if (GameSystem.GetStageCleared(SelectingStageNum) == true)
        //{
        //    Debug.Log("GameSystem.GetStageCleared(SelectingStageNum) = true");
        //}
        //else
        //{
        //    Debug.Log("GameSystem.GetStageCleared(SelectingStageNum) = false");
        //}
        //if (GameSystem.ClearJuwel[SelectingStageNum] <= GameSystem.GetAllStageJuwelNum())
        //{
        //    Debug.Log("GameSystem.ClearJuwel[SelectingStageNum] <= GameSystem.GetAllStageJuwelNum() == true");
        //}
        //else
        //{
        //    Debug.Log("GameSystem.ClearJuwel[SelectingStageNum] <= GameSystem.GetAllStageJuwelNum() == false");
        //}

        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown("joystick button 0"))
        {
            switch(SelectingStageNum)
            {
                case 0:
                    SceneManager.LoadScene("Title");
                    stageSelectAudio.PlayOneShot(SE_Cancel);
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    if (GameSystem.GetStageCleared(SelectingStageNum) == true 
                        && GameSystem.ClearJuwel[SelectingStageNum - 1] <= GameSystem.GetAllStageJuwelNum())
                    {
                        stageSelectAudio.PlayOneShot(SE_Enter);
                        GameSystem.WasPlayStage = SelectingStageNum;
                        PlayerScript.m_IsPlay = true;
                        MotionPlayer.m_IsPlay = true;
                        SceneManager.LoadScene(GetStageName(SelectingStageNum));
                    }
                    else
                    {
                        stageSelectAudio.PlayOneShot(SE_Cancel);
                    }
                    break;
                default:
                    break;
            }
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            for(int stageNum = 0; stageNum < 5; stageNum++)
            {
                for(int juwelNum = 0; juwelNum < 5; juwelNum++)
                {
                    GameSystem.SetJuwelCollection(stageNum, juwelNum, true);
                }
                GameSystem.SetStageCleared(true, stageNum);
            }
        }
    }

    string GetStageName(int selectingStageNum)
    {
        switch (selectingStageNum)
        {
            case 1:
                stageName = "Stage_1";
                break;
            case 2:
                stageName = "Stage_2";
                break;
            case 3:
                stageName = "Stage_3";
                break;
            //case 4:
            //    stageName = "Stage_4";
            //    break;
            //case 5:
            //    stageName = "Stage_5";
            //    break;
            default:
                break;
        }

        return stageName;
    }

    // 選択中のステージボタンの番号を取得
    public static int GetSelectingStageNum()
    {
        return SelectingStageNum;
    }
}
