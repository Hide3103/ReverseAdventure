using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaffold : MonoBehaviour
{
    public GameObject spriteObj;
    public float vibrationWidth = 1.00f; //振動幅
    public float vibrationSpeed = 30.0f; //振動速度
    public float fallTime = 1.0f;　　　　//落ちるまでの時間
    public float fallSpeed = 10.0f;　　　//落ちる速度
    public float returnTime = 3.0f;   //足場のリスポーン時間

    private bool isOn;
    private bool isFall;
    private bool isReturn;
    private Vector3 spriteDefaultPos;
    private Vector3 floorDefaultPos;
    private Vector2 fallVelocity;
    private BoxCollider2D col;
    private Rigidbody2D rb;
    private ObjectCollision oc;
    private SpriteRenderer sr;
    private float timer = 0.0f;
    private float fallingTimer = 0.0f;
    private float returnTimer = 0.0f;
    private float blinkTimer = 0.0f;

    public GameObject collisionObject;
    ObjectCollision objectCollisionScript;
    public bool playerHitFlg = false;

    void Start()
    {
        //初期設定
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        oc = GetComponent<ObjectCollision>();
        if (spriteObj != null && col != null && rb != null && oc != null)
        {
            spriteDefaultPos = spriteObj.transform.position;
            fallVelocity = new Vector2(0, -fallSpeed);
            floorDefaultPos = gameObject.transform.position;
            sr = spriteObj.GetComponent<SpriteRenderer>();
        }

        objectCollisionScript = collisionObject.GetComponent<ObjectCollision>();
    }

    void Update()
    {
        //一定時間経過後に明滅して戻る
        if (isReturn)
        {
            //明滅　ついている時に戻る
            if (blinkTimer > 0.2f)
            {
                sr.enabled = true;
                blinkTimer = 0.0f;
            }
            //明滅　消えている時
            else if (blinkTimer > 0.1f)
            {
                sr.enabled = false;
            }
            //明滅　ついている時
            else
            {
                sr.enabled = true;
            }

            //1秒経ったら明滅終了
            if (returnTime > 1.0f)
            {
                isReturn = false;
                blinkTimer = 0.0f;
                returnTime = 0.0f;
                sr.enabled = true;
            }
            else
            {
                blinkTimer += Time.deltaTime;
                returnTime += Time.deltaTime;
            }
        }

        playerHitFlg = objectCollisionScript.playerStepOn;

        if(playerHitFlg == true)
        {
            Debug.Log("playerHitFlg ＝ true");
            Destroy(this.gameObject,1.0f);
            //一定時間経過後に元の位置に戻る
            if (fallingTimer > returnTime)
            {
                isReturn = true;
                transform.position = floorDefaultPos;
                rb.velocity = Vector2.zero;
                isFall = false;
                timer = 0.0f;
                fallingTimer = 0.0f;
            }
            else
            {
                fallingTimer += Time.deltaTime;
                isOn = false;
            }

        }
    }
}
