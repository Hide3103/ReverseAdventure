using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUI : MonoBehaviour
{
    public GameObject PlayerHP1;
    public GameObject PlayerHP2;
    public GameObject PlayerHP3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(PlayerScript.m_PlayerHp==2)
        //{
        //    PlayerHP3.gameObject.SetActive(false);
        //}
        //if (PlayerScript.m_PlayerHp == 1)
        //{
        //    PlayerHP2.gameObject.SetActive(false);
        //}
        //if (PlayerScript.m_PlayerHp == 0)
        //{
        //    PlayerHP1.gameObject.SetActive(false);
        //}
        //if (PlayerScript.m_PlayerHp == 3)
        //{
        //    PlayerHP1.gameObject.SetActive(true);
        //    PlayerHP2.gameObject.SetActive(true);
        //    PlayerHP3.gameObject.SetActive(true);
        //}
        if (MotionPlayer.m_PlayerHp == 2)
        {
            PlayerHP3.gameObject.SetActive(false);
        }
        if (MotionPlayer.m_PlayerHp == 1)
        {
            PlayerHP2.gameObject.SetActive(false);
        }
        if (MotionPlayer.m_PlayerHp == 0)
        {
            PlayerHP1.gameObject.SetActive(false);
        }
        if (MotionPlayer.m_PlayerHp == 3)
        {
            PlayerHP1.gameObject.SetActive(true);
            PlayerHP2.gameObject.SetActive(true);
            PlayerHP3.gameObject.SetActive(true);
        }
    }
}
