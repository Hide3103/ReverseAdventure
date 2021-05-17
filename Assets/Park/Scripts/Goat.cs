using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goat : MonoBehaviour
{
    GameObject enemy;
    GameObject player;
    public GameObject lookPlayer;
    public GameObject lostPlayer;

    Transform enemyCollider;
    Transform playerCollider;

    public Sprite frontSprite;
    public Sprite backSprite;

    public AudioClip attack;

    Rigidbody2D rigid2D;

    Vector2 firstScale;

    SpriteRenderer spriteRenderer;

    Color32 beforeColor;

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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        enemyCollider = enemy.transform;
        playerCollider = player.transform;

        this.rigid2D = GetComponent<Rigidbody2D>();

        this.firstScale = transform.localScale;

        spriteRenderer = GetComponent<SpriteRenderer>();
        beforeColor = spriteRenderer.color;

        audioSource = GetComponent<AudioSource>();

        Initialize();
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

            Damage();
            Die();
            SystemDie();
            ChangeSprite();

            Physics2D.IgnoreCollision(playerTrans.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        
        if (enemy)
        {
            Transform enemyTrans = enemyCollider as Transform;

            Physics2D.IgnoreCollision(enemyTrans.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //"Sword"に当たった時にフラグ切り替え＆ノックバック
        if (collision.gameObject.tag == "Sword")
        {
            if (flg_blinking == false && ChangeWorld.StateFront)
            {
                flg_damage = true;

                rigid2D.AddForce(new Vector3(transform.localScale.x * 50.0f, 50.0f, 0.0f));

                m_systemHp--;
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
        else if(distance > 4.0f && flg_danger)
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
        if (m_systemHp <= 0.0f)
        {
            this.tag = "Untagged";
        }
    }

    void ChangeSprite()
    {
        if (ChangeWorld.StateFront)
        {
            spriteRenderer.sprite = frontSprite;
        }
        else
        {
            spriteRenderer.sprite = backSprite;
        }
    }

    public static void SetDirection(float dir)
    {
        m_direction *= dir;
    }
}
