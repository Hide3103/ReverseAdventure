using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadScript : MonoBehaviour
{
    public static bool PadOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 接続されているコントローラの名前を調べる
        var ControllerNames = Input.GetJoystickNames();

        // 一台もコントローラが接続されていなければエラー
        if (ControllerNames[0] == "")
        {
            PadOn = true;
        }
    }
}
