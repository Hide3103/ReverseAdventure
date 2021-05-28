using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionButton : MonoBehaviour
{
    // 再生アニメーションのResourcesフォルダ内のサブパス
    [SerializeField]
    public Object[] AnimationList;

    // 再生アニメーション指定用 
    private enum AnimationPattern : int
    {
        ClearScene_Next_Move,
        ClearScene_Next_NotMove,
        GameStart_Move,
        GameStart_NotMove,
        Result_Retry_Move,
        Result_Retry_NotMove,
        Result_StageSelect_Move,
        Result_StageSelect_NotMove,
        Select_Back_Move,
        Select_Back_NotMove,
        Title_Credit_Move,
        Title_Credit_NotMove,
        Title_End_Move,
        Title_End_NotMove,
        Title_Option_Move,
        Title_Option_NotMove,
        Count
    }

    // キャラクター管理用 
    private GameObject m_goCharacter = null;
    private GameObject m_goCharPos = null;
    private Vector3 m_vecCharacterPos;      // キャラクター位置 
    private Vector3 m_vecCharacterScale;    // キャラクタースケール 

    // 処理ステップ用 
    private enum Step : int
    {
        Init = 0,   // 初期化 
        ClearScene_Next_Move = 1,
        GameStart_Move = 2,
        Result_Retry_Move = 3,
        Result_StageSelect_Move = 4,
        Select_Back_Move = 5,
        Title_Credit_Move = 6,
        Title_End_Move = 7,
        Title_Option_Move = 8,
        End
    }

    // 処理ステップ管理用 
    private Step m_Step = Step.Init;

    // 汎用
    // いろいろ使いまわす用変数
    private int m_Count = 0;
    private bool m_SW = true;

    bool m_MotionChanged = true;
    int NowMotionPatternNum = 0;

    public bool ThisButtonSelecting = false;
    public int ThisButtonSelectingNum;

    //テープがどちらの面にあるか(true:前面、false:裏面)
    public bool m_TapeOnFrontSide = true;

    void MotionPatternChange(AnimationPattern pattern)
    {
        if (NowMotionPatternNum != (int)pattern)
        {
            NowMotionPatternNum = (int)pattern;
            AnimationChange(pattern);
        }
    }

    // Use this for initialization
    void Start()
    {

        GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        // キャラクターパラメータ関連を設定 

        // 座標設定 
        m_vecCharacterPos.x = 0.0f;
        m_vecCharacterPos.y = 0.0f;
        m_vecCharacterPos.z = 0.0f;

        // スケール設定 
        m_vecCharacterScale.x = 0.01f;
        m_vecCharacterScale.y = 0.01f;
        m_vecCharacterScale.z = 0.01f;

        AnimationStart();
    }

    // Update is called once per frame
    void Update()
    {
        switch (ThisButtonSelectingNum)
        {
            // 初期化
            case (int)Step.Init:

                m_Count = 0;
                m_SW = true;
                //m_Step = ThisButtonSelectingNum;
                AnimationStart();

                break;
            // 「次のステージへ」
            case (int)Step.ClearScene_Next_Move:
                if (ThisButtonSelecting == true)
                {
                    MotionPatternChange(AnimationPattern.ClearScene_Next_Move);
                }
                else
                {
                    MotionPatternChange(AnimationPattern.ClearScene_Next_NotMove);
                }
                break;
            // タイトル「ゲームスタート」
            case (int)Step.GameStart_Move:
                if (ThisButtonSelecting == true)
                {
                    MotionPatternChange(AnimationPattern.GameStart_Move);
                }
                else
                {
                    MotionPatternChange(AnimationPattern.GameStart_NotMove);
                }
                break;
            // リザルト「リトライ」
            case (int)Step.Result_Retry_Move:
                if (ThisButtonSelecting == true)
                {
                    MotionPatternChange(AnimationPattern.Result_Retry_Move);
                }
                else
                {
                    MotionPatternChange(AnimationPattern.Result_Retry_NotMove);
                }
                break;
            // リザルト「ステージセレクト」
            case (int)Step.Result_StageSelect_Move:
                if (ThisButtonSelecting == true)
                {
                    MotionPatternChange(AnimationPattern.Result_StageSelect_Move);
                }
                else
                {
                    MotionPatternChange(AnimationPattern.Result_StageSelect_NotMove);
                }
                break;
            // ステージセレクト「タイトルにもどる」
            case (int)Step.Select_Back_Move:
                if (ThisButtonSelecting == true)
                {
                    MotionPatternChange(AnimationPattern.Select_Back_Move);
                }
                else
                {
                    MotionPatternChange(AnimationPattern.Select_Back_NotMove);
                }
                break;
            // タイトル「クレジット」
            case (int)Step.Title_Credit_Move:
                if (ThisButtonSelecting == true)
                {
                    MotionPatternChange(AnimationPattern.Title_Credit_Move);
                }
                else
                {
                    MotionPatternChange(AnimationPattern.Title_Credit_NotMove);
                }
                break;
            // タイトル「ゲームをやめる」
            case (int)Step.Title_End_Move:
                if (ThisButtonSelecting == true)
                {
                    MotionPatternChange(AnimationPattern.Title_End_Move);
                }
                else
                {
                    MotionPatternChange(AnimationPattern.Title_End_NotMove);
                }
                break;
            // タイトル「オプション」
            case (int)Step.Title_Option_Move:
                if (ThisButtonSelecting == true)
                {
                    MotionPatternChange(AnimationPattern.Title_Option_Move);
                }
                else
                {
                    AnimationChange(AnimationPattern.Title_Option_NotMove);
                }
                break;
            default:
                break;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            ThisButtonSelecting = !ThisButtonSelecting;
        }
    }

    // アニメーション開始 
    private void AnimationStart()
    {
        Script_SpriteStudio6_Root scriptRoot = null;    // SpriteStudio Anime を操作するためのクラス
        int listLength = AnimationList.Length;

        // すでにアニメーション生成済 or リソース設定無い場合はreturn
        if (m_goCharacter != null || listLength < 1)
            return;

        // 再生するリソース名をリストから取得して再生する
        Object resourceObject = AnimationList[0];
        if (resourceObject != null)
        {
            // アニメーションを実体化
            m_goCharacter = Instantiate(resourceObject, Vector3.zero, Quaternion.identity) as GameObject;
            if (m_goCharacter != null)
            {
                scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(m_goCharacter);
                if (scriptRoot != null)
                {
                    // 座標設定するためのGameObject作成
                    m_goCharPos = new GameObject();
                    if (m_goCharPos == null)
                    {
                        // 作成できないケース対応 
                        Destroy(m_goCharacter);
                        m_goCharacter = null;
                    }
                    else
                    {
                        // Object名変更 
                        m_goCharPos.name = "Button";

                        // 座標設定 
                        m_goCharacter.transform.parent = m_goCharPos.transform;

                        // 自分の子に移動して座標を設定
                        m_goCharPos.transform.parent = this.transform;
                        m_goCharPos.transform.localPosition = m_vecCharacterPos;
                        m_goCharPos.transform.localRotation = Quaternion.identity;
                        m_goCharPos.transform.localScale = m_vecCharacterScale;

                        //アニメーション再生
                        MotionPatternChange(AnimationPattern.Count);
                    }
                }
            }
        }
    }

    // アニメーション 再生/変更 
    private void AnimationChange(AnimationPattern pattern)
    {
        Script_SpriteStudio6_Root scriptRoot = null;    // SpriteStudio Anime を操作するためのクラス
        int iTimesPlaey = 0;

        if (m_goCharacter == null)
            return;

        scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(m_goCharacter);
        if (scriptRoot != null)
        {
            switch (pattern)
            {
                case AnimationPattern.ClearScene_Next_Move:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.ClearScene_Next_NotMove:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.GameStart_Move:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.GameStart_NotMove:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Result_Retry_Move:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Result_Retry_NotMove:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Result_StageSelect_Move:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Result_StageSelect_NotMove:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Select_Back_Move:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Select_Back_NotMove:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Title_Credit_Move:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Title_Credit_NotMove:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Title_End_Move:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Title_End_NotMove:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Title_Option_Move:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Title_Option_NotMove:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                default:
                    break;
            }
            scriptRoot.AnimationPlay(-1, (int)pattern, iTimesPlaey);
        }
    }

    // アニメーションが再生中か停止中(エラー含)か取得します
    private bool IsAnimationPlay()
    {
        bool ret = false;

        Script_SpriteStudio6_Root scriptRoot = null;    // SpriteStudio Anime を操作するためのクラス

        if (m_goCharacter != null)
        {
            scriptRoot = Script_SpriteStudio6_Root.Parts.RootGet(m_goCharacter);
            if (scriptRoot != null)
            {
                // 再生回数を取得して、プレイ終了かを判断します
                int Remain = scriptRoot.PlayTimesGetRemain(0);
                if (Remain >= 0)
                    ret = true;
            }
        }

        return ret;
    }
}
