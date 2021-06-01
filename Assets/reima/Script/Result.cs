using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public Text GetJewel;
    int NowSelect = 1;

    public CanvasGroup TitleCanvasGroupstage1;
    public CanvasGroup TitleCanvasGroupstage2;
    public CanvasGroup TitleCanvasGroupstage3;

    float FlashTime = 0;
    float FlashSpeed = 0.7f;
    float WaitTime = 0;

    public float SetAlpha = 0.0f;
    public float SetAlpha2 = 0.0f;
    public float SetAlpha3 = 0.0f;

    bool[] array;

    float PadWaitTime = 0;
    float SetWaitTime = GameSystem.SetWaitTime;

    AudioSource resultAudio;
    public AudioClip SE_Enter;
    public AudioClip SE_Select;
    public AudioClip SE_Cancel;

    public GameObject NextStageImg;
    public GameObject StageSelectImg;
    public GameObject Retry;

    public GameObject Open_NextStageImg;
    public GameObject Open_StageSelectImg;
    public GameObject Open_Retry;

    public GameObject NotNumTextPanel;

    // Start is called before the first frame update
    void Start()
    {
        NotNumTextPanel.SetActive(false);
        GetJewel.gameObject.SetActive(true);
        Time.timeScale = 1;

        //if(GameSystem.GetClearTime(GameSystem.WasPlayStage) >= GameSystem.ClearTime)
        //{
        //    GameSystem.SetClearTime(Mathf.CeilToInt(GameSystem.ClearTime));
        //}

        switch(GameSystem.WasPlayStage)
        {
            case 1:
                array = GameSystem.Stage1JuwelGetted;
                break;
            case 2:
                array = GameSystem.Stage2JuwelGetted;
                break;
            case 3:
                array = GameSystem.Stage3JuwelGetted;
                break;
            case 4:
                array = GameSystem.Stage4JuwelGetted;
                break;
            case 5:
                array = GameSystem.Stage5JuwelGetted;
                break;
            case 6:
                array = GameSystem.Stage6JuwelGetted;
                break;
            case 7:
                array = GameSystem.Stage7JuwelGetted;
                break;
            case 8:
                array = GameSystem.Stage8JuwelGetted;
                break;
            case 9:
                array = GameSystem.Stage9JuwelGetted;
                break;
            case 10:
                array = GameSystem.Stage10JuwelGetted;
                break;
        }

        for (int i = 0; i < array.Length; i++)
        {
            if(GameSystem.GetJuwelGetted(i) == true)
            {
                GameSystem.SetJuwelCollection(i);
            }
        }

        resultAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetJewel.text = "獲得宝石数　:　" + GameSystem.NumJewel+"個";
        //GameSystem.SetClearTime(Mathf.CeilToInt(GameSystem.ClearTime));
        if(GameSystem.WasPlayStage != 10)
        {
            GameSystem.SetStageCleared(true, GameSystem.WasPlayStage);
        }

        SelectSystem();
    }


    //セレクトするときのシステム
    void SelectSystem()
    {
        float hori = Input.GetAxis("Horizontal");
        if (PadWaitTime > 0)
        {
            PadWaitTime -= Time.unscaledDeltaTime;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && NowSelect < 3 || (hori > 0 && WaitTime <= 0)&&NowSelect<3 && PadWaitTime <= 0)
        {
            resultAudio.PlayOneShot(SE_Select);
            NowSelect++;
            PadWaitTime = SetWaitTime;
            NotNumTextPanel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && NowSelect > 1 || (hori < 0 && WaitTime <= 0)&&NowSelect>1 && PadWaitTime <= 0)
        {
            resultAudio.PlayOneShot(SE_Select);
            NowSelect--;
            PadWaitTime = SetWaitTime;
            NotNumTextPanel.SetActive(false);
        }

        //セレクトシステム
        switch (NowSelect)
        {

            case 1:
                if (NowSelect == 1)
                {
                    NextStageImg.SetActive(false);
                    Open_NextStageImg.SetActive(true);

                    Open_Retry.SetActive(false);
                    Open_StageSelectImg.SetActive(false);

                    Retry.SetActive(true);
                    StageSelectImg.SetActive(true);
                    //仕様変更につき取り消し
                    //TitleCanvasGroupstage3.alpha = 1.0f;
                    //FlashTime += Time.unscaledDeltaTime;
                    //if (FlashTime< 1.5 && SetAlpha< 1)
                    //{
                    //    if (SetAlpha< 1)
                    //    {
                    //        SetAlpha += FlashSpeed * Time.unscaledDeltaTime;
                    //    }
                    //TitleCanvasGroupstage1.alpha = SetAlpha;
                    //}
                    //if (FlashTime > 1.5 && SetAlpha > 0.4)
                    //{
                    //    SetAlpha -= FlashSpeed * Time.unscaledDeltaTime;
                    //    if (SetAlpha< 0.4)
                    //    {
                    //        FlashTime = 0;
                    //    }
                    //}
                    //TitleCanvasGroupstage1.alpha = SetAlpha;
                    //TitleCanvasGroupstage2.alpha = 1.0f;
                    //TitleCanvasGroupstage3.alpha = 1.0f;
                }
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
                {
                    resultAudio.PlayOneShot(SE_Enter);
                    //次にどこのステージに行くか(ステージ追加時は必須)
                    switch (GameSystem.WasPlayStage)
                    {
                        case 0:
                            break;
                        case 1:
                            if (GameSystem.GetAllStageJuwelNum() > GameSystem.ClearJuwel[1])
                            {
                                GameSystem.WasPlayStage = 2;
                                SceneManager.LoadScene("Stage_2");
                            }
                            else
                            {
                                NotNumTextPanel.SetActive(true);
                            }
                            //リセット内容を記載


                            break;
                        case 2:
                            if (GameSystem.GetAllStageJuwelNum() > GameSystem.ClearJuwel[2])
                            {
                                GameSystem.WasPlayStage = 3;
                                SceneManager.LoadScene("Stage_3");
                            }
                            else
                            {
                                NotNumTextPanel.SetActive(true);
                            }
                            //リセット内容を記載
                            break;
                        case 3:
                            if (GameSystem.GetAllStageJuwelNum() > GameSystem.ClearJuwel[3])
                            {
                                GameSystem.WasPlayStage = 4;
                                SceneManager.LoadScene("Stage_4");
                            }
                            else
                            {
                                NotNumTextPanel.SetActive(true);
                            }
                            //リセット内容を記載
                            break;
                        case 4:
                            if (GameSystem.GetAllStageJuwelNum() > GameSystem.ClearJuwel[4])
                            {
                                GameSystem.WasPlayStage = 5;
                                SceneManager.LoadScene("Stage_5");
                            }
                            else
                            {
                                NotNumTextPanel.SetActive(true);
                            }
                            //リセット内容を記載


                            break;
                        case 5:
                            if (GameSystem.GetAllStageJuwelNum() > GameSystem.ClearJuwel[5])
                            {
                                GameSystem.WasPlayStage = 6;
                                SceneManager.LoadScene("Stage_6");
                            }
                            else
                            {
                                NotNumTextPanel.SetActive(true);
                            }
                            //リセット内容を記載


                            break;
                        case 6:
                            if (GameSystem.GetAllStageJuwelNum() > GameSystem.ClearJuwel[6])
                            {
                                GameSystem.WasPlayStage = 7;
                                SceneManager.LoadScene("Stage_7");
                            }
                            else
                            {
                                NotNumTextPanel.SetActive(true);
                            }
                            //リセット内容を記載


                            break;

                        case 7:
                            if (GameSystem.GetAllStageJuwelNum() > GameSystem.ClearJuwel[7])
                            {
                                GameSystem.WasPlayStage = 8;
                                SceneManager.LoadScene("Stage_8");
                            }
                            else
                            {
                                NotNumTextPanel.SetActive(true);
                            }
                            //リセット内容を記載


                            break;

                        case 8:
                            if (GameSystem.GetAllStageJuwelNum() > GameSystem.ClearJuwel[8])
                            {
                                GameSystem.WasPlayStage = 9;
                                SceneManager.LoadScene("Stage_9");
                            }
                            else
                            {
                                NotNumTextPanel.SetActive(true);
                            }
                            //リセット内容を記載


                            break;
                        case 9:
                            if (GameSystem.GetAllStageJuwelNum() > GameSystem.ClearJuwel[9])
                            {
                                GameSystem.WasPlayStage = 10;
                                SceneManager.LoadScene("Stage_10");
                            }
                            else
                            {
                                NotNumTextPanel.SetActive(true);
                            }
                            //リセット内容を記載
                            break;
                        default:
                            resultAudio.PlayOneShot(SE_Cancel);
                            break;
                    }
                }
                break;
            case 2:
                if (NowSelect == 2)
                {

                    StageSelectImg.SetActive(false);
                    Open_StageSelectImg.SetActive(true);

                    Open_Retry.SetActive(false);
                    Open_NextStageImg.SetActive(false);

                    Retry.SetActive(true);
                    NextStageImg.SetActive(true);
                    //TitleCanvasGroupstage1.alpha = 1.0f;
                    //FlashTime += Time.unscaledDeltaTime;
                    //if (FlashTime< 1.5 && SetAlpha< 1)
                    //{
                    //    if (SetAlpha< 1)
                    //    {
                    //        SetAlpha += FlashSpeed * Time.unscaledDeltaTime;
                    //    }
                    //    TitleCanvasGroupstage2.alpha = SetAlpha;
                    //}
                    //if (FlashTime > 1.5 && SetAlpha > 0.4)
                    //{
                    //    SetAlpha -= FlashSpeed * Time.unscaledDeltaTime;
                    //    if (SetAlpha< 0.4)
                    //    {
                    //        FlashTime = 0;
                    //    }
                    //}
                    //TitleCanvasGroupstage2.alpha = SetAlpha;
                    //TitleCanvasGroupstage1.alpha = 1.0f;
                    //TitleCanvasGroupstage3.alpha = 1.0f;
                }
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
                {
                    switch (GameSystem.WasPlayStage)
                    {
                        case 0:
                            break;
                        case 1:
                            GameSystem.WasPlayStage = 1;
                            break;
                        case 2:
                            GameSystem.WasPlayStage = 2;
                            break;
                        case 3:
                            GameSystem.WasPlayStage = 3;
                            break;
                        case 4:
                            GameSystem.WasPlayStage = 4;
                            break;
                        case 5:
                            GameSystem.WasPlayStage = 5;
                            break;
                        case 6:
                            GameSystem.WasPlayStage = 6;
                            break;
                        case 7:
                            GameSystem.WasPlayStage = 7;
                            break;
                        case 8:
                            GameSystem.WasPlayStage = 8;
                            break;
                        case 9:
                            GameSystem.WasPlayStage = 9;
                            break;
                        case 10:
                            GameSystem.WasPlayStage = 10;
                            break;
                    }
                    resultAudio.PlayOneShot(SE_Enter);
                    SceneManager.LoadScene("StageSelect");
                }
                break;
            case 3:
                if (NowSelect == 3)
                {
                    Retry.SetActive(false);
                    Open_Retry.SetActive(true);

                    Open_StageSelectImg.SetActive(false);
                    Open_NextStageImg.SetActive(false);

                    StageSelectImg.SetActive(true);
                    NextStageImg.SetActive(true);
                    //TitleCanvasGroupstage2.alpha = 1.0f;
                    //FlashTime += Time.unscaledDeltaTime;
                    //if (FlashTime< 1.5 && SetAlpha< 1)
                    //{
                    //    if (SetAlpha< 1)
                    //        SetAlpha += FlashSpeed * Time.unscaledDeltaTime;
                    //}
                    //TitleCanvasGroupstage3.alpha = SetAlpha;
                    //if (FlashTime > 1.5 && SetAlpha > 0.4)
                    //{
                    //    SetAlpha -= FlashSpeed * Time.unscaledDeltaTime;
                    //    if (SetAlpha< 0.4)
                    //    {
                    //        FlashTime = 0;
                    //    }
                    //    TitleCanvasGroupstage3.alpha = SetAlpha;
                    //    TitleCanvasGroupstage1.alpha = 1.0f;
                    //    TitleCanvasGroupstage2.alpha = 1.0f;
                    //}
                }
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
                {
                    resultAudio.PlayOneShot(SE_Enter);
                    //もう一回同じステージを読み込む(ステージ追加時は必須)
                    switch (GameSystem.WasPlayStage)
                    {
                        case 0:
                            break;
                        case 1:
                            SceneManager.LoadScene("Stage_1");
                            //リセット内容を記載
                            break;
                        case 2:
                            SceneManager.LoadScene("Stage_2");
                            //リセット内容を記載
                            break;
                        case 3:
                            SceneManager.LoadScene("Stage_3");
                            //リセット内容を記載
                            break;
                        case 4:
                            SceneManager.LoadScene("Stage_4");
                            //リセット内容を記載
                            break;
                        case 5:
                            SceneManager.LoadScene("Stage_5");
                            //リセット内容を記載
                            break;
                        case 6:
                            SceneManager.LoadScene("Stage_6");
                            //リセット内容を記載
                            break;
                        case 7:
                            SceneManager.LoadScene("Stage_7");
                            //リセット内容を記載
                            break;
                        case 8:
                            SceneManager.LoadScene("Stage_8");
                            //リセット内容を記載
                            break;
                        case 9:
                            SceneManager.LoadScene("Stage_9");
                            //リセット内容を記載
                            break;
                        case 10:
                            SceneManager.LoadScene("Stage_10");
                            //リセット内容を記載
                            break;
                        default:
                            break;
                    }
                }
                break;

        }


    }
}
