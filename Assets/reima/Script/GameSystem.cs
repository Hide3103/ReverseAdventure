﻿using System.Collections;
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

    //1ステージ目のダイヤの数
    public static bool[] Stage1JuwelGetted = new bool[] { false, false, false, false, false, false, false, false, false, false, };
    public static bool[] Stage1JuwelCollection = new bool[] { false, false, false, false, false, false, false, false, false, false, };
    public static bool[] Stage2JuwelGetted = new bool[] { false, false, false, false, false, false, false, false, false, false, };
    public static bool[] Stage2JuwelCollection = new bool[] { false, false, false, false, false, false, false, false, false, false, };
    public static bool[] Stage3JuwelGetted = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
    public static bool[] Stage3JuwelCollection = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

    //各ステージのクリアタイム
    public static float[] StageClearTimes = new float[] { 600.0f, 600.0f, 600.0f, 600.0f, 600.0f };
    // 各ステージのクリア状況
    public static bool[] StageCleared = new bool[] { true, false, false, false, false };


    //前どこのステージが選ばれているか
    public static int WasPlayStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        ClearTime = 0.0f;
        IsGoal = false;

        switch (WasPlayStage)
        {
            case 1:
                for (int i = 0; i < GameSystem.Stage1JuwelGetted.Length; i++)
                {
                    SetJuwelGetted(i, false);
                }
                break;
            case 2:
                for (int i = 0; i < GameSystem.Stage2JuwelGetted.Length; i++)
                {
                    SetJuwelGetted(i, false);
                }
                break;
            case 3:
                for (int i = 0; i < GameSystem.Stage3JuwelGetted.Length; i++)
                {
                    SetJuwelGetted(i, false);
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.m_IsPlay&&GameSystem.IsGoal==false)
        {
            ClearTime += Time.deltaTime;

        }
        if (NumNowMaxGetJewelStage1 > NumMaxGetJewelStage1)
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
    public static void SetStageCleared(bool stageCleared, int stageNum)
    {
        StageCleared[stageNum] = stageCleared;
    }
    public static bool GetStageCleared(int stageNum)
    {
        return StageCleared[stageNum - 1];
    }

    // 現在プレイしているステージの宝石獲得状況
    public static void SetJuwelGetted(int juwelNum, bool flg)
    {
        switch (WasPlayStage)
        {
            case 1:
                Stage1JuwelGetted[juwelNum] = flg;
                break;
            case 2:
                Stage2JuwelGetted[juwelNum] = flg;
                break;
            case 3:
                Stage3JuwelGetted[juwelNum] = flg;
                break;
            default:
                break;
        }
    }
    public static bool GetJuwelGetted(int juwelNum)
    {
        switch (WasPlayStage)
        {
            case 1:
                return Stage1JuwelGetted[juwelNum];
            case 2:
                return Stage2JuwelGetted[juwelNum];
            case 3:
                return Stage3JuwelGetted[juwelNum];
            default:
                break;
        }
        return false;
    }

    // クリアしたステージの宝石を記録する
    public static void SetJuwelCollection(int juwelNum)
    {
        switch (WasPlayStage)
        {
            case 1:
                Stage1JuwelCollection[juwelNum] = Stage1JuwelGetted[juwelNum];
                break;
            case 2:
                Stage2JuwelCollection[juwelNum] = Stage2JuwelGetted[juwelNum];
                break;
            case 3:
                Stage3JuwelCollection[juwelNum] = Stage3JuwelGetted[juwelNum];
                break;
            default:
                break;
        }
    }
    public static bool GetJuwelCollection(int juwelNum)
    {
        switch (WasPlayStage)
        {
            case 1:
                return Stage1JuwelCollection[juwelNum];
            case 2:
                return Stage2JuwelCollection[juwelNum];
            case 3:
                return Stage3JuwelCollection[juwelNum];
            default:
                break;
        }
        return false;
    }
}
