using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterUI : MonoBehaviour
{
    public GameObject Player;
    public GameObject Goal;
    public GameObject EndPoint;
    public GameObject StartPoint;
    public GameObject TriUI;

    Vector3 IniEndPos;
    Vector3 IniStartPos;
    Vector3 IniPlayerPos;
    Vector3 IniGoalPos;
    Vector3 IniThisPos;

    Vector3 v;
    Vector3 v2;
    Vector3 v3;
    Vector3 v4;
    Vector3 v5;

    float ThisPosX;
    Vector3 ThisPos;
    
    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.Find("Player");
        //float PlayerPosX = Player.transform.position.x;
        //Vector3 PlayerWorldPos = Camera.main.WorldToViewportPoint(Player.transform.position);
        //this.transform.position += new Vector3(PlayerWorldPos.x, PlayerWorldPos.y, PlayerWorldPos.z);
        ThisPos = new Vector3(IniStartPos.x, this.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void Update() 
    {

        v = new Vector3(EndPoint.transform.position.x, EndPoint.transform.position.y, EndPoint.transform.position.z);
        IniEndPos = v.normalized;

        v2 = new Vector3(StartPoint.transform.position.x, StartPoint.transform.position.y, StartPoint.transform.position.z);
        IniStartPos = v2.normalized;

        v3 = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);
        IniPlayerPos = v3.normalized;

        v4 = new Vector3(Goal.transform.position.x, Goal.transform.position.y, Goal.transform.position.z);
        IniGoalPos = v4.normalized;

        float ThisPosX = TriUI.gameObject.transform.position.x;
        Vector3 ThisPos = TriUI.transform.position;
        IniThisPos = ThisPos.normalized;

        //this.transform.position = Player.transform.position;


        IniEndPos = new Vector3(1, 1, 1);
        IniStartPos = new Vector3(0, 0, 0);
        IniThisPos.x = IniPlayerPos.x;
        TriUI.transform.position = new Vector3 (IniPlayerPos.x, this.transform.position.y, this.transform.position.z);


        Debug.Log("IniPlyPos : "+IniPlayerPos);
        Debug.Log("thisPos : " + ThisPos);
        Debug.Log("IniStartPos : " + IniStartPos);
        Debug.Log("IniGoalPos : " + IniGoalPos);
        Debug.Log("IniEndPos : " + IniEndPos);


    }
}
