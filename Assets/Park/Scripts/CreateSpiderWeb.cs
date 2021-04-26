using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpiderWeb : MonoBehaviour
{
    GameObject spider;
    Spider spiderScript;

    public GameObject spiderWeb;

    bool flg_onSpiderWeb;

    // Start is called before the first frame update
    void Start()
    {
        spider = GameObject.Find("Spider");
        spiderScript = spider.GetComponent<Spider>();

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        CreateWeb();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpiderWeb")
        {
            flg_onSpiderWeb = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpiderWeb")
        {
            flg_onSpiderWeb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpiderWeb")
        {
            flg_onSpiderWeb = false;
        }
    }

    void Initialize()
    {
        flg_onSpiderWeb = false;
    }

    void CreateWeb()
    {
        if (spiderScript.GetFlgMoveToPlayer() && flg_onSpiderWeb == false)
        {
            GameObject spiderWebs = Instantiate(spiderWeb) as GameObject;

            spiderWebs.transform.position = transform.position;
        }
    }
}
