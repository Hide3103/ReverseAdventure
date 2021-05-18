using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionBridge : MonoBehaviour
{

    // 再生アニメーションのResourcesフォルダ内のサブパス
    [SerializeField]
    public Object[] AnimationList;

    // 再生アニメーション指定用 
    private enum AnimationPattern : int
    {
        Extend_back = 0,
        Extend_front = 1,
        Motion1_back = 2,
        Motion1_front = 3,
        Round_back = 4,
        Round_front = 5,
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
        Round,
        Motion,
        Extend,
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

    // Use this for initialization
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        // キャラクターパラメータ関連を設定 

        // 座標設定 
        m_vecCharacterPos.x = 1.3f;
        m_vecCharacterPos.y = 1.3f;
        m_vecCharacterPos.z = 0.0f;

        // スケール設定 
        m_vecCharacterScale.x = 0.01f;
        m_vecCharacterScale.y = 0.01f;
        m_vecCharacterScale.z = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_Step)
        {
            // 初期化
            case Step.Init:
                m_Count = 0;
                m_SW = true;
                m_Step = Step.Round;
                AnimationStart();   // アニメーション開始処理(設定)
                break;
            // 待機
            case Step.Round:
                if (MotionFlg == true)        // 攻撃 
                {
                    // 攻撃に変更 
                    AnimationChange(AnimationPattern.Motion1_front);
                    m_Step = Step.Motion;
                    Debug.Log("Z押された"); 
                }
                else
                {
                    if(ChangeWorld.StateFront == true)
                    {
                        AnimationChange(AnimationPattern.Round_front);
                    }
                    else
                    {
                        AnimationChange(AnimationPattern.Round_back);
                    }
                }
                break;
            // 移動 
            case Step.Motion:
                if(MotionLimit < MotionDelta)
                {
                    AnimationChange(AnimationPattern.Extend_front);
                    m_Step = Step.Extend;
                    MotionDelta = 0.0f;
                }
                else
                {
                    MotionDelta += Time.deltaTime;
                }
                break;
            case Step.Extend:
                break;
            default:
                break;
        }

        ColliderChange();
    }

    void ColliderChange()
    {
        switch (m_Step)
        {
            case Step.Round:
                Debug.Log("コリジョン変化前");
                boxCollider2D.offset = new Vector2(0, 0);
                boxCollider2D.size = new Vector2(0.6f, 0.6f);
                break;
            case Step.Motion:
                Debug.Log("コリジョン変化中");
                float deltaOffsetX = 1.23f * Time.deltaTime;
                float deltaSizeX = (2.55f - 0.6f) * Time.deltaTime;
                boxCollider2D.offset = new Vector2(boxCollider2D.offset.x + deltaOffsetX, -0.255f);
                boxCollider2D.size = new Vector2(boxCollider2D.size.x + deltaSizeX, 0.1f);

                break;
            case Step.Extend:
                Debug.Log("コリジョン変化後");
                boxCollider2D.offset = new Vector2(1.23f, -0.255f);
                boxCollider2D.size = new Vector2(2.55f, 0.1f);
                break;

            default:
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            if(ChangeWorld.StateFront == false)
            {
                MotionFlg = true;
            }
        }
    }

    private void OnGUI()
    {
        // GUI変更
        GUIStyle guiStyle = new GUIStyle();
        GUIStyleState styleState = new GUIStyleState();

        switch (m_Step)
        {
            // タイトル
            case Step.Init:
                if (m_SW == true)
                {
                    styleState.textColor = Color.black; // 文字色 黒 
                    guiStyle.normal = styleState;       // スタイルの設定。
                    GUI.Label(new Rect(420, 180, 100, 50), "PRESS SPACE", guiStyle);
                }
                break;
            default:
                break;
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
                        m_goCharPos.name = "Comipo";

                        // 座標設定 
                        m_goCharacter.transform.parent = m_goCharPos.transform;

                        // 自分の子に移動して座標を設定
                        m_goCharPos.transform.parent = this.transform;
                        m_goCharPos.transform.localPosition = m_vecCharacterPos;
                        m_goCharPos.transform.localRotation = Quaternion.identity;
                        m_goCharPos.transform.localScale = m_vecCharacterScale;

                        //アニメーション再生
                        AnimationChange(AnimationPattern.Round_front);
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
                case AnimationPattern.Extend_back:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Extend_front:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Motion1_back:
                    iTimesPlaey = 1;    // 1回だけ再生 
                    break;
                case AnimationPattern.Motion1_front:
                    iTimesPlaey = 1;    // 1回だけ再生 
                    break;
                case AnimationPattern.Round_back:
                    iTimesPlaey = 1;    // 1回だけ再生 
                    break;
                case AnimationPattern.Round_front:
                    iTimesPlaey = 1;    // 1回だけ再生 
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
