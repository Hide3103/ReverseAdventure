using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public GameObject hiddenDiamond;
    SpriteRenderer diaSpriteRenderer;

    public int DiamondNum;

    AudioSource juwelAudio;
    public AudioClip SE_GetJuwel;

    bool GettedFlg = false;

    float DestroyDelta = 0.0f;
    float DestroyLimit = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        if (hiddenDiamond)
        {
            diaSpriteRenderer = hiddenDiamond.GetComponent<SpriteRenderer>();
        }
        
        if(GameSystem.GetJuwelCollection(DiamondNum) == true)
        {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            if(hiddenDiamond)
            {
                diaSpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
        }

        juwelAudio = GetComponent<AudioSource>();
    }

    //変更後
    //// Update is called once per frame
    //void Update()
    //{
    //    if(GettedFlg == true)
    //    {
    //        if(DestroyDelta < DestroyLimit)
    //        {
    //            DestroyDelta += Time.deltaTime;
    //        }
    //        else
    //        {
    //            if (hiddenDiamond)
    //            {
    //                Destroy(hiddenDiamond);
    //            }
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if(GettedFlg == false)
    //        {
    //            juwelAudio.PlayOneShot(SE_GetJuwel);
    //            Debug.Log("宝石獲得");
    //            GameSystem.NumJewel += 1;
    //            if(GameSystem.GetJuwelCollection(DiamondNum) == false)
    //            {
    //                GameSystem.SetJuwelGetted(DiamondNum, true);
    //            }
    //            GettedFlg = true;
    //        }
    //    }
    //}

    // 変更前
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            juwelAudio.PlayOneShot(SE_GetJuwel);
            GameSystem.NumJewel += 1;
            if (GameSystem.GetJuwelCollection(DiamondNum) == false)
            {
                GameSystem.SetJuwelGetted(DiamondNum, true);
            }

            //GameSystem.HavingNumJuwel += 1;
            if (hiddenDiamond)
            {
                Destroy(hiddenDiamond);
            }
            Destroy(gameObject);
        }
    }
}
