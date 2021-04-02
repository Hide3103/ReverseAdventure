using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectCameraScript : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        RightCursorScript = RightCursor.GetComponent<CursorScript>();
        LeftCursorScript = LeftCursor.GetComponent<CursorScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (BackButtonSelecting == false)
        {
            if (SelectingStageNum < StageMaxNum)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.position += new Vector3(5.0f, 0.0f, 0.0f);
                    SelectingStageNum += 1;
                    RightCursorScript.ButtonPressed = true;
                    RightCursorScript.flashDelta = 0.0f;
                }
            }
            if (1 < SelectingStageNum)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.position += new Vector3(-5.0f, 0.0f, 0.0f);
                    SelectingStageNum -= 1;
                    LeftCursorScript.ButtonPressed = true;
                    LeftCursorScript.flashDelta = 0.0f;
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

        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
        {
            switch(SelectingStageNum)
            {
                case 0:
                    SceneManager.LoadScene("Shop_2");
                    break;
                case 1:
                    SceneManager.LoadScene("Stage_1");
                    GameSystem.WasPlayStage = 1;
                    break;
            }
        }
    }
}
