using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PadScript : MonoBehaviour
{
    public GameObject PadUI;
    public GameObject KeyBoardUI;
    public static bool PadOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();

        if(PadOn ==true && SceneManager.GetActiveScene().name != "CreditScene" && MotionPlayer.GetPlayerArriving() == false)
        {
            KeyBoardUI.SetActive(false);
            PadUI.SetActive(true);
        }
        if(PadOn==false&&SceneManager.GetActiveScene().name!="CreditScene" && MotionPlayer.GetPlayerArriving() == true)
        {
            KeyBoardUI.SetActive(true);
            PadUI.SetActive(false);
        }
        if (GameSystem.IsGoal|| MotionPlayer.GetPlayerArriving() == false)
        {
            KeyBoardUI.SetActive(false);
            PadUI.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "CreditScene")
        {
            if (PadOn == true)
            {
                KeyBoardUI.SetActive(false);
                PadUI.SetActive(true);
            }
            if (PadOn == false)
            {
                KeyBoardUI.SetActive(true);
                PadUI.SetActive(false);
            }
        }
        if(MotionPlayer.GetPlayerArriving()==false)
        {
            KeyBoardUI.SetActive(false);
            PadUI.SetActive(false);
        }
    }

    //接続されているコントローラーチェック
    void CheckInput()
    {
        // 接続されているコントローラの名前を調べる
        var ControllerNames = Input.GetJoystickNames();

        if (ControllerNames.Length == 0)
        {
            PadOn = false;
        }
        else
        {
            PadOn = true;
        }
    }

    //
}
