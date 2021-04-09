using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWorld : MonoBehaviour
{
    public GameObject UraObj;
    public GameObject OmoteObj;
    public static float CoolDownTime = 0;
    public static float UraActiveTime = 10;
    public GameObject CoolDownUI;
    public GameObject UraActiveUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CoolDownTime);
        if (CoolDownTime <= 0)
        {
            CoolDownUI.SetActive(false);
            if (Input.GetKeyDown(KeyCode.K))
            {
                UraObj.SetActive(!UraObj.activeSelf);
                OmoteObj.SetActive(!OmoteObj.activeSelf);
                UraActiveUI.SetActive(true);
                CoolDownTime = 10;
            }
        }
        if(OmoteObj.activeSelf==true&&CoolDownTime>0)
        {
            CoolDownTime -= Time.deltaTime;
            UraActiveTime = 10;
           CoolDownUI.SetActive(true);
            UraActiveUI.SetActive(false);
        }
        
        if (UraObj.activeSelf == true )
        {
            UraActiveTime -= Time.deltaTime;
            if(UraActiveTime<0)
            {
                OmoteObj.SetActive(true);
                UraObj.SetActive(false);
            }
        }
        if (GameSystem.IsGoal==true)
        {
            UraObj.SetActive(false);
            OmoteObj.SetActive(true);
        }
    }
}
