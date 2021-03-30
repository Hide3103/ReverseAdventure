using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static bool IsGoal = false;
    //クリアタイム
    public static float ClearTime = 0;
    
    public static float NumJewel = 0;
    //これまでで最大何個とったかを入れる変数
    public static int NumMaxGetJewelStage1 = 0;
    public static int NumMaxGetJewelStage2 = 0;
    public static int NumMaxGetJewelStage3 = 0;
    public static int NumMaxGetJewelStage4 = 0;
    public static int NumMaxGetJewelStage5 = 0;
    //今のゲームで何個とったかを入れる変数
    public static int NumNowMaxGetJewelStage1 = 0;
    public static int NumNowMaxGetJewelStage2 = 0;
    public static int NumNowMaxGetJewelStage3 = 0;
    public static int NumNowMaxGetJewelStage4 = 0;
    public static int NumNowMaxGetJewelStage5 = 0;
    //ステージにあるダイヤの最大数
    public static int NumStage1MaxJewel = 10;
    public static int NumStage2MaxJewel = 20;
    public static int NumStage3MaxJewel = 30;
    public static int NumStage4MaxJewel = 40;
    public static int NumStage5MaxJewel = 50;
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
        if(NumNowMaxGetJewelStage1>NumMaxGetJewelStage1)
        {
            NumMaxGetJewelStage1 = NumNowMaxGetJewelStage1;
        }
        if (NumNowMaxGetJewelStage2 > NumMaxGetJewelStage2)
        {
            NumMaxGetJewelStage2 = NumNowMaxGetJewelStage2;
        }
        if (NumNowMaxGetJewelStage3 > NumMaxGetJewelStage3)
        {
            NumMaxGetJewelStage3 = NumNowMaxGetJewelStage3;
        }
        if (NumNowMaxGetJewelStage4 > NumMaxGetJewelStage4)
        {
            NumMaxGetJewelStage4 = NumNowMaxGetJewelStage4;
        }
        if (NumNowMaxGetJewelStage5 > NumMaxGetJewelStage5)
        {
            NumMaxGetJewelStage5 = NumNowMaxGetJewelStage5;
        }
    }
}
