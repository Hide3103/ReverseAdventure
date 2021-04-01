using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    public GameObject Player;
    public GameObject UraObj;
    public GameObject CoolDownTimeUI;
    public GameObject UraActiveUI;
    public GameObject OmoteObj;
    public float CoolDownTimeUseUI = 0;
    RectTransform IsRectTf;
    // Start is called before the first frame update
    void Start()
    {
        IsRectTf = GetComponent<RectTransform>();
       
    }

    // Update is called once per frame
    void Update()
    {
        IsRectTf.LookAt(Camera.main.transform);
        Image gaugeCtrl = GetComponent<Image>();
        if (this.gameObject.name == "CoolDownUI")
        {
            CoolDownTimeUseUI = ChangeWorld.CoolDownTime;
            gaugeCtrl.fillAmount = 1 - CoolDownTimeUseUI / 10;
        }
        if (this.gameObject.name == "UraActiveUI")
        {
            CoolDownTimeUseUI = ChangeWorld.CoolDownTime;
            gaugeCtrl.fillAmount =  ChangeWorld.UraActiveTime / 10;
        }
        if(UraObj.activeSelf==true)
        {
            CoolDownTimeUI.SetActive(false);
            UraActiveUI.SetActive(true);
        }
        if (OmoteObj.activeSelf == true)
        {
            CoolDownTimeUI.SetActive(true);
            UraActiveUI.SetActive(false);
        }
    }
}
