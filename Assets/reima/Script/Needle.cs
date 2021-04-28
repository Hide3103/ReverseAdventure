using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Needle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //最大のHPをへらす
        if(collision.transform.name =="Player")
        {
            PlayerScript.m_PlayerHp -= PlayerScript.m_PlayerHp;
        }
    }
}
