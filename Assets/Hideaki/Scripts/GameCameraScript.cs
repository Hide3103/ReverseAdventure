using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraScript : MonoBehaviour
{
    public GameObject player;
    Transform playerTrans;

    public GameObject start;
    Vector3 startPos;

    float CamPosX = 0.0f;
    Camera cam;
    //public GameObject raderCamera;
    //Camera raderCam;


    // Start is called before the first frame update
    void Start()
    {
        startPos = start.GetComponent<Transform>().position;
        //raderCam = raderCamera.GetComponent<Camera>();
        //raderCam.enabled = true;
        cam = GetComponent<Camera>();
        cam.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            playerTrans = player.GetComponent<Transform>();
            Vector3 playerPos = playerTrans.position;
            if (startPos.x < playerPos.x )
            {
                CamPosX = playerPos.x;
            }
            this.transform.position = new Vector3(CamPosX, playerPos.y + 0.75f, this.transform.position.z);
        }
    }
}
