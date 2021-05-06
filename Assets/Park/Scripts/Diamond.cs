using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public GameObject hiddenDiamond;
    SpriteRenderer diaSpriteRenderer;

    public int DiamondNum;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameSystem.NumJewel += 1;
            if(GameSystem.GetJuwelCollection(DiamondNum) == false)
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
