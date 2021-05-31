using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriScript_UI : MonoBehaviour
{
    public GameObject StageBar;
    public Vector3 StageBarPos;

    public GameObject PlayerStart;
    public GameObject PlayerGoal;
    public GameObject Player;

    public Vector3 PlayerStartPos;
    public Vector3 PlayerGoalPos;
    public Vector3 PlayerPos;


    public GameObject UI_Start;
    public GameObject UI_Goal;
    public GameObject UI_Player;

    public Vector3 UI_StartPos;
    public Vector3 UI_GoalPos;
    public Vector3 UI_PlayerPos;

    public float uiPosx = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStartPos = PlayerStart.GetComponent<Transform>().position;
        PlayerGoalPos = PlayerGoal.GetComponent<Transform>().position;

        UI_StartPos = UI_Start.GetComponent<Transform>().localPosition;
        UI_GoalPos = UI_Goal.GetComponent<Transform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            // プレイヤーの現在地
            PlayerPos = Player.GetComponent<Transform>().position;
            // UI上でのプレイヤーの現在地
            UI_PlayerPos = GetComponent<Transform>().localPosition;

            // ゴールとスタートの間の距離
            float StartToGoalDistance = Vector3.Distance(PlayerGoalPos, PlayerStartPos);
            // プレイヤーとスタートの間の距離
            float StartToPlayerDistance = Vector3.Distance(PlayerPos, PlayerStartPos);
            // プレイヤーとゴールの間の距離
            float GoalToPlayerDistance = Vector3.Distance(PlayerGoalPos, PlayerStartPos);

            // UI上でのゴールとスタートの間の距離
            float UI_StartToGoalDistance = Vector3.Distance(UI_GoalPos, UI_StartPos);
            // UI上でのプレイヤーとスタートの間の距離
            float UI_StartToPlayerDistance = Vector3.Distance(UI_PlayerPos, UI_StartPos);
            // UI上でのプレイヤーとゴールの間の距離
            float UI_GoalToPlayerDistance = Vector3.Distance(UI_GoalPos, UI_StartPos);

            float UI_PosX = StartToPlayerDistance / StartToGoalDistance * UI_StartToGoalDistance;/* / StartToGoalDistance * UI_StartToGoalDistance;*/

            if(PlayerStartPos.x < PlayerPos.x)
            {
                this.transform.localPosition = new Vector3(UI_PosX + UI_StartPos.x, 0.1f, 0.0f);
            }
        }
    }
}
