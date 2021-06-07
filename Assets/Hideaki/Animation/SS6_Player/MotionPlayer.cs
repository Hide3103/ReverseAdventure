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
        End
    }

    // 処理ステップ管理用 
    private Step m_Step = Step.Init;

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
    //落ちて死んだか
    bool m_FallDied = false;
    // 死んだモーションが終了したか
    static bool m_DieMotionEnd = false;

    //物理演算コンポーネント
    Rigidbody2D m_Rigid2D;
    // プレイヤーの移動速度
    float m_MoveSpeed = 3.0f;
    // ジャンプ時に上方向へかかる力
    float m_JumpForce = 240.0f;
    // 空中にいるか判定するフラグ
    bool m_Flying = false;
    // Y軸移動の速度
    float m_MoveSpeedLimitY = 0.1f;
    // 梯子につかまっているかのフラグ
    bool m_LadderHanging = false;
    // 最初のスケール
    Vector3 m_FirstScale;
    //宝箱のオブジェクト
    public GameObject m_GoalObject;
    Vector3 m_GoalPos;

    // プレイヤーの行動許可
    public static bool m_IsPlay = false;
    // 移動しているかのフラグ
    bool m_Moving = false;
    // 移動する時のスティックの傾きを感知する値
    float m_StickTilt = 0.3f;

    //UIで管理するのにstaticにしました 3/20
    public static int m_PlayerHp = 3;
    int m_PlayerMaxHP = 3;

    //アーマーを装備しているか
    public static bool m_ArmorUsing;

    //攻撃のクールタイム
    float m_AttackSpan = 0.5f;
    //攻撃を行ってからの時間
    float m_AttackDelta = 0.0f;
    //　今向いている方向
    public int m_LookKey = 1;
    // 攻撃範囲
    public GameObject m_AttackLange;

    // ポーズ画面用のUI
    public GameObject m_PauseUI;

    //リバースに使用する画像
    public GameObject m_RawImage;
    RawImageScript m_RawImageScript;
    // リバース発動までの時間
    public float m_ReverseCoolTime = 0.4f;
    // 現在のリバース発動経過時間
    float m_ReverseDeltaTime = 0.0f;

    // 当たり判定用のオブジェクト
    public GameObject m_CollisionObj;
    // 当たり判定用のオブジェクトのスクリプト
    CollisionScript m_CollisionScript;
    // リバースを制御するオブジェクト
    public GameObject m_ChangeWorld;
    // リバースを制御するオブジェクトのスクリプト
    ChangeWorld m_ChangeWorldScript;

    // SE用のコンポーネント
    AudioSource playerAudio;
    // 登録するSE（攻撃、ジャンプ、リバース、ダメージ、宝石獲得、剣を振る、キャンセル）
    public AudioClip SE_Attack;
    public AudioClip SE_Jump;
    public AudioClip SE_Reverse;
    public AudioClip SE_Damage;
    public AudioClip SE_GetJuwel;
    public AudioClip SE_SwordSwing;
    public AudioClip SE_Cancel;
    
    // モーションパターンの番号を登録しておく変数
    int NowMotionPatternNum = 0;

    // 最初に読み込む関数
    void Start()
    {
        // 物理演算コンポーネント
        this.m_Rigid2D = GetComponent<Rigidbody2D>();
        // 最初のスケール
        m_FirstScale = transform.localScale;
        // アーマーを装備しているか
        m_ArmorUsing = GameSystem.GetArmorUsing();
        // オーディオコンポーネントを変数へ代入する
        playerAudio = GetComponent<AudioSource>();
        // プレイヤーの体力を最大値に設定する
        m_PlayerHp = m_PlayerMaxHP;
        // RawImageのスクリプトを変数へ代入する
        m_RawImageScript = m_RawImage.GetComponent<RawImageScript>();
        // CollisionObjectのスクリプトを変数へ代入する
        m_CollisionScript = m_CollisionObj.GetComponent<CollisionScript>();
        // GameSystemオブジェクトのChangeWorldスクリプトを変数へ代入する
        m_ChangeWorldScript = m_ChangeWorld.GetComponent<ChangeWorld>();
        // プレイヤーに付いている画像コンポーネント(モーションとは別)の透明度をゼロにする
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

        // 死んだモーションが終わったかのフラグをfalseにする
        m_DieMotionEnd = false;

    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの操作
        MoveBehaviour();
        // アニメーションの更新処理
        AnimationBehaviour();
    }

    void AnimationBehaviour()
    {
        //ゴールなどプレイを中断させたい時のフラグ
        if (m_IsPlay == true && m_RawImageScript.GetChanggingFlg() == false)
        {
            switch (m_Step)
            {
                // 初期化
                case Step.Init:
                    AnimationStartDefault();   // アニメーション開始処理(設定)
                    break;
                // タイトル
                case Step.Title:
                    if (Input.GetKeyDown(KeyCode.Space) == true)
                    {
                        AnimationStartDefault();   // アニメーション開始処理(設定)
                        m_Step = Step.Wait;
                    }
                    break;
                // 攻撃中 
                case Step.Attack:
                    //攻撃
                    if (m_AttackSpan < m_AttackDelta)
                    {
                        // 待機に変更 
                        AnimationChange(AnimationPattern.Wait);
                        m_Step = Step.Wait;
                        m_AttackDelta = 0.0f;
                        m_AttackLange.SetActive(false);
                    }
                    else
                    {
                        m_AttackLange.SetActive(true);
                        m_AttackDelta += Time.deltaTime;
                    }
                    break;
                case Step.Damage:
                    break;
                case Step.Die:
                    if (IsAnimationPlay() == false)
                    {
                        m_DieMotionEnd = true;
                        Time.timeScale = 0;
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
                        m_RawImageScript.changgingFlg = true;
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
                    if (IsAnimationPlay() == false)
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
                        if (m_CollisionScript.m_LadderFlg == true)
                        {
                            // 走りに変更 
                            AnimationChange(AnimationPattern.Wait);
                        }
                        else if (m_Moving == true)
                        {
                            // 走りに変更 
                            AnimationChange(AnimationPattern.Walk);
                            m_Step = Step.Walk;
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
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    void MoveBehaviour()
    {
        if(GameSystem.IsGoal == false && m_Step != Step.Die)
        {
            // ダメージを受けている時の処理・ダメージを受けていない時の処理(移動)
            if (CantMoveRecoveryed == true && m_Step != Step.Throw)
            {
                float hori = Input.GetAxis("Horizontal");
                float vert = Input.GetAxis("Vertical");
                // 移動
                if (Input.GetKey(KeyCode.LeftArrow) == true || hori < -m_StickTilt)   // 左移動 
                {
                    //m_LookKey = -1;
                    transform.position -= new Vector3(m_MoveSpeed, 0, 0) * Time.deltaTime;
                    transform.localScale = new Vector3(-m_FirstScale.x, m_FirstScale.y, m_FirstScale.z);
                    m_Moving = true;
                }
                else if (Input.GetKey(KeyCode.RightArrow) == true || hori > m_StickTilt)  // 右移動 
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
                float speedY = Mathf.Abs(this.m_Rigid2D.velocity.y);
                if (speedY <= m_MoveSpeedLimitY)
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
                    if (m_Flying == false && m_CollisionScript.m_LadderFlg == false)
                    {
                        this.m_Rigid2D.AddForce(transform.up * 240.0f);
                        playerAudio.PlayOneShot(SE_Jump);
                        if (m_HavingBlock == false)
                        {
                            m_Step = Step.Jump;
                            AnimationChange(AnimationPattern.Jump);
                        }
                        m_Flying = true;
                    }
                    // 梯子に触れている時に捕まる
                    else if (m_CollisionScript.m_LadderFlg == true)
                    {
                        m_LadderFlg = true;
                    }
                }

                //梯子を上り下りする
                if (m_CollisionScript.m_LadderFlg == true)
                {
                    m_Rigid2D.gravityScale = 0;
                    m_Rigid2D.velocity = new Vector2(0.0f, 0.0f);
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
                    m_Rigid2D.gravityScale = 1;
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
                if(m_FallDied == false)
                {
                    m_PlayerHp = 0;
                    m_Step = Step.Die;
                    AnimationChange(AnimationPattern.Die);
                    m_FallDied = true;
                }
            }
        }
        
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

                        ////アニメーション再生
                        //m_Step = Step.Climb;
                        //AnimationChange(AnimationPattern.Climb);
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
        if(m_CollisionScript.m_LadderFlg == false)
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

    public static bool GetDieMotionEnd()
    {
        if(m_DieMotionEnd == true)
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