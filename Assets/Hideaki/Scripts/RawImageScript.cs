using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawImageScript : MonoBehaviour
{
    public Vector2 firstScale;
    public Vector2 secondScale;

    RectTransform rectTrans;
    public Vector2 sizeDelta;

    public bool changgingFlg = false;

    float m_TurnSpeed = 20.0f;
    float m_SizeChangeSpeed = 0.98f;

    public int faseNum = 0;

    public GameObject m_ChangeWorld;
    ChangeWorld changeWorldScript;

    // Start is called before the first frame update
    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        firstScale = new Vector2(GetWidth(rectTrans), GetHeigth(rectTrans));
        secondScale = new Vector2(firstScale.x * 0.9f, firstScale.y * 0.9f);

        changeWorldScript = m_ChangeWorld.GetComponent<ChangeWorld>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("sizeDelta.x :" + sizeDelta.x + "  sizeDelta.y :" + sizeDelta.y);
        //if(Input.GetKeyDown(KeyCode.C))
        //{
        //    changgingFlg = true;
        //}

        if(changgingFlg == true)
        {
            sizeDelta = rectTrans.sizeDelta;

            switch (faseNum)
            {
                case 0:
                    rectTrans.sizeDelta = new Vector2(sizeDelta.x * m_SizeChangeSpeed, sizeDelta.y * m_SizeChangeSpeed);
                    if (sizeDelta.x < secondScale.x || sizeDelta.y < secondScale.y)
                    {
                        faseNum += 1;
                    }
                    break;
                case 1:
                    rectTrans.sizeDelta = new Vector2(sizeDelta.x - m_TurnSpeed, sizeDelta.y);
                    if(sizeDelta.x <= 0)
                    {
                        if (ChangeWorld.GetStateFront() == true)
                        {
                            changeWorldScript.SetBackStage();
                        }
                        else
                        {
                            changeWorldScript.SetFrontStage();
                            if(changeWorldScript.CoolTimeOver == true)
                            {
                                ChangeWorld.SetUraActiveTime(0);
                                changeWorldScript.CoolTimeOver = false;
                            }
                        }
                        faseNum += 1;
                    }
                    break;
                case 2:
                    rectTrans.sizeDelta = new Vector2(sizeDelta.x + m_TurnSpeed, sizeDelta.y);
                    if(secondScale.x <= sizeDelta.x)
                    {
                        rectTrans.sizeDelta = secondScale;
                        faseNum += 1;
                    }
                    break;
                case 3:
                    rectTrans.sizeDelta = new Vector2(sizeDelta.x * (2 - m_SizeChangeSpeed), sizeDelta.y * (2 - m_SizeChangeSpeed));
                    if (firstScale.x < sizeDelta.x || firstScale.x < sizeDelta.y)
                    {
                        rectTrans.sizeDelta = firstScale;
                        faseNum += 1;
                    }
                    break;
                case 4:
                    changgingFlg = false;
                    faseNum = 0;
                    break;
            }


            //if(sizeDelta.x < secondScale.x || sizeDelta.y < secondScale.y)
            //{
            //    rectTrans.sizeDelta = new Vector2(sizeDelta.x - m_TurnSpeed, sizeDelta.y);
            //}
            //else
            //{
            //    rectTrans.sizeDelta = new Vector2(sizeDelta.x * m_SizeChangeSpeed, sizeDelta.y * m_SizeChangeSpeed);
            //}
        }
    }

    public static float GetWidth(RectTransform self)
    {
        return self.sizeDelta.x;
    }
    public static float GetHeigth(RectTransform self)
    {
        return self.sizeDelta.y;
    }
}
