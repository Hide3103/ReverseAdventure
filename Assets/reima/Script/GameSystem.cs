using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static bool IsGoal = false;
    //クリアタイム
    public static float ClearTime = 0;

    //ステージセレクト・ショップでの現在の宝石所持数
    public static int HavingNumJuwel = 10;

    //ステージ内での所持宝石数
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

    //各ステージのクリアタイム
    public static float[] StageClearTimes = new float[] {0.0f, 0.0f, 0.0f, 0.0f, 0.0f };
    // 各ステージのクリア状況
    public static bool[] StageCleared = new bool[] { true, false, false, false, false };


    //前どこのステージが選ばれているか
    public static int WasPlayStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        IsGoal = false;
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
    //クリアタイムの代入・取得
    public static void SetClearTime(float setClearTime)
    {
        StageClearTimes[WasPlayStage] = setClearTime;
    }
    public static float GetClearTime(int stageNum)
    {
        return StageClearTimes[stageNum];
    }

    //クリア済みステージの代入・取得
    public static void SetStageCleared(bool stageCleared)
    {
        StageCleared[WasPlayStage] = stageCleared;
    }
    public static bool GetStageCleared(int stageNum)
    {
        return StageCleared[stageNum];
    }
}
