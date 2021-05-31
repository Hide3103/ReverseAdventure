using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    //SaveData用変数
    public static int ToSaveNumData = 1;//ステージ1がクリアされているから1


    public static bool IsGoal = false;
    //クリアタイム
    public static float ClearTime = 0;

    //ステージセレクト・ショップでの現在の宝石所持数
    public static int HavingNumJuwel = 100;

    //ステージ内での所持宝石数
    public static float NumJewel = 0;
    //これまでで最大何個とったかを入れる変数
    public static int NumMaxGetJewelStage1 = 0;
    public static int NumMaxGetJewelStage2 = 0;
    public static int NumMaxGetJewelStage3 = 0;
    public static int NumMaxGetJewelStage4 = 0;
    public static int NumMaxGetJewelStage5 = 0;
    public static int NumMaxGetJewelStage6 = 0;
    public static int NumMaxGetJewelStage7 = 0;
    public static int NumMaxGetJewelStage8 = 0;
    public static int NumMaxGetJewelStage9 = 0;
    public static int NumMaxGetJewelStage10 = 0;
    //今のゲームで何個とったかを入れる変数
    public static int NumNowMaxGetJewelStage1 = 0;
    public static int NumNowMaxGetJewelStage2 = 0;
    public static int NumNowMaxGetJewelStage3 = 0;
    public static int NumNowMaxGetJewelStage4 = 0;
    public static int NumNowMaxGetJewelStage5 = 0;
    public static int NumNowMaxGetJewelStage6 = 0;
    public static int NumNowMaxGetJewelStage7 = 0;
    public static int NumNowMaxGetJewelStage8 = 0;
    public static int NumNowMaxGetJewelStage9 = 0;
    public static int NumNowMaxGetJewelStage10 = 0;
    //ステージにあるダイヤの最大数
    public static int NumStage1MaxJewel = 10;
    public static int NumStage2MaxJewel = 20;
    public static int NumStage3MaxJewel = 30;
    public static int NumStage4MaxJewel = 40;
    public static int NumStage5MaxJewel = 50;
    public static int NumStage6MaxJewel = 10;
    public static int NumStage7MaxJewel = 20;
    public static int NumStage8MaxJewel = 30;
    public static int NumStage9MaxJewel = 40;
    public static int NumStage10MaxJewel = 50;

    //1ステージ目のダイヤの数
    public static bool[] Stage1JuwelGetted = new bool[] { false, false, false, false, false };
    public static bool[] Stage1JuwelCollection = new bool[] { false, false, false, false, false };
    public static bool[] Stage2JuwelGetted = new bool[] { false, false, false, false, false };
    public static bool[] Stage2JuwelCollection = new bool[] { false, false, false, false, false };
    public static bool[] Stage3JuwelGetted = new bool[] { false, false, false, false, false };
    public static bool[] Stage3JuwelCollection = new bool[] { false, false, false, false, false };
    public static bool[] Stage4JuwelGetted = new bool[] { false, false, false, false, false };
    public static bool[] Stage4JuwelCollection = new bool[] { false, false, false, false, false };
    public static bool[] Stage5JuwelGetted = new bool[] { false, false, false, false, false };
    public static bool[] Stage5JuwelCollection = new bool[] { false, false, false, false, false };
    public static bool[] Stage6JuwelGetted = new bool[] { false, false, false, false, false };
    public static bool[] Stage6JuwelCollection = new bool[] { false, false, false, false, false };
    public static bool[] Stage7JuwelGetted = new bool[] { false, false, false, false, false };
    public static bool[] Stage7JuwelCollection = new bool[] { false, false, false, false, false };
    public static bool[] Stage8JuwelGetted = new bool[] { false, false, false, false, false };
    public static bool[] Stage8JuwelCollection = new bool[] { false, false, false, false, false };
    public static bool[] Stage9JuwelGetted = new bool[] { false, false, false, false, false };
    public static bool[] Stage9JuwelCollection = new bool[] { false, false, false, false, false };
    public static bool[] Stage10JuwelGetted = new bool[] { false, false, false, false, false };
    public static bool[] Stage10JuwelCollection = new bool[] { false, false, false, false, false };

    //各ステージのクリアタイム
    public static float[] StageClearTimes = new float[] { 600.0f, 600.0f, 600.0f, 600.0f, 600.0f, 600.0f, 600.0f, 600.0f, 600.0f, 600.0f };
    // 各ステージのクリア状況
    public static bool[] StageCleared = new bool[] { true, false, false, false, false, false, false, false, false, false };
    // ステージ開放に必要な宝石の数
    public static int[] ClearJuwel = new int[] { 0, 3, 6, 9, 12, 15, 18, 21, 24, 27 };

    // プレイヤーがアーマーを装備しているか
    public static bool ArmorUsing = false;

    //前どこのステージが選ばれているか
    public static int WasPlayStage = 0;

    //UIのゲームオブジェクト
    public GameObject Timer;
    public GameObject NumJewelText;
    public GameObject PlayerHP;
    public GameObject CoolDownCountTxt;
    public GameObject CoolDownUI;
    public GameObject MaterUI;
    public GameObject PlayerPosTri;
    public GameObject PlayUI;

    //Padを制御するための時間
    public static float SetWaitTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        ClearTime = 0.0f;
        IsGoal = false;
        NumJewel = 0;
        PlayerScript.PlayerAlive = true;
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
            case 4:
                for (int i = 0; i < GameSystem.Stage4JuwelGetted.Length; i++)
                {
                    SetJuwelGetted(i, false);
                }
                break;
            case 5:
                for (int i = 0; i < GameSystem.Stage5JuwelGetted.Length; i++)
                {
                    SetJuwelGetted(i, false);
                }
                break;
            case 6:
                for (int i = 0; i < GameSystem.Stage6JuwelGetted.Length; i++)
                {
                    SetJuwelGetted(i, false);
                }
                break;
            case 7:
                for (int i = 0; i < GameSystem.Stage7JuwelGetted.Length; i++)
                {
                    SetJuwelGetted(i, false);
                }
                break;
            case 8:
                for (int i = 0; i < GameSystem.Stage8JuwelGetted.Length; i++)
                {
                    SetJuwelGetted(i, false);
                }
                break;
            case 9:
                for (int i = 0; i < GameSystem.Stage9JuwelGetted.Length; i++)
                {
                    SetJuwelGetted(i, false);
                }
                break;
            case 10:
                for (int i = 0; i < GameSystem.Stage10JuwelGetted.Length; i++)
                {
                    SetJuwelGetted(i, false);
                }
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GoalAfterDeleteUI();
        Debug.Log(MotionPlayer.m_PlayerHp);
        if (PlayerScript.m_IsPlay && GameSystem.IsGoal == false)
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
        if (NumNowMaxGetJewelStage6 > NumMaxGetJewelStage6)
        {
            NumMaxGetJewelStage6 = NumNowMaxGetJewelStage6;
        }
        if (NumNowMaxGetJewelStage7 > NumMaxGetJewelStage7)
        {
            NumMaxGetJewelStage7 = NumNowMaxGetJewelStage7;
        }
        if (NumNowMaxGetJewelStage8 > NumMaxGetJewelStage8)
        {
            NumMaxGetJewelStage8 = NumNowMaxGetJewelStage8;
        }
        if (NumNowMaxGetJewelStage9 > NumMaxGetJewelStage9)
        {
            NumMaxGetJewelStage9 = NumNowMaxGetJewelStage9;
        }
        if (NumNowMaxGetJewelStage10 > NumMaxGetJewelStage10)
        {
            NumMaxGetJewelStage10 = NumNowMaxGetJewelStage10;
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
            case 4:
                Stage4JuwelGetted[juwelNum] = flg;
                break;
            case 5:
                Stage5JuwelGetted[juwelNum] = flg;
                break;
            case 6:
                Stage6JuwelGetted[juwelNum] = flg;
                break;
            case 7:
                Stage7JuwelGetted[juwelNum] = flg;
                break;
            case 8:
                Stage8JuwelGetted[juwelNum] = flg;
                break;
            case 9:
                Stage9JuwelGetted[juwelNum] = flg;
                break;
            case 10:
                Stage10JuwelGetted[juwelNum] = flg;
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
            case 4:
                return Stage4JuwelGetted[juwelNum];
            case 5:
                return Stage5JuwelGetted[juwelNum];
            case 6:
                return Stage6JuwelGetted[juwelNum];
            case 7:
                return Stage7JuwelGetted[juwelNum];
            case 8:
                return Stage8JuwelGetted[juwelNum];
            case 9:
                return Stage9JuwelGetted[juwelNum];
            case 10:
                return Stage10JuwelGetted[juwelNum];
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
            case 4:
                Stage4JuwelCollection[juwelNum] = Stage4JuwelGetted[juwelNum];
                break;
            case 5:
                Stage5JuwelCollection[juwelNum] = Stage5JuwelGetted[juwelNum];
                break;
            case 6:
                Stage6JuwelCollection[juwelNum] = Stage6JuwelGetted[juwelNum];
                break;
            case 7:
                Stage7JuwelCollection[juwelNum] = Stage7JuwelGetted[juwelNum];
                break;
            case 8:
                Stage8JuwelCollection[juwelNum] = Stage8JuwelGetted[juwelNum];
                break;
            case 9:
                Stage9JuwelCollection[juwelNum] = Stage9JuwelGetted[juwelNum];
                break;
            case 10:
                Stage10JuwelCollection[juwelNum] = Stage10JuwelGetted[juwelNum];
                break;
            default:
                break;
        }
    }
    public static void SetJuwelCollection(int juwelNum, int stageNum, bool juwelGetted)
    {
        switch (stageNum)
        {
            case 1:
                Stage1JuwelCollection[juwelNum] = juwelGetted;
                break;
            case 2:
                Stage2JuwelCollection[juwelNum] = juwelGetted;
                break;
            case 3:
                Stage3JuwelCollection[juwelNum] = juwelGetted;
                break;
            case 4:
                Stage4JuwelCollection[juwelNum] = juwelGetted;
                break;
            case 5:
                Stage5JuwelCollection[juwelNum] = juwelGetted;
                break;
            case 6:
                Stage6JuwelCollection[juwelNum] = juwelGetted;
                break;
            case 7:
                Stage7JuwelCollection[juwelNum] = juwelGetted;
                break;
            case 8:
                Stage8JuwelCollection[juwelNum] = juwelGetted;
                break;
            case 9:
                Stage9JuwelCollection[juwelNum] = juwelGetted;
                break;
            case 10:
                Stage10JuwelCollection[juwelNum] = juwelGetted;
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
            case 4:
                return Stage4JuwelCollection[juwelNum];
            case 5:
                return Stage5JuwelCollection[juwelNum];
            case 6:
                return Stage6JuwelCollection[juwelNum];
            case 7:
                return Stage7JuwelCollection[juwelNum];
            case 8:
                return Stage8JuwelCollection[juwelNum];
            case 9:
                return Stage9JuwelCollection[juwelNum];
            case 10:
                return Stage10JuwelCollection[juwelNum];
            default:
                break;
        }
        return false;
    }
    public static bool GetJuwelCollection(int juwelNum, int StageNum)
    {
        switch (StageNum)
        {
            case 1:
                return Stage1JuwelCollection[juwelNum];
            case 2:
                return Stage2JuwelCollection[juwelNum];
            case 3:
                return Stage3JuwelCollection[juwelNum];
            case 4:
                return Stage4JuwelCollection[juwelNum];
            case 5:
                return Stage5JuwelCollection[juwelNum];
            case 6:
                return Stage6JuwelCollection[juwelNum];
            case 7:
                return Stage7JuwelCollection[juwelNum];
            case 8:
                return Stage8JuwelCollection[juwelNum];
            case 9:
                return Stage9JuwelCollection[juwelNum];
            case 10:
                return Stage10JuwelCollection[juwelNum];
            default:
                break;
        }
        return false;
    }
    // クリアしたステージの宝石を記録する
    public static int GetClearJuwelNum(int stageNum)
    {
        return ClearJuwel[stageNum];
    }

    public static int GetAllStageJuwelNum()
    {
        int AllJuwelNum = 0;
        for (int stageNum = 1; stageNum <= 10; stageNum++)
        {
            for (int juwelNum = 0; juwelNum < 5; juwelNum++)
            {
                if (GetJuwelCollection(juwelNum, stageNum) == true)
                {
                    AllJuwelNum += 1;
                }
            }
        }
        return AllJuwelNum;
    }

    // プレイヤーにアーマーを付与
    public static void SetArmorUsing(bool armorUsing)
    {
        ArmorUsing = armorUsing;
    }
    public static bool GetArmorUsing()
    {
        return ArmorUsing;
    }

    public void GoalAfterDeleteUI()
    {
        if (IsGoal == true && SceneManager.GetActiveScene().name != "StageSelect")
        {
            PlayerHP.SetActive(false);
            NumJewelText.SetActive(false);
            Timer.SetActive(false);
            CoolDownCountTxt.SetActive(false);
            CoolDownUI.SetActive(false);
            MaterUI.SetActive(false);
            PlayerPosTri.SetActive(false);

        }
        if (IsGoal == false && SceneManager.GetActiveScene().name != "StageSelect")
        {
            PlayerHP.SetActive(true);
            NumJewelText.SetActive(true);
            Timer.SetActive(true);
            CoolDownCountTxt.SetActive(true);
            CoolDownUI.SetActive(true);
            MaterUI.SetActive(true);
            PlayerPosTri.SetActive(true);
        }
        if(MotionPlayer.m_PlayerHp == 0 && SceneManager.GetActiveScene().name != "StageSelect")
        {
            PlayerHP.SetActive(false);
            NumJewelText.SetActive(false);
            Timer.SetActive(false);
            CoolDownCountTxt.SetActive(false);
            CoolDownUI.SetActive(false);
            MaterUI.SetActive(false);
            PlayerPosTri.SetActive(false);
        }
    }
}
