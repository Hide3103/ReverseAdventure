using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraScript : MonoBehaviour
{
    public GameObject player;
    MotionPlayer motionPlayerScript;
    Transform playerTrans;

    public GameObject start;
    Vector3 startPos;

    float CamPosX = 0.0f;
    Camera cam;
    //public GameObject raderCamera;
    //Camera raderCam;

    public float CamPosY = 0.75f;


    // Start is called before the first frame update
    void Start()
    {
        startPos = start.GetComponent<Transform>().position;
        //raderCam = raderCamera.GetComponent<Camera>();
        //raderCam.enabled = true;

        motionPlayerScript = player.GetComponent<MotionPlayer>();

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
            if(motionPlayerScript.m_PlayerDeathPosY + 4.0f < playerPos.y + CamPosY)
            {
                this.transform.position = new Vector3(CamPosX, playerPos.y + CamPosY, this.transform.position.z);
            }
        }
    }
}
