using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI : MonoBehaviour
{
    RectTransform IsRectTf;
    // Start is called before the first frame update
    void Start()
    {
        IsRectTf = GetComponent<RectTransform>();
        Image gaugeCtrl = GetComponent<Image>();
        gaugeCtrl.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        IsRectTf.LookAt(Camera.main.transform);

        Image gaugeCtrl = GetComponent<Image>();
        if (this.gameObject.name == "CoolDownUI")
        {
            gaugeCtrl.fillAmount = 1-ChangeWorld.CoolDownTime/ 10;
        }
        if (this.gameObject.name == "UraActiveUI")
        {
            gaugeCtrl.fillAmount =  ChangeWorld.UraActiveTime / 10;
        }
    }
}
