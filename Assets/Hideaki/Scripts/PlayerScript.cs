using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // ダメージを受けたかのフラグ
    public bool m_DamagedFlg = false;
    // ダメージを受けてからの秒数    
    float m_DamageDelta = 0.0f;
    //ダメージ後の無敵時間の秒数
    float m_DamageLimit = 1.0f;

    //プレイヤーが死ぬ座標
    public float m_PlayerDeathPosY = -6.0f;

    //被ダメージ時に透明にする用のコンポーネント
    SpriteRenderer spriteRenderer;
    // ダメージを受ける前の色
    Color32 beforeColor;

    Rigidbody2D rigid2D;
    Transform transform;
    // プレイヤーの移動速度
    float m_MoveSpeed = 3.0f;
    // ジャンプ時に上方向へかかる力
    float m_JumpForce = 240.0f;
    // 最初のスケール
    Vector3 m_FirstScale;
    //宝箱のオブジェクト
    public GameObject GoalObject;
    Vector3 GoalPos;

    public bool m_TurnFlg = false;

    public static bool m_IsPlay = false;

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

    public GameObject PauseUI;

    public GameObject rawImage;
    RawImageScript rawImageScript;

    //public GameObject gameManager;
    //GameManagerScript gameManagerScript;

    void Start()
    {

        GameSystem.IsGoal = false;

        this.rigid2D = GetComponent<Rigidbody2D>();
        this.transform = GetComponent<Transform>();
        m_FirstScale = transform.localScale;
        //gameManagerScript = gameManager.GetComponent<GameManagerScript>();

        GoalPos = GoalObject.GetComponent<Transform>().position;

        spriteRenderer = GetComponent<SpriteRenderer>();
        beforeColor = spriteRenderer.color;

        ArmorUsing = GameSystem.GetArmorUsing();

        m_PlayerHp = playerMaxHP;

        rawImageScript = rawImage.GetComponent<RawImageScript>();
    }

    void Update()
    {
        if(GameSystem.ArmorUsing == true)
        {
            //Debug.Log("ArmorUsingはtrue");
        }
        else
        {
            //Debug.Log("ArmorUsingはfalse");
        }

        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        //ゴールなどプレイを中断させたい時のフラグ
        if (m_IsPlay == true && rawImageScript.GetChanggingFlg() == false)
            {
            // 移動
            if (m_DamagedFlg == false)
            {
                if (Input.GetKey(KeyCode.RightArrow) || hori > 0.3f)
                {
                    if (this.transform.position.x < GoalPos.x)
                    {
                        //m_LookKey = 1;
                        transform.position += new Vector3(m_MoveSpeed, 0, 0) * Time.deltaTime;
                        transform.localScale = new Vector3(m_FirstScale.x, m_FirstScale.y, m_FirstScale.z);
                    }
                }
                if (Input.GetKey(KeyCode.LeftArrow) || hori < -0.3f)
                {
                    //m_LookKey = -1;
                    transform.position -= new Vector3(m_MoveSpeed, 0, 0) * Time.deltaTime;
                    transform.localScale = new Vector3(-m_FirstScale.x, m_FirstScale.y, m_FirstScale.z);
                }
            }

            if (PauseUI.activeSelf == false)
            {
                // ジャンプ
                float speedY = Mathf.Abs(this.rigid2D.velocity.y);
                if (speedY <= 0.1f)
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown("joystick button 0"))
                    {
                        this.rigid2D.AddForce(transform.up * m_JumpForce);
                    }
                }
            }

            //ステージ反転
            if (m_TurnFlg == true)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    //gameManagerScript.SetWorldFrontSideFlg(!(gameManagerScript.GetWorldFrontSideFlg()));
                }
            }

            //攻撃
            if (attackSpan < attackDelta)
            {
                if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown("joystick button 1"))
                {
                    AttackLange.SetActive(true);
                    attackSpan = 0.0f;
                }
            }
            attackDelta += Time.deltaTime;

            // ダメージを受けた時の処理
            if (m_DamagedFlg == true)
            {
                if (m_DamageDelta < m_DamageLimit)
                {
                    float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
                }
                else
                {
                    spriteRenderer.color = beforeColor;
                    m_DamagedFlg = false;
                    m_DamageDelta = 0.0f;

                    if (m_PlayerHp <= 0.0f)
                    {
                        SceneManager.LoadScene("GameOver");
                        Destroy(this.gameObject);
                    }
                }

                m_DamageDelta += Time.deltaTime;
            }
            //穴に落ちた時の処理
            if (this.transform.position.y <= m_PlayerDeathPosY)
            {
                SceneManager.LoadScene("GameOver");
                Destroy(this.gameObject);
            }
        }
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        m_MoveSpeed = moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Damage();
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
            m_DamagedFlg = true;
            var rigidbody2D = GetComponent<Rigidbody2D>();
            //rigidbody.AddForce(-transform.forward * 5f, ForceMode.VelocityChange);
            rigidbody2D.velocity = Vector3.zero;
            rigidbody2D.AddForce(new Vector3(-transform.localScale.x * 500.0f, 200.0f, 0.0f));
        }
    }

    public static void SetIsPlay(bool isPlay)
    {
        m_IsPlay = isPlay;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}

