using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectCameraScript : MonoBehaviour
{
    [SerializeField]
    public static int SelectingStageNum = 1;
    int StageMaxNum = 5;
    int StageMinNum = -1;
    static bool BackButtonSelecting = false;
    int beforeStageNum = 1;

    public GameObject RightCursor;
    public GameObject LeftCursor;
    CursorScript RightCursorScript;
    CursorScript LeftCursorScript;

    [SerializeField]
    bool MovingFlg = false;

    string stageName;

    public bool test;

    public float WaitTime = 0;
    float SetWaitTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        RightCursorScript = RightCursor.GetComponent<CursorScript>();
        LeftCursorScript = LeftCursor.GetComponent<CursorScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (WaitTime > 0)
        {
            WaitTime -= Time.unscaledDeltaTime;
        }
        float hori = Input.GetAxis("Horizontal");
        if (BackButtonSelecting == false)
        {
            if (SelectingStageNum < StageMaxNum)
            {
                if ((Input.GetKeyDown(KeyCode.RightArrow))||(hori>0 && WaitTime <= 0))
                {
                    transform.position += new Vector3(5.0f, 0.0f, 0.0f);
                    SelectingStageNum += 1;
                    RightCursorScript.ButtonPressed = true;
                    RightCursorScript.flashDelta = 0.0f;
                    WaitTime = SetWaitTime;
                }
            }
            if (1 < SelectingStageNum)
            {
                if ((Input.GetKeyDown(KeyCode.LeftArrow))||(hori<0 && WaitTime <= 0))
                {
                    transform.position += new Vector3(-5.0f, 0.0f, 0.0f);
                    SelectingStageNum -= 1;
                    LeftCursorScript.ButtonPressed = true;
                    LeftCursorScript.flashDelta = 0.0f;
                    WaitTime = SetWaitTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                beforeStageNum = SelectingStageNum;
                SelectingStageNum = 0;
                BackButtonSelecting = true;
            }
        }
        else
        {
            //if (StageMinNum < SelectingStageNum)
            //{
            //    if (Input.GetKeyDown(KeyCode.RightArrow))
            //    {
            //        SelectingStageNum += 1;
            //        RightCursorScript.ButtonPressed = true;
            //    }
            //}
            //if (SelectingStageNum < 0)
            //{
            //    if (Input.GetKeyDown(KeyCode.LeftArrow))
            //    {
            //        SelectingStageNum -= 1;
            //        LeftCursorScript.ButtonPressed = true;
            //    }
            //}
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SelectingStageNum = beforeStageNum;
                BackButtonSelecting = false;
            }
        }

        if (SelectingStageNum != 0)
        {
            test = GameSystem.GetStageCleared(SelectingStageNum);
        }
        //Debug.Log(SelectingStageNum);

        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown("joystick button 0"))
        {
            switch(SelectingStageNum)
            {
                case 0:
                    SceneManager.LoadScene("Shop_2");
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    if (test == true)
                    {
                        GameSystem.WasPlayStage = SelectingStageNum;
                        PlayerScript.m_IsPlay = true;
                        MotionPlayer.m_IsPlay = true;
                        SceneManager.LoadScene(GetStageName(SelectingStageNum));
                    }
                    break;
                default:
                    break;
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
}
