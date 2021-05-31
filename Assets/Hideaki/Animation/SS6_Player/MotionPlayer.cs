using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MotionPlayer : MonoBehaviour
{

    // 再生アニメーションのResourcesフォルダ内のサブパス
    [SerializeField]
    public Object[] AnimationList;


    // キャラクター管理用 
    private GameObject m_goCharacter = null;
    private GameObject m_goCharPos = null;
    private Vector3 m_vecCharacterPos;      // キャラクター位置 
    private Vector3 m_vecCharacterScale;    // キャラクタースケール 

    // 再生アニメーション指定用 
    private enum AnimationPattern : int
    {
        Attack = 0,     // 攻撃 
        Damage = 1,     // ダメージ
        Die = 2,
        Goal = 3,       // ゴール
        Jump = 4,       // ジャンプ
        Lift = 5,
        Lift_Wait = 6,
        Reverse = 7,    // リバース
        Throw = 8,
        Wait = 9,       // 待機
        Walk = 10,       // 歩き
        Walk_Lift = 11,

        Climb = 12,
        Count
    }

    // 処理ステップ用 
    private enum Step : int
    {
        Init = 0,   // 初期化 
        Title,      // タイトル 
        Attack,     // 攻撃
        Damage,     // ダメージ
        Die,        // 死亡
        Goal,       // ゴール
        Jump,       // ジャンプ
        Lift,       // 箱を持つ
        Reverse,    // リバース
        Throw,      // 箱を投げる
        Wait,       // 待機 
        Walk,       // 移動 
        Walk_Lift,  // 箱を持ったまま歩く
        Climb,      // 梯子を登る
        End
    }

    // 処理ステップ管理用 
    private Step m_Step = Step.Init;

    // 汎用
    // いろいろ使いまわす用変数
    private int m_Count = 0;
    private bool m_SW = true;

    // ダメージを受けたかのフラグ
    public bool m_DamagedFlg = false;
    // ダメージを受けてからの秒数    
    float m_DamageDelta = 0.0f;
    //ダメージ後の無敵時間の秒数
    float m_DamageLimit = 1.0f;
    // ダメージを受けてから移動可能になるまでの時間
    float CantMoveRecoveryDeltaLimit = 0.7f;
    // ダメージを受けてから移動できるようになったか
    bool CantMoveRecoveryed = true;
    //ブロックを所持しているか
    public bool m_HavingBlock = false;

    // 梯子に接触しているかのフラグ
    public bool m_LadderFlg = false;

    //プレイヤーが死ぬ座標
    public float m_PlayerDeathPosY = -6.0f;

    ////被ダメージ時に透明にする用のコンポーネント
    //SpriteRenderer spriteRenderer;
    //// ダメージを受ける前の色
    //Color32 beforeColor;

    Rigidbody2D rigid2D;
    Transform transform;
    // プレイヤーの移動速度
    float m_MoveSpeed = 3.0f;
    // ジャンプ時に上方向へかかる力
    float m_JumpForce = 240.0f;
    // 空中にいるか判定するフラグ
    bool m_Flying = false;
    // 梯子につかまっているかのフラグ
    bool m_LadderHanging = false;
    // 最初のスケール
    Vector3 m_FirstScale;
    //宝箱のオブジェクト
    public GameObject GoalObject;
    Vector3 GoalPos;

    // プレイヤーの行動許可
    public static bool m_IsPlay = false;
    // 移動しているかのフラグ
    bool m_Moving = false;

    //UIで管理するのにstaticにしました 3/20
    public static int m_PlayerHp = 3;
    int playerMaxHP = 3;

    //アーマーを装備しているか
    public static bool ArmorUsing;

    //攻撃のクールタイム
    float attackSpan = 0.5f;
    //攻撃を行ってからの時間
    float attackDelta = 0.0f;
    //　今向いている方向
    public int m_LookKey = 1;
    // 攻撃範囲
    public GameObject AttackLange;

    // ポーズ画面用のUI
    public GameObject PauseUI;

    //リバースに使用する画像
    public GameObject rawImage;
    RawImageScript rawImageScript;
    // リバース発動までの時間
    public float m_ReverseCoolTime = 0.4f;
    // 現在のリバース発動経過時間
    float m_ReverseDeltaTime = 0.0f;

    // 当たり判定用のオブジェクト
    public GameObject CollisionObj;
    CollisionScript collisionScript;
    // ChangeWorld
    public GameObject changeWorld;
    ChangeWorld changeWorldScript;

    // SE用のコンポーネント
    AudioSource playerAudio;
    public AudioClip SE_Attack;
    public AudioClip SE_Jump;
    public AudioClip SE_Reverse;
    public AudioClip SE_Damage;
    public AudioClip SE_GetJuwel;
    public AudioClip SE_SwordSwing;
    public AudioClip SE_Cancel;

    bool m_MotionChanged = true;
    int NowMotionPatternNum = 0;
    int NowStepNum = 0;

    // Use this for initialization
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        m_FirstScale = transform.localScale;

        ArmorUsing = GameSystem.GetArmorUsing();

        playerAudio = GetComponent<AudioSource>();

        m_PlayerHp = playerMaxHP;

        rawImageScript = rawImage.GetComponent<RawImageScript>();

        collisionScript = CollisionObj.GetComponent<CollisionScript>();

        changeWorldScript = changeWorld.GetComponent<ChangeWorld>();

        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        // キャラクターパラメータ関連を設定 

        // 座標設定 
        m_vecCharacterPos.x = 0.0f;
        m_vecCharacterPos.y = 1.0f;
        m_vecCharacterPos.z = 0.0f;

        // スケール設定 
        m_vecCharacterScale.x = 0.01f;
        m_vecCharacterScale.y = 0.01f;
        m_vecCharacterScale.z = 0.01f;

    }

    // Update is called once per frame
    void Update()
    {
        AnimationBehaviour();
    }

    void AnimationBehaviour()
    {

        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        //ゴールなどプレイを中断させたい時のフラグ
        if (m_IsPlay == true && rawImageScript.GetChanggingFlg() == false )
        {
            switch (m_Step)
            {
                // 初期化
                case Step.Init:
                    m_Count = 0;
                    m_SW = true;
                    //if (collisionScript.LadderFlg == true)
                    //{
                    //    AnimationStartClimb();
                    //    Debug.Log("AnimationStartClimb()");
                    //}
                    //else
                    //{
                        AnimationStartDefault();   // アニメーション開始処理(設定)
                    //}
                    break;
                // タイトル
                case Step.Title:
                    if (++m_Count > 15)
                    {
                        m_SW = !m_SW;
                        m_Count = 0;
                    }
                    if (Input.GetKeyDown(KeyCode.Space) == true)
                    {
                        AnimationStartDefault();   // アニメーション開始処理(設定)
                        m_Step = Step.Wait;
                    }
                    break;
                // 攻撃中 
                case Step.Attack:
                    //攻撃
                    if (attackSpan < attackDelta)
                    {
                        // 待機に変更 
                        AnimationChange(AnimationPattern.Wait);
                        m_Step = Step.Wait;
                        attackDelta = 0.0f;
                        AttackLange.SetActive(false);
                    }
                    else
                    {
                        AttackLange.SetActive(true);
                        attackDelta += Time.deltaTime;
                    }
                    break;
                case Step.Damage:
                    break;
                case Step.Die:
                    if (IsAnimationPlay() == false)
                    {
                        PlayerScript.PlayerAlive = false;
                        Time.timeScale = 0;
                        //SceneManager.LoadScene("GameOver");
                        //Destroy(this.gameObject);
                    }
                    break;
                case Step.Goal:
                    break;
                // ジャンプ
                case Step.Jump:
                    // ジャンプ終了
                    if (m_Flying == false)
                    {
                        AnimationChange(AnimationPattern.Wait);
                        m_Step = Step.Wait;
                    }
                    break;
                case Step.Lift:
                    if (IsAnimationPlay() == false)
                    {
                        AnimationChange(AnimationPattern.Wait);
                        m_Step = Step.Wait;
                    }
                    break;
                case Step.Reverse:
                    if (m_ReverseCoolTime < m_ReverseDeltaTime)
                    {
                        rawImageScript.changgingFlg = true;
                        m_ReverseDeltaTime = 0.0f;
                        AnimationChange(AnimationPattern.Wait);
                        m_Step = Step.Wait;

                    }
                    else
                    {
                        m_ReverseDeltaTime += Time.deltaTime;
                    }
                    break;
                case Step.Throw:
                    if(IsAnimationPlay() == false)
                    {
                        m_Step = Step.Wait;
                        AnimationChange(AnimationPattern.Wait);
                    }
                    break;
                // 待機
                case Step.Wait:
                    if (m_HavingBlock == true)
                    {
                        MotionPatternChange(AnimationPattern.Lift_Wait);
                    }
                    else
                    {
                        MotionPatternChange(AnimationPattern.Wait);
                    }
                    // ダメージを受けていないとき
                    if (CantMoveRecoveryed == true)
                    {
                        if (Input.GetKeyDown(KeyCode.Z) == true)        // 攻撃 
                        {
                            //// 攻撃に変更 
                            //AnimationChange(AnimationPattern.Attack);
                            //m_Step = Step.Attack;
                        }
                        else if (collisionScript.LadderFlg == true)
                        {
                            // 走りに変更 
                            AnimationChange(AnimationPattern.Wait);
                            //Debug.Log("AnimationChange(AnimationPattern.Walk)");
                        }
                        else if (m_Moving == true)
                        {
                            // 走りに変更 
                            AnimationChange(AnimationPattern.Walk);
                            m_Step = Step.Walk;
                            Debug.Log("AnimationChange(AnimationPattern.Walk)");
                        }
                    }   
                    break;
                // 移動 
                case Step.Walk:
                    if (m_HavingBlock == true)
                    {
                        MotionPatternChange(AnimationPattern.Walk_Lift);
                    }
                    else
                    {
                        MotionPatternChange(AnimationPattern.Walk);
                    }
                    // 移動
                    if (CantMoveRecoveryed == true)
                    {
                        if (m_Moving == false)
                        {
                            // 待機に変更 
                            AnimationChange(AnimationPattern.Wait);
                            m_Step = Step.Wait;
                            //Debug.Log("AnimationChange(AnimationPattern.Wait)");
                        }
                        else if (collisionScript.LadderFlg == true)
                        {

                        }

                        //if(collisionScript.LadderFlg == true)
                        //{
                        //    m_goCharPos.transform.localPosition = m_vecCharacterPos;    // 座標反映 
                        //}
                        //else
                        //{
                        //    m_goCharPos.transform.localPosition = m_vecCharacterPos;    // 座標反映 
                        //}
                    }
                    break;
                case Step.Climb:

                    break;
                default:
                    break;
            }

            if(GameSystem.IsGoal == false)
            {
                // ダメージを受けている時の処理・ダメージを受けていない時の処理(移動)
                if (CantMoveRecoveryed == true && m_Step != Step.Throw)
                {
                    // 移動
                    if (Input.GetKey(KeyCode.LeftArrow) == true || hori < -0.3f)   // 左移動 
                    {
                        //m_LookKey = -1;
                        transform.position -= new Vector3(m_MoveSpeed, 0, 0) * Time.deltaTime;
                        transform.localScale = new Vector3(-m_FirstScale.x, m_FirstScale.y, m_FirstScale.z);
                        m_Moving = true;
                    }
                    else if (Input.GetKey(KeyCode.RightArrow) == true || hori > 0.3f)  // 右移動 
                    {
                        //m_LookKey = 1;
                        transform.position += new Vector3(m_MoveSpeed, 0, 0) * Time.deltaTime;
                        transform.localScale = new Vector3(m_FirstScale.x, m_FirstScale.y, m_FirstScale.z);
                        m_Moving = true;
                    }
                    else if (Input.GetKeyDown(KeyCode.C) == true || Input.GetKeyDown("joystick button 3"))
                    {
                        if (m_Step != Step.Reverse)
                        {
                            if (ChangeWorld.CoolDownTime <= 0 && ChangeWorld.StateFront == true)
                            {
                                m_ReverseDeltaTime = 0.0f;
                                playerAudio.PlayOneShot(SE_Reverse);
                                AnimationChange(AnimationPattern.Reverse);
                                m_Step = Step.Reverse;
                            }
                            else if (ChangeWorld.UraActiveTime < 10 && ChangeWorld.StateFront == false)
                            {
                                m_ReverseDeltaTime = 0.0f;
                                AnimationChange(AnimationPattern.Reverse);
                                m_Step = Step.Reverse;
                            }
                            else
                            {
                                playerAudio.PlayOneShot(SE_Cancel);
                            }
                        }
                    }
                    else
                    {
                        m_Moving = false;
                    }

                    // 空中にいるかの判定
                    float speedY = Mathf.Abs(this.rigid2D.velocity.y);
                    if (speedY <= 0.01f)
                    {
                        m_Flying = false;
                    }
                    else
                    {
                        m_Flying = true;
                    }

                    // ジャンプ処理
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("joystick button 0"))
                    {
                        if (m_Flying == false && collisionScript.LadderFlg == false)
                        {
                            this.rigid2D.AddForce(transform.up * 240.0f);
                            playerAudio.PlayOneShot(SE_Jump);
                            if (m_HavingBlock == false)
                            {
                                m_Step = Step.Jump;
                                AnimationChange(AnimationPattern.Jump);
                            }
                            m_Flying = true;
                        }
                        // 梯子に触れている時に捕まる
                        else if (collisionScript.LadderFlg == true)
                        {
                            m_LadderFlg = true;
                        }
                    }

                    //梯子を上り下りする
                    if (collisionScript.LadderFlg == true)
                    {
                        rigid2D.gravityScale = 0;
                        rigid2D.velocity = new Vector2(0.0f, 0.0f);
                        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || vert > 0)
                        {
                            this.transform.position += new Vector3(0.0f, 2.0f * Time.deltaTime, 0.0f);
                        }
                        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.DownArrow) || vert < 0)
                        {
                            this.transform.position += new Vector3(0.0f, -2.0f * Time.deltaTime, 0.0f);
                        }
                        //else
                        //{
                        //m_Step = Step.Wait;
                        //MotionPatternChange(AnimationPattern.Wait);
                        //    //AnimationStartClimb();
                        //}
                    }
                    else
                    {
                        rigid2D.gravityScale = 1;
                    }

                    // 攻撃処理
                    if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown("joystick button 1"))
                    {
                        if (m_HavingBlock == false)
                        {
                            if (m_Step != Step.Attack)
                            {
                                playerAudio.PlayOneShot(SE_SwordSwing);
                                AnimationChange(AnimationPattern.Attack);
                                m_Step = Step.Attack;
                            }
                        }
                        else
                        {
                            playerAudio.PlayOneShot(SE_Cancel);
                        }
                    }

                }

                if (m_DamagedFlg == true)
                {
                    if (m_DamageLimit < m_DamageDelta)
                    {
                        m_DamagedFlg = false;
                        m_DamageDelta = 0.0f;
                        // 待機に変更 
                        if (0 < m_PlayerHp)
                        {
                            AnimationChange(AnimationPattern.Wait);
                            m_Step = Step.Wait;
                        }

                    }
                    else
                    {
                        m_DamageDelta += Time.deltaTime;
                        if (CantMoveRecoveryDeltaLimit < m_DamageDelta)
                        {
                            if (m_PlayerHp <= 0.0f)
                            {
                                m_Step = Step.Die;
                                AnimationChange(AnimationPattern.Die);
                                Debug.Log("AnimationChange(AnimationPattern.Die)");
                            }
                            else
                            {
                                CantMoveRecoveryed = true;
                            }
                        }
                    }
                }

                //穴に落ちた時の処理
                if (this.transform.position.y <= m_PlayerDeathPosY)
                {
                    SceneManager.LoadScene("GameOver");
                    Destroy(this.gameObject);
                }
            }
        }

        //if (collisionScript.LadderFlg == true)
        //{
        //    // 走りに変更 
        //    AnimationChange(AnimationPattern.Count);
        //    //Debug.Log("AnimationChange(AnimationPattern.Walk)");
        //}

        // ゴールに触れていたら
        if (GameSystem.IsGoal == true)
        {
            AnimationChange(AnimationPattern.Goal);
            m_Step = Step.Goal;
        }
    }

    // アニメーション開始 
    private void AnimationStartDefault()
    {
        Script_SpriteStudio6_Root scriptRoot = null;    // SpriteStudio Anime を操作するためのクラス
        int listLength = AnimationList.Length;

        // すでにアニメーション生成済 or リソース設定無い場合はreturn
        if (m_goCharacter != null || listLength < 2)
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
                        m_goCharPos.name = "DefaultPlayer";

                        // 座標設定 
                        m_goCharacter.transform.parent = m_goCharPos.transform;

                        // 自分の子に移動して座標を設定
                        m_goCharPos.transform.parent = this.transform;
                        m_goCharPos.transform.localPosition = m_vecCharacterPos;
                        m_goCharPos.transform.localRotation = Quaternion.identity;
                        m_goCharPos.transform.localScale = m_vecCharacterScale;

                        //アニメーション再生
                        m_Step = Step.Wait;
                        AnimationChange(AnimationPattern.Wait);
                    }
                }
            }
        }
    }

    // アニメーション開始 
    private void AnimationStartClimb()
    {
        Script_SpriteStudio6_Root scriptRoot = null;    // SpriteStudio Anime を操作するためのクラス
        int listLength = AnimationList.Length;

        // すでにアニメーション生成済 or リソース設定無い場合はreturn
        if (m_goCharacter != null || listLength < 2)
            return;

        // 再生するリソース名をリストから取得して再生する
        Object resourceObject = AnimationList[1];
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
                        m_goCharPos.name = "ClimbPlayer";

                        // 座標設定 
                        m_goCharacter.transform.parent = m_goCharPos.transform;

                        // 自分の子に移動して座標を設定
                        m_goCharPos.transform.parent = this.transform;
                        m_goCharPos.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                        m_goCharPos.transform.localRotation = Quaternion.identity;
                        m_goCharPos.transform.localScale = m_vecCharacterScale;

                        //アニメーション再生
                        m_Step = Step.Climb;
                        AnimationChange(AnimationPattern.Climb);
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
        if(collisionScript.LadderFlg == false)
        {
            if (scriptRoot != null)
            {
                switch (pattern)
                {
                    case AnimationPattern.Attack:
                        iTimesPlaey = 1;    // 1回だけ再生 
                        break;
                    case AnimationPattern.Damage:
                        iTimesPlaey = 1;
                        break;
                    case AnimationPattern.Die:
                        iTimesPlaey = 1;
                        break;
                    case AnimationPattern.Goal:
                        iTimesPlaey = 0;
                        break;
                    case AnimationPattern.Jump:
                        iTimesPlaey = 1;    // ループ再生 
                        break;
                    case AnimationPattern.Lift:
                        iTimesPlaey = 1;
                        break;
                    case AnimationPattern.Reverse:
                        iTimesPlaey = 1;
                        break;
                    case AnimationPattern.Throw:
                        iTimesPlaey = 1;
                        break;
                    case AnimationPattern.Wait:
                        iTimesPlaey = 0;    // ループ再生 
                        break;
                    case AnimationPattern.Walk:
                        iTimesPlaey = 0;    // ループ再生 
                        break;
                    case AnimationPattern.Walk_Lift:
                        iTimesPlaey = 0;    // ループ再生 
                        break;
                    default:
                        break;
                }
                scriptRoot.AnimationPlay(-1, (int)pattern, iTimesPlaey);
            }
        }
        //else
        //{
        //    if (scriptRoot != null)
        //    {
        //        switch (pattern)
        //        {
        //            case AnimationPattern.Climb:
        //                iTimesPlaey = 0;
        //                break;
        //            default:
        //                break;
        //        }
        //        scriptRoot.AnimationPlay(-1, 0, iTimesPlaey);
        //    }

        //}
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
    
    public void SetMoveSpeed(float moveSpeed)
    {
        m_MoveSpeed = moveSpeed;
    }

    void MotionPatternChange(AnimationPattern pattern)
    {
        if (NowMotionPatternNum != (int)pattern)
        {
            NowMotionPatternNum = (int)pattern;
            AnimationChange(pattern);
        }
    }

    public void ThrowBlock()
    {
        m_Step = Step.Throw;
        MotionPatternChange(AnimationPattern.Throw);
    }

    public bool GetThrowing()
    {
        if(m_Step == Step.Throw)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if(0 < m_PlayerHp)
            {
                Damage();
            }
        }
    }

    public static bool GetPlayerArriving()
    {
        if(0 < m_PlayerHp)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Damage()
    {
        if (m_DamagedFlg == false)
        {
            if (GameSystem.ArmorUsing == true)
            {
                GameSystem.ArmorUsing = false;
            }
            else
            {
                m_PlayerHp -= 1;
            }
            playerAudio.PlayOneShot(SE_Damage);
            AnimationChange(AnimationPattern.Damage);
            m_Step = Step.Damage;
            m_DamagedFlg = true;
            CantMoveRecoveryed = false;
            var rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = Vector3.zero;
            rigidbody2D.AddForce(new Vector3(-transform.localScale.x * 120.0f, 200.0f, 0.0f));
        }
    }

    public static void SetIsPlay(bool isPlay)
    {
        m_IsPlay = isPlay;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Juwel")
        {
            Debug.Log("ぶつかった");
            playerAudio.PlayOneShot(SE_GetJuwel);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

}