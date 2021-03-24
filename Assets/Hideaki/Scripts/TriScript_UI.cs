using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriScript_UI : MonoBehaviour
{
    public GameObject PlayerStart;
    public GameObject PlayerGoal;
    public GameObject player;
    public GameObject StageBar;

    Vector3 startPos;
    Vector3 goalPos;
    Vector3 playerPos;
    Vector3 UI_StartPos;
    Vector3 UI_GoalPos;
    Vector3 UI_PlayerPos;

    public float uiPosx = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = PlayerStart.GetComponent<Transform>().position;
        goalPos = PlayerGoal.GetComponent<Transform>().position;

        Transform stageBarTrans = StageBar.GetComponent<Transform>();
        UI_StartPos = stageBarTrans.position - stageBarTrans.localScale;
        UI_GoalPos = stageBarTrans.position + stageBarTrans.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.GetComponent<Transform>().position;

        Vector3 StageDistace = goalPos - startPos;
        Vector3 toGoalDistance = goalPos - playerPos;
        float goalStartDistance = Vector3.Distance(goalPos, startPos);

        Vector3 toPlayerPos = playerPos - startPos;
        float playerStartDistance = Vector3.Distance(playerPos, startPos);

        Vector3 UI_StageDistance = UI_GoalPos - UI_StartPos; 

        float PosX = StageDistace.x - toGoalDistance.x; // スタートからの距離
        float UI_PosX = StageBar.transform.position.x - 7.0f + playerStartDistance * 14 / goalStartDistance;

        uiPosx = UI_PosX;

        this.transform.position = new Vector3(UI_PosX, this.transform.position.y, this.transform.position.z);

    }
}
