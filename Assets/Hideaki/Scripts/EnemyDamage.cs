using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public GameObject player;
    float m_PlayerSearchLength = 5.0f;

    float m_MoveSpeed = 0.2f;

    Vector3 m_FirstScale;

    //被ダメージ時に透明にする用のコンポーネント
    SpriteRenderer spriteRenderer;
    // ダメージを受ける前の色
    Color32 beforeColor;
    // ダメージ時に使用する変数
    public bool m_DamageFlg = false;
    float m_DamageDelta = 0.0f;
    float m_DamageLimit = 1.0f;
    
    void Start()
    {
        //点滅用コンポーネント
        spriteRenderer = GetComponent<SpriteRenderer>();
        beforeColor = spriteRenderer.color;

        //歩行時の向き変更
        m_FirstScale = transform.localScale;
    }
    
    void Update()
    {
        // プレイヤーに向かって移動する
        if (player)
        {
            float distance = Vector3.Distance(player.transform.position, this.transform.position);
            if (distance < m_PlayerSearchLength)
            {
                if (player.transform.position.x < this.transform.position.x)
                {
                    this.transform.position -= new Vector3(m_MoveSpeed * Time.deltaTime, 0, 0);
                    transform.localScale = new Vector3(m_FirstScale.x, m_FirstScale.y, m_FirstScale.z);
                }
                else if (this.transform.position.x < player.transform.position.x)
                {
                    this.transform.position += new Vector3(m_MoveSpeed * Time.deltaTime, 0, 0);
                    transform.localScale = new Vector3(-m_FirstScale.x, m_FirstScale.y, m_FirstScale.z);
                }
            }
        }

        //ダメージ時に点滅
        if (m_DamageFlg == true)
        {
            if(m_DamageDelta < m_DamageLimit)
            {
                float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
                m_DamageDelta += Time.deltaTime;
            }
            else
            {
                spriteRenderer.color = beforeColor;
                m_DamageFlg = false;
                m_DamageDelta = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //"Sword"に当たった時にフラグ切り替え＆ノックバック
        if (collision.gameObject.tag == "Sword")
        {
            m_DamageFlg = true;
            var rigidbody = GetComponent<Rigidbody2D>();
            //rigidbody.AddForce(-transform.forward * 5f, ForceMode.VelocityChange);
            rigidbody.AddForce(new Vector3(transform.localScale.x * 60.0f, 50.0f, 0.0f));

        }
    }
}
