using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static bool IsGoal = false;
    public static float ClearTime = 0;
    public static float NumJewel = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerScript.m_IsPlay)
        {
            ClearTime += Time.deltaTime;
        }
    }
}
