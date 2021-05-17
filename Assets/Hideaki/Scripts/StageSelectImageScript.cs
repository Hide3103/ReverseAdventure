using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectImageScript : MonoBehaviour
{
    public int ThisStageNum = 1;
    public GameObject CameraObject;
    StageSelectCameraScript cameraScript;

    public GameObject TapeObject;
    //public GameObject TapeText;

    Vector3 firstScale;

    public Sprite LockImage = null;
    public Sprite OpenImage = null;

    SpriteRenderer spriteRenderer;

    public Canvas m_canvas;

    public Text StageText;
    RectTransform rectTrans;

    // Start is called before the first frame update
    void Start()
    {
        cameraScript = CameraObject.GetComponent<StageSelectCameraScript>();
        firstScale = transform.localScale;

        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ThisStageNum != 0)
        {
            if (GameSystem.GetStageCleared(ThisStageNum) == true
                        && GameSystem.ClearJuwel[ThisStageNum - 1] <= GameSystem.GetAllStageJuwelNum())
            {
                TapeObject.SetActive(false);
                //TapeText.SetActive(false);
                spriteRenderer.sprite = OpenImage;
            }
            else
            {
                spriteRenderer.sprite = LockImage;
                StageText.text = ThisStageNum + "ステージクリア\n" + GameSystem.GetClearJuwelNum(ThisStageNum - 1) + "個所持";
            }
        }

        if (StageSelectCameraScript.SelectingStageNum == ThisStageNum)
        {
            this.transform.localScale = firstScale + new Vector3(0.1f, 0.1f, 0.1f) * Mathf.Abs(Mathf.Sin(Time.time * 2));
        }
        else
        {
            this.transform.localScale = firstScale;
        }
    }

    private Vector3 GetWorldPositionFromRectPosition(RectTransform rect)
    {
        //UI座標からスクリーン座標に変換
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(m_canvas.worldCamera, rect.position);

        //ワールド座標
        Vector3 result = Vector3.zero;

        //スクリーン座標→ワールド座標に変換
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPos, m_canvas.worldCamera, out result);

        return result;
    }
}
