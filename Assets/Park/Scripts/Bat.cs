using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    public GameObject lookPlayer;
    public GameObject lostPlayer;

    Rigidbody2D rigid2D;

    Vector2 firstScale;
    Vector2 firstPosition;

    SpriteRenderer spriteRenderer;

    Color32 beforeColor;

    bool flg_normal;
    bool flg_lookPlayer;
    bool flg_moveToPlayer;
    bool flg_lostPlayer;
    bool flg_danger;
    bool flg_damage;
    bool flg_blinking;
    bool flg_attackPlayer;

    float m_hp;
    float m_systemHp;
    float m_speed;
    float m_direction;
    float m_moveToplayerTime;
    float m_lostPlayerTime;
    float m_damageDelta;
    float m_damageLimit;
    float m_attackTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        this.rigid2D = GetComponent<Rigidbody2D>();

        this.firstScale = transform.localScale;
        this.firstPosition = transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
        beforeColor = spriteRenderer.color;

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (m_systemHp > 0)
            {
                Move();
                MoveToPlayer();
            }

            Damage();
            Die();
            SystemDie();
        }

        if (enemy)
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //"Sword"に当たった時にフラグ切り替え＆ノックバック
        if (collision.gameObject.tag == "Sword")
        {
            if (flg_blinking == false)
            {
                flg_damage = true;
                m_systemHp -= 1.0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //flg_moveToPlayer = false;
            flg_attackPlayer = true;
        }
    }

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

            if (Position.y < firstPosition.y)
            {
                Position.y += m_speed * Time.deltaTime;
            }

            transform.position = Position;

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

    void MoveToPlayer()
    {
        Transform target = player.transform;
        float distance = Vector2.Distance(target.position, transform.position);

        if (distance <= 4.0f)
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

    void Damage()
    {
        if (flg_damage)
        {
            if (m_damageDelta < m_damageLimit)
            {
                float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
                m_damageDelta += Time.deltaTime;

                flg_blinking = true;
                flg_moveToPlayer = false;
            }
            else
            {
                m_hp--;
                spriteRenderer.color = beforeColor;
                m_damageDelta = 0.0f;

                flg_blinking = false;
                flg_damage = false;
                flg_moveToPlayer = true;
            }

        }
    }

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

    void Die()
    {
        if (m_hp <= 0.0f)
        {
            m_hp = 0.0f;

            Destroy(gameObject);
        }
    }

    void SystemDie()
    {
        Vector2 Position = transform.position;

        if (m_systemHp <= 0.0f)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            Position.y -= (m_speed *= 1.1f) * Time.deltaTime;

            transform.position = Position;
        }
    }
}

