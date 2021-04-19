using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectClearTime : MonoBehaviour
{
    public Text ClearTimeText;
    public GameObject cameraObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClearTimeText.text = "クリアタイム：" + GameSystem.GetClearTime(StageSelectCameraScript.SelectingStageNum) + "秒";
    }
}
