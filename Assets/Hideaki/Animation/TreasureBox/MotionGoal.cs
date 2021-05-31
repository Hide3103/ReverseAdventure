using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionGoal : MonoBehaviour
{
    // 再生アニメーションのResourcesフォルダ内のサブパス
    [SerializeField]
    public Object[] AnimationList;

    // 再生アニメーション指定用 
    private enum AnimationPattern : int
    {
        Close,
        Openning,
        Open,
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
        Close,
        Openning,
        Open,
        End
    }

    // 処理ステップ管理用 
    private Step m_Step = Step.Init;

    // 汎用
    // いろいろ使いまわす用変数
    private int m_Count = 0;
    private bool m_SW = true;

    float MotionLimit = 1.0f;
    float MotionDelta = 0.0f;

    public bool MotionFlg = false;

    BoxCollider2D boxCollider2D;

    //public GameObject MainCamera;
    //public GameObject GoalCamera;

    SpriteRenderer spriteRenderer;

    public GameObject Result;

    // Start is called before the first frame update
    void Start()
    {
        //GoalCamera.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        boxCollider2D = GetComponent<BoxCollider2D>();

        // キャラクターパラメータ関連を設定 

        // 座標設定 
        m_vecCharacterPos.x = 0.0f;
        m_vecCharacterPos.y = -0.1f;
        m_vecCharacterPos.z = 0.0f;

        // スケール設定 
        m_vecCharacterScale.x = 0.024f;
        m_vecCharacterScale.y = 0.024f;
        m_vecCharacterScale.z = 0.024f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameSystem.IsGoal == true)
        //{
        //    spriteRenderer.sprite = open;

        //    StayTime += Time.unscaledDeltaTime;
        //    if (StayTime > 3)
        //    {
        //        //GoalCamera.SetActive(true);
        //        //MainCamera.SetActive(false);
        //        Result.SetActive(true);
        //    }
        //}
        //else
        //{
        //    spriteRenderer.sprite = close;
        //}
        MotionBehaviour();
    }

    void MotionBehaviour()
    {
        switch (m_Step)
        {
            // 初期化
            case Step.Init:
                m_Step = Step.Close;
                AnimationStart();   // アニメーション開始処理(設定)
                break;
            // 待機
            case Step.Close:
                if (GameSystem.IsGoal == true)        // 攻撃 
                {
                    // 攻撃に変更 
                    AnimationChange(AnimationPattern.Openning);
                    m_Step = Step.Openning;
                }
                //else
                //{
                //    if (ChangeWorld.StateFront == true)
                //    {
                //        AnimationChange(AnimationPattern.Round_front);
                //    }
                //    else
                //    {
                //        AnimationChange(AnimationPattern.Round_back);
                //    }
                //}
                break;
            // 移動 
            case Step.Openning:
                if (IsAnimationPlay() == false)
                {
                    AnimationChange(AnimationPattern.Open);
                    m_Step = Step.Open;
                }
                break;
            case Step.Open:
                if(MotionLimit < MotionDelta)
                {
                    Result.SetActive(true);
                }
                else
                {
                    MotionDelta += Time.deltaTime;
                }
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameSystem.IsGoal = true;
            //Time.timeScale = 0;
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
                        m_goCharPos.name = "Goal";

                        // 座標設定 
                        m_goCharacter.transform.parent = m_goCharPos.transform;

                        // 自分の子に移動して座標を設定
                        m_goCharPos.transform.parent = this.transform;
                        m_goCharPos.transform.localPosition = m_vecCharacterPos;
                        m_goCharPos.transform.localRotation = Quaternion.identity;
                        m_goCharPos.transform.localScale = m_vecCharacterScale;

                        //アニメーション再生
                        AnimationChange(AnimationPattern.Close);
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
                case AnimationPattern.Close:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Openning:
                    iTimesPlaey = 1;    // ループ再生 
                    break;
                case AnimationPattern.Open:
                    iTimesPlaey = 0;    // 1回だけ再生 
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
