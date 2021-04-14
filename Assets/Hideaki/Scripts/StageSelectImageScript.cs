using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectImageScript : MonoBehaviour
{
    public int ThisStageNum = 1;
    public GameObject CameraObject;
    StageSelectCameraScript cameraScript;

    Vector3 firstScale;

    public Sprite LockImage = null;
    public Sprite OpenImage = null;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        cameraScript = CameraObject.GetComponent<StageSelectCameraScript>();
        firstScale = transform.localScale;

        spriteRenderer = this.GetComponent<SpriteRenderer>();

        if (ThisStageNum != 0)
        {
            if (GameSystem.GetStageCleared(ThisStageNum) == false)
            {
                spriteRenderer.sprite = LockImage;
            }
            else
            {
                spriteRenderer.sprite = OpenImage;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StageSelectCameraScript.SelectingStageNum == ThisStageNum)
        {
            this.transform.localScale = firstScale + new Vector3(0.1f, 0.1f, 0.1f) * Mathf.Abs(Mathf.Sin(Time.time * 2));
        }
        else
        {
            this.transform.localScale = firstScale;
        }

        //if(GameSystem.GetStageCleared(cameraScript.) == )
        //{

        //}
    }
}
