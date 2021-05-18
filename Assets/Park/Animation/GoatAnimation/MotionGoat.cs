﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionGoat : MonoBehaviour
{
    GameObject enemy;
    GameObject player;
    public GameObject lookPlayer;
    public GameObject lostPlayer;

    Transform enemyCollider;
    Transform playerCollider;

    //public Sprite frontSprite;
    //public Sprite backSprite;

    public AudioClip attack;

    Rigidbody2D rigid2D;

    Vector2 firstScale;

    //SpriteRenderer spriteRenderer;

    //Color32 beforeColor;

    AudioSource audioSource;

    bool flg_normal;
    bool flg_lookPlayer;
    public static bool flg_moveToPlayer;
    bool flg_lostPlayer;
    bool flg_danger;
    bool flg_damage;
    bool flg_blinking;
    bool flg_attackPlayer;
    bool flg_attackSound;

    float m_hp;
    float m_systemHp;
    float m_speed;
    public static float m_direction;
    float m_moveToplayerTime;
    float m_lostPlayerTime;
    float m_damageDelta;
    float m_damageLimit;
    float m_attackTime;
    // 再生アニメーションのResourcesフォルダ内のサブパス
    [SerializeField]
    public Object[] AnimationList;

    // 再生アニメーション指定用 
    private enum AnimationPattern : int
    {
        Chase_Back = 0,
        Chase_Front = 1,
        Damage_Back = 2,
        Damage_Front = 3,
        Discovery_Back = 4,
        Discovery_Front = 5,
        Walk_Back = 6,
        Walk_Front = 7,
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
        Chase_Back = 1,
        Chase_Front = 2,
        Damage_Back = 3,
        Damage_Front = 4,
        Discovery_Back = 5,
        Discovery_Front = 6,
        Walk_Back = 7,
        Walk_Front = 8,
        End
    }

    // 処理ステップ管理用 
    private Step m_Step = Step.Init;

    // 汎用
    // いろいろ使いまわす用変数
    private int m_Count = 0;
    private bool m_SW = true;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        enemyCollider = enemy.transform;
        playerCollider = player.transform;

        this.rigid2D = GetComponent<Rigidbody2D>();

        this.firstScale = transform.localScale;

        //spriteRenderer = GetComponent<SpriteRenderer>();
        //beforeColor = spriteRenderer.color;

        audioSource = GetComponent<AudioSource>();

        Initialize();

        // キャラクターパラメータ関連を設定 

        // 座標設定 
        m_vecCharacterPos.x = 0.0f;
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
        rigid2D.constraints = RigidbodyConstraints2D.FreezeRotation; //ローテーション固定

        if (player && GameSystem.IsGoal == false)
        {
            Transform playerTrans = playerCollider as Transform;

            if (m_systemHp > 0)
            {
                Move();
                MoveToPlayer();
            }

            //Damage();
           // Die();
            //SystemDie();
            //ChangeSprite();

            Physics2D.IgnoreCollision(playerTrans.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        if (enemy)
        {
            Transform enemyTrans = enemyCollider as Transform;

            Physics2D.IgnoreCollision(enemyTrans.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        switch (m_Step)
        {
            // 初期化
            case Step.Init:
                m_Count = 0;
                m_SW = true;
                m_Step = Step.Walk_Front;
                AnimationStart();
                break;
            // タイトル
            case Step.Walk_Front:
                break;
            //// 待機
            //case Step.Wait:
            //    if (Input.GetKeyDown(KeyCode.Z) == true)        // 攻撃 
            //    {
            //        // 攻撃に変更 
            //        AnimationChange(AnimationPattern.Attack);
            //        m_Step = Step.Attack;
            //    }
            //    else if (Input.GetKeyDown(KeyCode.LeftArrow) == true)   // 左移動 
            //    {
            //        if (m_vecCharacterScale.x < 0)
            //            m_vecCharacterScale.x *= -1;    // 左向きにします
            //        m_goCharPos.transform.localScale = m_vecCharacterScale; // 向き設定 
            //        // 走りに変更 
            //        AnimationChange(AnimationPattern.Run);
            //        m_Step = Step.Move;
            //    }
            //    else if (Input.GetKeyDown(KeyCode.RightArrow) == true)  // 右移動 
            //    {
            //        if (m_vecCharacterScale.x > 0)
            //            m_vecCharacterScale.x *= -1;    // 右向きにします
            //        m_goCharPos.transform.localScale = m_vecCharacterScale; // 向き設定 
            //        // 走りに変更 
            //        AnimationChange(AnimationPattern.Run);
            //        m_Step = Step.Move;
            //    }
            //    break;
            //// 移動 
            //case Step.Move:
            //    if (Input.GetKey(KeyCode.LeftArrow) == true)   // 左移動 
            //    {
            //        if (m_vecCharacterPos.x > -560.0f)
            //            m_vecCharacterPos.x -= 10.0f;
            //    }
            //    else if (Input.GetKey(KeyCode.RightArrow) == true)  // 右移動 
            //    {
            //        if (m_vecCharacterPos.x < 560.0f)
            //            m_vecCharacterPos.x += 10.0f;
            //    }
            //    else
            //    {
            //        // 待機に変更 
            //        AnimationChange(AnimationPattern.Wait);
            //        m_Step = Step.Wait;
            //    }
            //    m_goCharPos.transform.localPosition = m_vecCharacterPos;    // 座標反映 
            //    break;
            //// 攻撃中 
            //case Step.Attack:
            //    if (IsAnimationPlay() == false)
            //    {
            //        // 待機に変更 
            //        AnimationChange(AnimationPattern.Wait);
            //        m_Step = Step.Wait;
            //    }
            //    break;
            default:
                break;
        }
    }

    private void OnGUI()
    {
        // GUI変更
        GUIStyle guiStyle = new GUIStyle();
        GUIStyleState styleState = new GUIStyleState();

        //switch (m_Step)
        //{
        //    // タイトル
        //    case Step.Title:
        //        if (m_SW == true)
        //        {
        //            styleState.textColor = Color.black; // 文字色 黒 
        //            guiStyle.normal = styleState;       // スタイルの設定。
        //            GUI.Label(new Rect(420, 180, 100, 50), "PRESS SPACE", guiStyle);
        //        }
        //        break;
        //    default:
        //        break;
        //}
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
                        AnimationChange(AnimationPattern.Walk_Front);
                        Debug.Log("AnimationChange(AnimationPattern.Walk_Front);");
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
                case AnimationPattern.Chase_Back:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Chase_Front:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Damage_Back:
                    iTimesPlaey = 1;    // 1回だけ再生 
                    break;
                case AnimationPattern.Damage_Front:
                    iTimesPlaey = 1;    // 1回だけ再生 
                    break;
                case AnimationPattern.Discovery_Back:
                    iTimesPlaey = 1;    // ループ再生 
                    break;
                case AnimationPattern.Discovery_Front:
                    iTimesPlaey = 1;    // ループ再生 
                    break;
                case AnimationPattern.Walk_Back:
                    iTimesPlaey = 0;    // ループ再生 
                    break;
                case AnimationPattern.Walk_Front:
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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //"Sword"に当たった時にフラグ切り替え＆ノックバック
    //    if (collision.gameObject.tag == "Sword")
    //    {
    //        if (flg_blinking == false && ChangeWorld.StateFront)
    //        {
    //            flg_damage = true;

    //            rigid2D.AddForce(new Vector3(transform.localScale.x * 50.0f, 50.0f, 0.0f));

    //            m_systemHp--;
    //        }
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        //flg_moveToPlayer = false;
    //        flg_attackPlayer = true;
    //    }
    //}

    void Initialize()
    {
        flg_normal = true;
        flg_lookPlayer = false;
        flg_moveToPlayer = false;
        flg_lostPlayer = false;
        flg_danger = false;
        flg_damage = false;
        flg_blinking = false;
        flg_attackPlayer = false;
        flg_attackSound = false;

        m_hp = 1.0f;
        m_systemHp = m_hp;
        m_direction = -1.0f;
        m_speed = 0.5f;
        m_moveToplayerTime = 0.0f;
        m_lostPlayerTime = 0.0f;
        m_damageDelta = 0.0f;
        m_damageLimit = 1.0f;
        m_attackTime = 0.0f;
    }

    void Move()
    {
        Vector2 Position = transform.position;
        Transform target = player.transform;
        Vector2 direction = (target.position - transform.position).normalized;

        float accelaration = 2.0f; //速度
        float velocity = (accelaration * Time.deltaTime); //速度を秒速に変える
        float distance = Vector2.Distance(target.position, transform.position);

        if (flg_normal)
        {
            transform.localScale = new Vector3(firstScale.x, firstScale.y);

            Position.x += m_speed * m_direction * Time.deltaTime;

            transform.position = Position;

            NormalDirection();

            lookPlayer.SetActive(false);
            lostPlayer.SetActive(false);
        }

        if (flg_lookPlayer)
        {
            Direction();

            lookPlayer.SetActive(true);
            lostPlayer.SetActive(false);

            m_moveToplayerTime += Time.deltaTime;

            if (m_moveToplayerTime >= 1.0f)
            {
                if (flg_attackSound == false)
                {
                    audioSource.PlayOneShot(attack);

                    flg_attackSound = true;
                }

                flg_attackSound = false;

                m_moveToplayerTime = 0.0f;

                flg_moveToPlayer = true;
                flg_lookPlayer = false;
            }
        }

        if (flg_moveToPlayer)
        {
            if (player)
            {
                Direction();

                this.transform.position = new Vector2(transform.position.x + (direction.x * velocity),
                                          transform.position.y + (direction.y * velocity)); //追いかける
            }
        }

        if (flg_lostPlayer)
        {
            lostPlayer.SetActive(true);
            lookPlayer.SetActive(false);

            m_lostPlayerTime += Time.deltaTime;

            if (m_lostPlayerTime >= 5.0f)
            {
                m_lostPlayerTime = 0.0f;

                flg_normal = true;
                flg_lookPlayer = false;
                flg_moveToPlayer = false;
                flg_lostPlayer = false;
                flg_danger = false;
            }
        }

        if (flg_attackPlayer)
        {
            m_attackTime += Time.deltaTime;

            flg_moveToPlayer = false;

            if (m_attackTime >= 1.0f)
            {
                flg_moveToPlayer = true;
                flg_attackPlayer = false;

                m_attackTime = 0.0f;
                //
            }
        }
    }

    void NormalDirection()
    {
        if (m_direction == 1.0f)
        {
            transform.localScale = new Vector3(-firstScale.x, firstScale.y);
        }
        else if (m_direction == -1.0f)
        {
            transform.localScale = new Vector3(firstScale.x, firstScale.y);
        }
    }

    void MoveToPlayer()
    {
        Transform target = player.transform;
        float distance = Vector2.Distance(target.position, transform.position);

        if (distance <= 4.0f && flg_moveToPlayer == false)
        {
            flg_normal = false;
            flg_lookPlayer = true;
            flg_lostPlayer = false;
            flg_danger = true;

            m_lostPlayerTime = 0.0f;
        }
        else if (distance > 4.0f && flg_danger)
        {
            flg_lookPlayer = false;
            flg_moveToPlayer = false;
            flg_lostPlayer = true;

            m_moveToplayerTime = 0.0f;
        }
    }

    //void Damage()
    //{
    //    if (flg_damage)
    //    {
    //        if (m_damageDelta < m_damageLimit)
    //        {
    //            float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
    //            //gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
    //            m_damageDelta += Time.deltaTime;

    //            flg_blinking = true;
    //            flg_moveToPlayer = false;
    //        }
    //        else
    //        {
    //            m_hp--;
    //            //spriteRenderer.color = beforeColor;
    //            m_damageDelta = 0.0f;

    //            flg_blinking = false;
    //            flg_damage = false;
    //            flg_moveToPlayer = true;
    //        }

    //    }
    //}

    void Direction()
    {
        Vector2 scale = transform.localScale;
        if (player.transform.position.x < this.transform.position.x)
        {
            transform.localScale = new Vector3(firstScale.x, firstScale.y);
        }
        else if (player.transform.position.x > this.transform.position.x)
        {
            transform.localScale = new Vector3(-firstScale.x, firstScale.y);
        }
    }

    //void Die()
    //{
    //    if (m_hp <= 0.0f)
    //    {
    //        m_hp = 0.0f;

    //        Destroy(gameObject);
    //    }
    //}

    //void SystemDie()
    //{
    //    if (m_systemHp <= 0.0f)
    //    {
    //        this.tag = "Untagged";
    //    }
    //}

    ////void ChangeSprite()
    ////{
    ////    if (ChangeWorld.StateFront)
    ////    {
    ////        spriteRenderer.sprite = frontSprite;
    ////    }
    ////    else
    ////    {
    ////        spriteRenderer.sprite = backSprite;
    ////    }
    ////}

    //public static void SetDirection(float dir)
    //{
    //    m_direction *= dir;
    //}
}
