using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectImageScript : MonoBehaviour
{
    public int SelectingStageNum = 0;
    public GameObject CameraObject;
    StageSelectCameraScript cameraScript;

    Vector3 firstScale;

    // Start is called before the first frame update
    void Start()
    {
        cameraScript = CameraObject.GetComponent<StageSelectCameraScript>();
        firstScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraScript.SelectingStageNum == SelectingStageNum)
        {
            this.transform.localScale = firstScale + new Vector3(0.1f, 0.1f, 0.1f) * Mathf.Abs(Mathf.Sin(Time.time * 2));
        }
        else
        {
            this.transform.localScale = firstScale;
        }
    }
}
