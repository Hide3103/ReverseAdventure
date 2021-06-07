using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public bool m_CollisionFlg = false;
    public bool m_LadderFlg = false;

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
        if(collision.gameObject.tag == "Enemy")
        {
            m_CollisionFlg = true;
            Debug.Log("CollisionFlg : true");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            m_CollisionFlg = false;
            Debug.Log("CollisionFlg : false");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            m_LadderFlg = true;
            Debug.Log("LadderFlg : true");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            m_LadderFlg = true;
            Debug.Log("LadderFlg : true");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            m_LadderFlg = false;
            Debug.Log("LadderFlg : false");
        }
    }

    public bool GetCollisionFlg()
    {
        return m_CollisionFlg;
    }
}
