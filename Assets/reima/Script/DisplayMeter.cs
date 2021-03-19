using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMeter : MonoBehaviour
{
    public GameObject Player;
    public GameObject GoalObj;
    float PlayerPosX;
    float GoalObjPosX;
    public float PlayerToGoalMeter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GoalObjPosX = GoalObj.transform.position.x;
        PlayerPosX = Player.transform.position.x;
        PlayerToGoalMeter = GoalObjPosX - PlayerPosX;

    }
}
