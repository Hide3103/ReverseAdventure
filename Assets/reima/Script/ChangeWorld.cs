using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWorld : MonoBehaviour
{
    public GameObject UraObj;
    public GameObject OmoteObj;
    public static float CoolDownTime = 0;
    public static float UraActiveTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CoolDownTime);
        Debug.Log(UraActiveTime);
        if (Input.GetKeyDown(KeyCode.K)&&CoolDownTime<=0)
        {
            UraObj.SetActive(!UraObj.activeSelf);
            OmoteObj.SetActive(!OmoteObj.activeSelf);
            CoolDownTime = 10;
        }
        if(OmoteObj.activeSelf==true&&CoolDownTime>0)
        {
            CoolDownTime -= Time.deltaTime;
            UraActiveTime = 10;
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
