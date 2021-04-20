using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWorld : MonoBehaviour
{
    GameObject Player;
    public GameObject UraObj;
    public GameObject OmoteObj;
    public static float CoolDownTime = 0;
    public static float UraActiveTime = 10;
    public GameObject CoolDownUI;
    public GameObject UraActiveUI;
    public static bool StateFront = true;
    private Vector3 SetAfterSwitchingPlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (CoolDownTime <= 0)
        {
            CoolDownUI.SetActive(false);
            if (Input.GetKeyDown(KeyCode.C))
            {
                //SetAfterSwitchingPlayerPos = Player.transform.position;
                UraObj.SetActive(!UraObj.activeSelf);
                OmoteObj.SetActive(!OmoteObj.activeSelf);
                UraActiveUI.SetActive(true);
                CoolDownTime = 10;
                StateFront = false;
            }
        }
        if(OmoteObj.activeSelf==true&&CoolDownTime>0)
        {
            CoolDownTime -= Time.deltaTime;
            UraActiveTime = 10;
           CoolDownUI.SetActive(true);
            UraActiveUI.SetActive(false);
            StateFront = true;
        }
        
        if (UraObj.activeSelf == true )
        {
            UraActiveTime -= Time.deltaTime;
            if(UraActiveTime<0)
            {
                OmoteObj.SetActive(true);
                UraObj.SetActive(false);
                StateFront = true;
                if(Player.transform.position.y<0.4)
                {
                    Player.transform.position = SetAfterSwitchingPlayerPos;
                }
            }
        }
        if (GameSystem.IsGoal==true)
        {
            UraObj.SetActive(false);
            OmoteObj.SetActive(true);
        }
    }
}
