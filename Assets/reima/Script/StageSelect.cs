using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    //ステージ数
    int NumStage = 1;

    public CanvasGroup TitleCanvasGroupstage1;
    public CanvasGroup TitleCanvasGroupstage2;
    public CanvasGroup TitleCanvasGroupstage3;

    float FlashTime = 0;
    float WaitTime = 0;

    public float SetAlpha = 0.0f;
    public float SetAlpha2 = 0.0f;
    public float SetAlpha3 = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && NumStage < 3)
        {
            NumStage++;
            Debug.Log(NumStage);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && NumStage > 1)
        {
            NumStage--;
            Debug.Log(NumStage);
        }

        switch (NumStage)
        {
            case 1:
                if (NumStage == 1)
                {
                    TitleCanvasGroupstage3.alpha = 1.0f;
                    FlashTime += Time.deltaTime;
                    if (FlashTime < 1.5 && SetAlpha < 1)
                    {
                        if (SetAlpha < 1)
                        {
                            SetAlpha += 0.7f * Time.deltaTime;
                        }
                        TitleCanvasGroupstage1.alpha = SetAlpha;
                    }
                    if (FlashTime > 1.5 && SetAlpha > 0)
                    {
                        SetAlpha -= 0.7f * Time.deltaTime;
                        if (SetAlpha < 0)
                        {
                            FlashTime = 0;
                        }
                    }
                    TitleCanvasGroupstage1.alpha = SetAlpha;
                }
                else
                {
                    SetAlpha = 1.0f;
                    TitleCanvasGroupstage1.alpha = SetAlpha;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {

                    SceneManager.LoadScene("Stage_1");
                }
                break;



            case 2:
                if (NumStage == 2)
                {
                    TitleCanvasGroupstage1.alpha = 1.0f;
                    FlashTime += Time.deltaTime;
                    if (FlashTime < 1.5 && SetAlpha < 1)
                    {
                        if (SetAlpha < 1)
                        {
                            SetAlpha += 0.7f * Time.deltaTime;
                        }
                        TitleCanvasGroupstage2.alpha = SetAlpha;
                    }
                    if (FlashTime > 1.5 && SetAlpha > 0)
                    {
                        SetAlpha -= 0.7f * Time.deltaTime;
                        if (SetAlpha < 0)
                        {
                            FlashTime = 0;
                        }
                    }
                    TitleCanvasGroupstage2.alpha = SetAlpha;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Stage_2");
                }
                break;




            case 3:
                if (NumStage == 3)
                {
                    TitleCanvasGroupstage2.alpha = 1.0f;
                    FlashTime += Time.deltaTime;
                    if (FlashTime < 1.5 && SetAlpha < 1)
                    {
                        if (SetAlpha < 1)
                            SetAlpha += 0.7f * Time.deltaTime;
                    }
                    TitleCanvasGroupstage3.alpha = SetAlpha;
                }
                if (FlashTime > 1.5 && SetAlpha > 0)
                {
                    SetAlpha -= 0.7f * Time.deltaTime;
                    if (SetAlpha < 0)
                    {
                        FlashTime = 0;
                    }
                }
                TitleCanvasGroupstage3.alpha = SetAlpha;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Stage_3");
                }
                break;
        }
    }
}
