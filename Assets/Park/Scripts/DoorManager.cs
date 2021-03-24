using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject closeDoor;
    public GameObject openDoor;

    bool flg_playerKey;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.gameObject.tag == "Player")
        {
            if (flg_playerKey)
            {
                closeDoor.SetActive(false);
                openDoor.SetActive(true);
            }

            flg_playerKey = false;
        }
    }

    void Initialize()
    {
        flg_playerKey = false;
    }

    public bool GetPlayerKeyFlg()
    {
        return flg_playerKey;
    }

    public void SetPlayerKeyFlg(bool set_PlayerKeyFlg)
    {
        flg_playerKey = set_PlayerKeyFlg;
    }
}
