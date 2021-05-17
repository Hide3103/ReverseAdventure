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

    // Start is called before the first frame update
    void Start()
    {
        GetJewel.gameObject.SetActive(true);
        Time.timeScale = 1;

        if(GameSystem.GetClearTime(GameSystem.WasPlayStage) >= GameSystem.ClearTime)
        {
            GameSystem.SetClearTime(Mathf.CeilToInt(GameSystem.ClearTime));
        }

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
        GameSystem.SetStageCleared(true, GameSystem.WasPlayStage);

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
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && NowSelect > 1 || (hori < 0 && WaitTime <= 0)&&NowSelect>1 && PadWaitTime <= 0)
        {
            resultAudio.PlayOneShot(SE_Select);
            NowSelect--;
            PadWaitTime = SetWaitTime;
        }

        //セレクトシステム
        switch (NowSelect)
        {

            case 1:
                if (NowSelect == 1)
                {
                    TitleCanvasGroupstage3.alpha = 1.0f;
                    FlashTime += Time.unscaledDeltaTime;
                    if (FlashTime< 1.5 && SetAlpha< 1)
                    {
                        if (SetAlpha< 1)
                        {
                            SetAlpha += FlashSpeed * Time.unscaledDeltaTime;
                        }
                    TitleCanvasGroupstage1.alpha = SetAlpha;
                    }
                    if (FlashTime > 1.5 && SetAlpha > 0.4)
                    {
                        SetAlpha -= FlashSpeed * Time.unscaledDeltaTime;
                        if (SetAlpha< 0.4)
                        {
                            FlashTime = 0;
                        }
                    }
                    TitleCanvasGroupstage1.alpha = SetAlpha;
                    TitleCanvasGroupstage2.alpha = 1.0f;
                    TitleCanvasGroupstage3.alpha = 1.0f;
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
                            SceneManager.LoadScene("Stage_2");
                            //リセット内容を記載


                            GameSystem.WasPlayStage = 2;
                            break;
                        case 2:
                            SceneManager.LoadScene("Stage_3");
                            //リセット内容を記載


                            GameSystem.WasPlayStage = 3;
                            break;
                        case 3:
                            SceneManager.LoadScene("Stage_4");
                            //リセット内容を記載


                            GameSystem.WasPlayStage = 4;
                            break;
                        case 4:
                            SceneManager.LoadScene("Stage_5");
                            //リセット内容を記載


                            GameSystem.WasPlayStage = 5;
                            break;
                    }
                }
                break;
            case 2:
                if (NowSelect == 2)
                {
                    TitleCanvasGroupstage1.alpha = 1.0f;
                    FlashTime += Time.unscaledDeltaTime;
                    if (FlashTime< 1.5 && SetAlpha< 1)
                    {
                        if (SetAlpha< 1)
                        {
                            SetAlpha += FlashSpeed * Time.unscaledDeltaTime;
                        }
                        TitleCanvasGroupstage2.alpha = SetAlpha;
                    }
                    if (FlashTime > 1.5 && SetAlpha > 0.4)
                    {
                        SetAlpha -= FlashSpeed * Time.unscaledDeltaTime;
                        if (SetAlpha< 0.4)
                        {
                            FlashTime = 0;
                        }
                    }
                    TitleCanvasGroupstage2.alpha = SetAlpha;
                    TitleCanvasGroupstage1.alpha = 1.0f;
                    TitleCanvasGroupstage3.alpha = 1.0f;
                }
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
                {
                    resultAudio.PlayOneShot(SE_Enter);
                    SceneManager.LoadScene("StageSelect");
                }
                break;
            case 3:
                if (NowSelect == 3)
                {
                    TitleCanvasGroupstage2.alpha = 1.0f;
                    FlashTime += Time.unscaledDeltaTime;
                    if (FlashTime< 1.5 && SetAlpha< 1)
                    {
                        if (SetAlpha< 1)
                            SetAlpha += FlashSpeed * Time.unscaledDeltaTime;
                    }
                    TitleCanvasGroupstage3.alpha = SetAlpha;
                    if (FlashTime > 1.5 && SetAlpha > 0.4)
                    {
                        SetAlpha -= FlashSpeed * Time.unscaledDeltaTime;
                        if (SetAlpha< 0.4)
                        {
                            FlashTime = 0;
                        }
                        TitleCanvasGroupstage3.alpha = SetAlpha;
                        TitleCanvasGroupstage1.alpha = 1.0f;
                        TitleCanvasGroupstage2.alpha = 1.0f;
                    }
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
                    }
                }
                break;

        }


    }
}
