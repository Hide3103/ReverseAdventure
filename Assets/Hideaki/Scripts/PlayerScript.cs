using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool m_DamagedFlg = false;
    float m_DamageDelta = 0.0f;
    float m_DamageLimit = 1.0f;

    //被ダメージ時に透明にする用のコンポーネント
    SpriteRenderer spriteRenderer;
    // ダメージを受ける前の色
    Color32 beforeColor;

    Rigidbody2D rigid2D;
    Transform transform;
    float m_MoveSpeed = 3.0f;
    float m_JumpForce = 350.0f;

    Vector3 m_FirstScale;

    public bool m_TurnFlg = false;

    public int m_PlayerHp = 3;
    public int m_StoneBoardStock = 0;

    public GameObject Sword;
    float attackSpan = 0.5f;
    float attackDelta = 0.0f;
    public int m_LookKey = 1;

    public GameObject AttackLange;

    //public GameObject gameManager;
    //GameManagerScript gameManagerScript;

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.transform = GetComponent<Transform>();
        m_FirstScale = transform.localScale;
        //gameManagerScript = gameManager.GetComponent<GameManagerScript>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        beforeColor = spriteRenderer.color;
    }

    void Update()
    {
        // 移動
        if (m_DamagedFlg == false)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //m_LookKey = 1;
                transform.position += new Vector3(m_MoveSpeed, 0, 0) * Time.deltaTime;
                transform.localScale = new Vector3(m_FirstScale.x, m_FirstScale.y, m_FirstScale.z);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //m_LookKey = -1;
                transform.position -= new Vector3(m_MoveSpeed, 0, 0) * Time.deltaTime;
                transform.localScale = new Vector3(-m_FirstScale.x, m_FirstScale.y, m_FirstScale.z);
            }
        }

        // ジャンプ
        float speedY = Mathf.Abs(this.rigid2D.velocity.y);
        if (speedY <= 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.rigid2D.AddForce(transform.up * m_JumpForce);
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
            if (Input.GetKeyDown(KeyCode.X))
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
                    Destroy(this.gameObject);
                }
            }

            m_DamageDelta += Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            if (m_DamagedFlg == false)
            {
                m_PlayerHp -= 1;
                m_DamagedFlg = true;
                var rigidbody2D = GetComponent<Rigidbody2D>();
                //rigidbody.AddForce(-transform.forward * 5f, ForceMode.VelocityChange);
                rigidbody2D.velocity = Vector3.zero;
                rigidbody2D.AddForce(new Vector3(-transform.localScale.x * 500.0f, 200.0f, 0.0f));
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}

