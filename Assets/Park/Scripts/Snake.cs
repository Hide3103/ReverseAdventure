using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    public GameObject lookPlayer;
    public GameObject lostPlayer;
    public GameObject sleep;

    Vector2 firstScale;

    Rigidbody2D rigid2D;

    SpriteRenderer spriteRenderer;

    Color32 beforeColor;

    bool flg_normal;
    bool flg_lookPlayer;
    bool flg_moveToPlayer;
    bool flg_lostPlayer;
    bool flg_danger;
    bool flg_damage;
    bool flg_blinking;
    bool flg_chargeAttack;
    bool flg_isSleeping;

    float m_hp;
    float m_systemHp;
    float m_speed;
    float m_direction;
    float m_changeDirectionTime;
    float m_moveToplayerTime;
    float m_lostPlayerTime;
    float m_damageDelta;
    float m_damageLimit;
    float m_chargeTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        this.firstScale = transform.localScale;

        this.rigid2D = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        beforeColor = spriteRenderer.color;

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        rigid2D.constraints = RigidbodyConstraints2D.FreezeRotation; //ローテーション固定

        if (player)
        {
            if (m_systemHp > 0)
            {
                Move();
                MoveToPlayer();
            }

            Damage();
            ChargeForAttack();
            Sleep();
            Die();
            SystemDie();

            Debug.Log(flg_isSleeping);
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
                m_systemHp--;

                rigid2D.AddForce(new Vector3(transform.localScale.x * 500.0f, 50.0f, 0.0f));
            }
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
        flg_chargeAttack = false;
        flg_isSleeping = true;

        m_hp = 3.0f;
        m_systemHp = m_hp;
        m_speed = 0.5f;
        m_direction = -1.0f;
        m_changeDirectionTime = 0.0f;
        m_moveToplayerTime = 0.0f;
        m_lostPlayerTime = 0.0f;
        m_damageDelta = 0.0f;
        m_damageLimit = 1.0f;
        m_chargeTime = 0.0f;
    }

    void Move()
    {
        Vector2 Position = transform.position;
        Transform target = player.transform;
        Vector2 direction = (target.position - transform.position).normalized;

        float accelaration = 2.5f; //速度
        float velocity = (accelaration * Time.deltaTime); //速度を秒速に変える
        float distance = Vector2.Distance(target.position, transform.position);

        if (flg_normal)
        {
            m_changeDirectionTime += Time.deltaTime;

            if (m_changeDirectionTime >= 4.0f)
            {
                m_direction *= -1.0f;
                m_changeDirectionTime = 0.0f;
            }

            Position.x += m_speed * m_direction * Time.deltaTime;

            transform.position = Position;

            lookPlayer.SetActive(false);
            lostPlayer.SetActive(false);

            NormalDirection();
        }

        if (flg_lookPlayer)
        {
            MoveToPlayerDirection();

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
                MoveToPlayerDirection();

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

        if (flg_chargeAttack)
        {
            m_chargeTime += Time.deltaTime;

            flg_moveToPlayer = false;

            if (m_chargeTime >= 1.0f)
            {
                rigid2D.AddForce(new Vector3(transform.localScale.x * -1200.0f, 100.0f, 0.0f));

                m_chargeTime = 0.0f;
            }
        }
    }

    void MoveToPlayer()
    {
        Transform target = player.transform;
        float distance = Vector2.Distance(target.position, transform.position);

        if (flg_isSleeping == false)
        {
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

    void MoveToPlayerDirection()
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
                flg_chargeAttack = false;
                m_chargeTime = 0.0f;
            }
            else
            {
                m_hp--;
                spriteRenderer.color = beforeColor;
                m_damageDelta = 0.0f;

                flg_blinking = false;
                flg_damage = false;
                flg_moveToPlayer = true;
                flg_chargeAttack = true;
            }

        }
    }

    void ChargeForAttack()
    {
        Transform target = player.transform;
        float distance = Vector2.Distance(target.position, transform.position);

        if (flg_isSleeping == false)
        {
            if (distance <= 2.0f)
            {
                flg_chargeAttack = true;
                flg_moveToPlayer = false;
            }
            else if (distance > 2.0f && distance <= 7.0f && flg_chargeAttack)
            {
                flg_chargeAttack = false;
                flg_moveToPlayer = true;
            }
        }
    }

    void Sleep()
    {
        Transform target = player.transform;
        float distance = Vector2.Distance(target.position, transform.position);

        if (flg_isSleeping)
        {
            sleep.SetActive(true);

            flg_normal = false;

            if (distance <= 1.0f && ChangeWorld.StateFront == false)
            {
                m_moveToplayerTime = 0.5f;

                flg_lookPlayer = true;
                flg_isSleeping = false;

                sleep.SetActive(false);
            }
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
        if (m_systemHp <= 0.0f)
        {
            this.tag = "Untagged";
        }

        if (flg_isSleeping)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rigid2D.isKinematic = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            rigid2D.isKinematic = false;
        }
    }
}
