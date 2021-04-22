using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownCountUI : MonoBehaviour
{
    public Text UraActiveCountTxt;
    public Text CoolDownTimeCountTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeWorld.StateFront == false)
        {
            UraActiveCountTxt.enabled = true;
            CoolDownTimeCountTxt.enabled = false;
            UraActiveCountTxt.text = ChangeWorld.UraActiveTime.ToString("N0");
            UraActiveCountTxt.color = new Color(1, 1, 1);
            if(ChangeWorld.UraActiveTime<=0)
            {
                UraActiveCountTxt.enabled = false;
            }
            else
            {
                UraActiveCountTxt.enabled = true;
            }
        }
        if(ChangeWorld.StateFront==true)
        {
            UraActiveCountTxt.enabled = false;
            CoolDownTimeCountTxt.enabled = true;
            CoolDownTimeCountTxt.text = ChangeWorld.CoolDownTime.ToString("N0");
            UraActiveCountTxt.color = new Color(0,0,0);
            if(ChangeWorld.CoolDownTime<=0)
            {
                CoolDownTimeCountTxt.enabled = false;
            }
            else
            {
             CoolDownTimeCountTxt.enabled = true;
            }
        }

    }
}
