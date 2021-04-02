using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraScript : MonoBehaviour
{
    public GameObject player;
    Transform playerTrans;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            playerTrans = player.GetComponent<Transform>();
            this.transform.position = new Vector3(playerTrans.position.x, playerTrans.position.y, this.transform.position.z);
        }
    }
}
