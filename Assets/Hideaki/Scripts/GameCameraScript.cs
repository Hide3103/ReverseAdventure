using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraScript : MonoBehaviour
{
    public GameObject player;
    Transform playerTrans;

    public GameObject start;
    Vector3 startPos;


    // Start is called before the first frame update
    void Start()
    {
        startPos = start.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            playerTrans = player.GetComponent<Transform>();
            Vector3 playerPos = playerTrans.position;
            if (startPos.x < playerPos.x)
            {
                this.transform.position = new Vector3(playerPos.x, playerPos.y, this.transform.position.z);
            }
        }
    }
}
