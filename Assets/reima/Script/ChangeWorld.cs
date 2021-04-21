using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWorld : MonoBehaviour
{
    GameObject Player;
    public GameObject UraObj;
    public GameObject OmoteObj;
    public static float CoolDownTime = 10;
    public static float UraActiveTime = 10;
    public GameObject CoolDownUI;
    public GameObject UraActiveUI;
    public static bool StateFront = true;
    private Vector3 SetAfterSwitchingPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");


        CoolDownTime = 10;
        UraActiveTime = 10;
        StateFront = true;
        UraActiveUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(CoolDownTime + " : CoolDownTime");
        Debug.Log(UraActiveTime + " : UraActiveTime");

        //クールダウンが10以上
        if (CoolDownTime >= 10 && OmoteObj.activeSelf == true)
        {
            //クールダウンUIをオフにする
            CoolDownUI.SetActive(false);
        }
        if (CoolDownTime <= 10 && StateFront == true)
        {
            CoolDownTime += Time.deltaTime;
            UraActiveTime += Time.deltaTime;
        }

        //裏オブジェがtrueかつ表オブジェがfalseで
        if (UraObj.activeSelf == true && OmoteObj.activeSelf == false)
        {
            //裏アクティブタイムが0より大きい
            if (UraActiveTime > 0)
            {
                //裏アクティブタイムを引き続ける
                UraActiveTime -= Time.deltaTime;
            }
            //裏アクティブタイムが0以上
            if (UraActiveTime < 0)
            {
                //表オブジェを切り替える
                OmoteObj.SetActive(!OmoteObj.activeSelf);
                //裏オブジェを切り替える
                UraObj.SetActive(!UraObj.activeSelf);
                UraActiveUI.SetActive(false);
                CoolDownUI.SetActive(true);
                StateFront = true;

            }
        }


        //0以上の間
        if (CoolDownTime >= 0 && Input.GetKeyDown(KeyCode.C) && StateFront == true)
        {
            //表オブジェを切り替える
            OmoteObj.SetActive(!OmoteObj.activeSelf);
            //裏オブジェを切り替える
            UraObj.SetActive(!UraObj.activeSelf);

            //裏アクティブタイムUIをtrueにする
            UraActiveUI.SetActive(true);
            CoolDownTime = 0;
            StateFront = false;
        }

        //裏アクティブタイムが10以下かつ裏オブジェがtrueの時かつCを押したとき
        if (UraActiveTime < 10 && StateFront == false && Input.GetKeyDown(KeyCode.C))
        {
            //表オブジェを切り替える
            OmoteObj.SetActive(!OmoteObj.activeSelf);
            //裏オブジェを切り替える
            UraObj.SetActive(!UraObj.activeSelf);

            //クールダウンタイムをtrueにする
            CoolDownUI.SetActive(true);
            //裏アクティブタイムをfalseにする
            UraActiveUI.SetActive(false);
            UraActiveTime = 0;
            StateFront = true;

        }




        //------------------------------------------------------------------------
        //表
        //if (CoolDownTime <= 0)
        //{
        //    CoolDownUI.SetActive(false);
        //    if (Input.GetKeyDown(KeyCode.C))
        //    {
        //        //SetAfterSwitchingPlayerPos = Player.transform.position;
        //        UraObj.SetActive(!UraObj.activeSelf);
        //        OmoteObj.SetActive(!OmoteObj.activeSelf);
        //        UraActiveUI.SetActive(true);
        //        StateFront = false;
        //    }
        //}


        ////裏
        //if (UraActiveTime >= 0)
        //{
        //    if (Input.GetKeyDown(KeyCode.C))
        //    {
        //        UraObj.SetActive(!UraObj.activeSelf);
        //        OmoteObj.SetActive(!OmoteObj.activeSelf);
        //        UraActiveUI.SetActive(false);
        //        CoolDownUI.SetActive(true);
        //        StateFront = true;
        //    }
        //}

        //if (UraActiveTime > 0 && UraObj.activeSelf == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.C))
        //    {
        //        //SetAfterSwitchingPlayerPos = Player.transform.position;
        //        UraObj.SetActive(!UraObj.activeSelf);
        //        OmoteObj.SetActive(!OmoteObj.activeSelf);
        //        CoolDownTime = 10;
        //        CoolDownUI.SetActive(true);
        //        UraActiveUI.SetActive(false);
        //        StateFront = true;
        //    }
        //}

        //if (OmoteObj.activeSelf == true && CoolDownTime > 0)
        //{
        //    CoolDownTime -= Time.deltaTime;
        //    UraActiveTime = 10;
        //    CoolDownUI.SetActive(true);
        //    UraActiveUI.SetActive(false);
        //    StateFront = true;
        //}

        //if (UraObj.activeSelf == true)
        //{
        //    UraActiveTime -= Time.deltaTime;
        //    if (UraActiveTime < 0)
        //    {
        //        OmoteObj.SetActive(true);
        //        UraObj.SetActive(false);
        //        StateFront = true;
        //        if (Player.transform.position.y < 0.4)
        //        {
        //            Player.transform.position = SetAfterSwitchingPlayerPos;
        //        }
        //    }
        //}
        if (GameSystem.IsGoal == true)
        {
            UraObj.SetActive(false);
            OmoteObj.SetActive(true);
        }
    }
}
