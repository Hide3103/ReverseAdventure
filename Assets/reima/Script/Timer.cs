using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    int minute = 10;
    float seconds = 0f;
    float oldseconds = 0;
    public Text TimeText;
    public GameObject Obj_TimeText;
    public static float TotalTime;
    public static float CUTotalTime;
    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        oldseconds = 0;
        TotalTime = minute * 60 + seconds;


    }

    // Update is called once per frame
    void Update()
    {
        Obj_TimeText.gameObject.SetActive(true);
        if (TotalTime <= 0f)
        {
            return;
        }
        TotalTime = minute * 60 + seconds;
        TotalTime -= Time.deltaTime;


        minute = (int)TotalTime / 60;
        seconds = TotalTime - minute * 60;

        if ((int)seconds != (int)oldseconds)
        {
            TimeText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        }
        oldseconds = seconds;

        if (TotalTime <= 0f)
        {
            Debug.Log("制限時間終了");
        }
    }
    void CountUp()
    {
        CUTotalTime = minute * 60 + seconds;
        CUTotalTime += Time.deltaTime;


        minute = (int)TotalTime / 60;
        seconds = CUTotalTime - minute * 60;

        //if ((int)seconds != (int)oldseconds)
        //{
        //    TimeText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
        //}
        oldseconds = seconds;
    }
}