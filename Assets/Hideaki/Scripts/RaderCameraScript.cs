using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaderCameraScript : MonoBehaviour
{
    public GameObject GameCamera;
    Transform CamTrans;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameCamera)
        {
            CamTrans = GameCamera.GetComponent<Transform>();
            Vector3 cameraPos = CamTrans.position;
            this.transform.position = cameraPos;
        }

    }
}
