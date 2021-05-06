using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseUI;
    public GameObject TiTleBackUI;
    public GameObject ReTry;
    public GameObject GameBack;
    int NowNumSelect = 1;

    public CanvasGroup TitleCanvasGroupstage1;
    public CanvasGroup TitleCanvasGroupstage2;
    public CanvasGroup TitleCanvasGroupstage3;

    float FlashTime = 0;
    float WaitTime = 0;

    public float SetAlpha = 0.0f;
    public float SetAlpha2 = 0.0f;
    public float SetAlpha3 = 0.0f;

    public GameObject RawImage;
    RawImageScript rawImageScript;

    bool UseStick = false; 
    // Start is called before the first frame update
    void Start()
    {
        PauseUI.SetActive(false);

        rawImageScript = RawImage.GetComponent<RawImageScript>();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(WaitTime);
        float vert = Input.GetAxis("Vertical");
        UseStick = true;

        if (GameSystem.IsGoal == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown("joystick button 7"))
            {
                PauseUI.SetActive(!PauseUI.activeSelf);
            }
            if (PauseUI.activeSelf == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                if (rawImageScript.changgingFlg == true)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }

            //ポーズ画面がtrueの時
            if (PauseUI.activeSelf == true)
            {
                if (WaitTime > 0)
                {
                    WaitTime -= Time.unscaledDeltaTime;
                }
                if (((Input.GetKeyDown(KeyCode.UpArrow) && NowNumSelect > 1) || (vert>0 && NowNumSelect > 1&&WaitTime<=0)))
                {
                        NowNumSelect--;
                        WaitTime = 2;
                }
                if (((Input.GetKeyDown(KeyCode.DownArrow) && NowNumSelect < 3) || (vert < 0 && NowNumSelect < 3 && WaitTime <= 0)))
                {
                        NowNumSelect++;
                        WaitTime = 2;
                }
            }
        }


        switch(NowNumSelect)
        {
            case 1:
                if (NowNumSelect == 1)
                {
                    TitleCanvasGroupstage3.alpha = 1.0f;
                    TitleCanvasGroupstage2.alpha = 1.0f;
                    FlashTime += Time.unscaledDeltaTime;
                    if (FlashTime < 1.5 && SetAlpha < 1)
                    {
                        if (SetAlpha < 1)
                        {
                            SetAlpha += 0.7f * Time.unscaledDeltaTime;
                        }
                        TitleCanvasGroupstage1.alpha = SetAlpha;
                    }
                    if (FlashTime > 1.5 && SetAlpha > 0)
                    {
                        SetAlpha -= 0.7f * Time.unscaledDeltaTime;
                        if (SetAlpha < 0)
                        {
                            FlashTime = 0;
                        }
                    }
                    TitleCanvasGroupstage1.alpha = SetAlpha;
                }
                else
                {
                    SetAlpha3 = 1.0f;
                    TitleCanvasGroupstage3.alpha = SetAlpha3;
                    SetAlpha2 = 1.0f;
                    TitleCanvasGroupstage2.alpha = SetAlpha2;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //ゲームに戻る
                    PauseUI.SetActive(!PauseUI.activeSelf);
                }
                    break;

            case 2:
                if (NowNumSelect == 2)
                {
                    TitleCanvasGroupstage1.alpha = 1.0f;
                    TitleCanvasGroupstage3.alpha = 1.0f;
                    FlashTime += Time.unscaledDeltaTime;
                    if (FlashTime < 1.5 && SetAlpha2 < 1)
                    {
                        if (SetAlpha2 < 1)
                        {
                            SetAlpha2 += 0.7f * Time.unscaledDeltaTime;
                        }
                        TitleCanvasGroupstage2.alpha = SetAlpha2;
                    }
                    if (FlashTime > 1.5 && SetAlpha2 > 0)
                    {
                        SetAlpha2 -= 0.7f * Time.unscaledDeltaTime;
                        if (SetAlpha2 < 0)
                        {
                            FlashTime = 0;
                        }
                    }
                    TitleCanvasGroupstage2.alpha = SetAlpha2;
                }
                else
                {
                    SetAlpha3 = 1.0f;
                    TitleCanvasGroupstage3.alpha = SetAlpha3;
                    SetAlpha = 1.0f;
                    TitleCanvasGroupstage1.alpha = SetAlpha;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {

                    //ステージセレクトに戻る
                    SceneManager.LoadScene("StageSelect");

                }
                break;

            case 3:

                if (NowNumSelect == 3)
                {
                    TitleCanvasGroupstage2.alpha = 1.0f;
                    TitleCanvasGroupstage1.alpha = 1.0f;
                    FlashTime += Time.unscaledDeltaTime;
                    if (FlashTime < 1.5 && SetAlpha3 < 1)
                    {
                        if (SetAlpha3 < 1)
                        {
                            SetAlpha3 += 0.7f * Time.unscaledDeltaTime;
                        }
                        TitleCanvasGroupstage3.alpha = SetAlpha3;
                    }
                    if (FlashTime > 1.5 && SetAlpha3 > 0)
                    {
                        SetAlpha3 -= 0.7f * Time.unscaledDeltaTime;
                        if (SetAlpha3 < 0)
                        {
                            FlashTime = 0;
                        }
                    }
                    TitleCanvasGroupstage3.alpha = SetAlpha3;
                }
                else
                {
                    SetAlpha = 1.0f;
                    TitleCanvasGroupstage1.alpha = SetAlpha;
                    SetAlpha3 = 1.0f;
                    TitleCanvasGroupstage3.alpha = SetAlpha3;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Application.Quit();
                }
                break;
        }
    }
}
