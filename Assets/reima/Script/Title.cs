﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    public GameObject TitleCanvas;
    public GameObject SettingCanvas;
    public GameObject CloudCanvas;

    public GameObject CreditCanvas;


    //仕様変更
    //public CanvasGroup TitleCanvasGroupstage1;
   // public CanvasGroup TitleCanvasGroupstage2;
   // public CanvasGroup TitleCanvasGroupstage3;
    //public CanvasGroup TitleCanvasGroupstage4;

    public CanvasGroup DarkeningImg;


    public int NumSelect = 1;
    //1=ボタンを押してスタート
    //2=オプション
    //3=クレジット
    //4=ゲームを終わる
    float FlashTime = 0;
    float FlashSpeed = 0.8f;
    float WaitTime = 0;
    float PadWaitTime = 0;
    float SetWaitTime = GameSystem.SetWaitTime;

    public static bool TitleOther = false;

    public float SetAlpha = 0.0f;
    public float DarkeningAlpha = 0.0f;

    AudioSource titleAudio;
    public AudioClip SE_Enter;
    public AudioClip SE_ItemChange;
    public AudioClip SE_Cancel;

    public static bool DarkeningOn;

    public GameObject PushStartImg;
    public GameObject OptionImg;
    public GameObject CreditImg;
    public GameObject GameEndImg;

    public GameObject Open_PushStartImg;
    public GameObject Open_OptionImg;
    public GameObject Open_CreditImg;
    public GameObject Open_GameEndImg;

    bool Push = false;
    
    float SetTitlePosY = 250;

    bool GoSceneCredit = false;
    // Start is called before the first frame update
    void Start()
    {

        titleAudio = GetComponent<AudioSource>();
        DarkeningAlpha = 0.0f;
        NumSelect = 1;
    }

    // Update is called once per frame
    void Update()
    {
        MenuMove();
        switch (NumSelect)
        {
            case 1:
                //  StartFlash();
                SelectUiChange2();
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
                {
                    Push = true;
                    titleAudio.PlayOneShot(SE_Enter);
                }
                if(Push)
                {
                    Darkening();
                }
                break;
            case 2:
                // StartFlash();
                SelectUiChange2();
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
                {
                    Push = true;
                    titleAudio.PlayOneShot(SE_Enter);
                }
                if (Push)
                {
                    Darkening();
                }
                break;
            case 3:
                // StartFlash();
                SelectUiChange2();
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
                {
                    Push = true;
                    titleAudio.PlayOneShot(SE_Enter);
                }
                if (Push)
                {
                    Darkening();
                }
                break;
            case 4:
                //StartFlash();
                SelectUiChange2();
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
                {
                    titleAudio.PlayOneShot(SE_Enter);
                    Application.Quit();
                }
                break;
        }



    }

    //void StartFlash()
    //{
    //    FlashTime += Time.deltaTime;
    //    if (FlashTime < 1.5 && SetAlpha < 1)
    //    {
    //        if (SetAlpha < 1)
    //        {
    //            SetAlpha += FlashSpeed * Time.deltaTime;
    //        }
    //    }
    //    switch (NumSelect)
    //    {
    //        case 1:
    //            TitleCanvasGroupstage1.alpha = SetAlpha;
    //            TitleCanvasGroupstage2.alpha = 1;
    //            TitleCanvasGroupstage3.alpha = 1;
    //            TitleCanvasGroupstage4.alpha = 1;
    //            break;
    //        case 2:
    //            TitleCanvasGroupstage1.alpha = 1;
    //            TitleCanvasGroupstage2.alpha = SetAlpha;
    //            TitleCanvasGroupstage3.alpha = 1;
    //            TitleCanvasGroupstage4.alpha = 1;
    //            break;
    //        case 3:
    //            TitleCanvasGroupstage1.alpha = 1;
    //            TitleCanvasGroupstage2.alpha = 1;
    //            TitleCanvasGroupstage3.alpha = SetAlpha;
    //            TitleCanvasGroupstage4.alpha = 1;
    //            break;
    //        case 4:
    //            TitleCanvasGroupstage1.alpha = 1;
    //            TitleCanvasGroupstage2.alpha = 1;
    //            TitleCanvasGroupstage3.alpha = 1;
    //            TitleCanvasGroupstage4.alpha = SetAlpha;
    //            break;
    //    }
    //    仕様変更によりalphaを戻す必要なし
    //    if (FlashTime > 1.5 && SetAlpha > 0)
    //    {
    //        SetAlpha -= FlashSpeed * Time.deltaTime;
    //        if (SetAlpha < 0)
    //        {
    //            FlashTime = 0;
    //        }
    //    }
    //    switch (NumSelect)
    //    {
    //        case 1:
    //            TitleCanvasGroupstage1.alpha = SetAlpha;
    //            TitleCanvasGroupstage2.alpha = 1;
    //            TitleCanvasGroupstage3.alpha = 1;
    //            TitleCanvasGroupstage4.alpha = 1;
    //            break;
    //        case 2:
    //            TitleCanvasGroupstage1.alpha = 1;
    //            TitleCanvasGroupstage2.alpha = SetAlpha;
    //            TitleCanvasGroupstage3.alpha = 1;
    //            TitleCanvasGroupstage4.alpha = 1;
    //            break;
    //        case 3:
    //            TitleCanvasGroupstage1.alpha = 1;
    //            TitleCanvasGroupstage2.alpha = 1;
    //            TitleCanvasGroupstage3.alpha = SetAlpha;
    //            TitleCanvasGroupstage4.alpha = 1;
    //            break;
    //        case 4:
    //            TitleCanvasGroupstage1.alpha = 1;
    //            TitleCanvasGroupstage2.alpha = 1;
    //            TitleCanvasGroupstage3.alpha = 1;
    //            TitleCanvasGroupstage4.alpha = SetAlpha;
    //            break;
    //    }
    //}

    void SelectUiChange()
    {
        switch(NumSelect)
        {
            //case 1:
            //    PushStartImg.GetComponent<MotionButton>().ThisButtonSelecting = true;
            //    //Open_PushStartImg.SetActive(true);

            //    //選択中側
            //    Open_OptionImg.SetActive(false);
            //    Open_CreditImg.SetActive(false);
            //    Open_GameEndImg.SetActive(false);

            //    //選択されてない側
            //    OptionImg.SetActive(true);
            //    CreditImg.SetActive(true);
            //    GameEndImg.SetActive(true);

            //    break;

            //case 2:
            //    OptionImg.SetActive(false);
            //    Open_OptionImg.SetActive(true);

            //    //選択中側
            //    PushStartImg.GetComponent<MotionButton>().ThisButtonSelecting = false ;
            //    //Open_PushStartImg.SetActive(false);
            //    Open_CreditImg.SetActive(false);
            //    Open_GameEndImg.SetActive(false);

            //    //選択されてない側
            //    //PushStartImg.SetActive(true);
            //    CreditImg.SetActive(true);
            //    GameEndImg.SetActive(true);
            //    break;

            //case 3:
            //    CreditImg.SetActive(false);
            //    Open_CreditImg.SetActive(true);

            //    //選択中側
            //    PushStartImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
            //    //Open_PushStartImg.SetActive(false);
            //    Open_OptionImg.SetActive(false);
            //    Open_GameEndImg.SetActive(false);

            //    //選択されてない側
            //    OptionImg.SetActive(true);
            //    //PushStartImg.SetActive(true);
            //    GameEndImg.SetActive(true);
            //    break;

            //case 4:
            //    GameEndImg.SetActive(false);
            //    Open_GameEndImg.SetActive(true);

            //    //選択中側
            //    PushStartImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
            //    //Open_PushStartImg.SetActive(false);
            //    Open_OptionImg.SetActive(false);
            //    Open_CreditImg.SetActive(false);

            //    //選択されてない側
            //    OptionImg.SetActive(true);
            //    CreditImg.SetActive(true);
            //    //PushStartImg.SetActive(true);
            //    break;
            default:
                break;
        }
    }

    void SelectUiChange2()
    {
        switch (NumSelect)
        {
            case 1:
                PushStartImg.GetComponent<MotionButton>().ThisButtonSelecting = true;
                OptionImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
                CreditImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
                GameEndImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
                break;

            case 2:
                PushStartImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
                OptionImg.GetComponent<MotionButton>().ThisButtonSelecting = true;
                CreditImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
                GameEndImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
                break;

            case 3:
                PushStartImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
                OptionImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
                CreditImg.GetComponent<MotionButton>().ThisButtonSelecting = true;
                GameEndImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
                break;

            case 4:
                PushStartImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
                OptionImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
                CreditImg.GetComponent<MotionButton>().ThisButtonSelecting = false;
                GameEndImg.GetComponent<MotionButton>().ThisButtonSelecting = true;
                break;
            default:
                break;
        }
    }

    void SettingScene()
    {
        TitleCanvas.SetActive(false);
        SettingCanvas.SetActive(true);
        CreditCanvas.SetActive(false);
        CloudCanvas.SetActive(false);

    }

    void CreditScene()
    {
        TitleCanvas.SetActive(false);
        SettingCanvas.SetActive(false);
        CloudCanvas.SetActive(false);
        CreditCanvas.SetActive(true);
    }

    void MenuMove()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        if (PadWaitTime > 0)
        {
            PadWaitTime -= Time.unscaledDeltaTime;
        }
        //ボタン押してからスタートから下矢印押すとオプションに移動する
        if ((Input.GetKeyDown(KeyCode.DownArrow) && NumSelect == 1)||vert<0&&NumSelect==1 && PadWaitTime <= 0)
        {
            titleAudio.PlayOneShot(SE_ItemChange);
            NumSelect = 2;
            PadWaitTime = SetWaitTime;
        }

        //オプションから右矢印を押すとクレジットに移動する
        if ((Input.GetKeyDown(KeyCode.RightArrow) && NumSelect == 2 )|| hori > 0 && NumSelect == 2 && PadWaitTime <= 0)
        {
            titleAudio.PlayOneShot(SE_ItemChange);
            NumSelect = 3;
            PadWaitTime = SetWaitTime;
        }

        //オプションから左矢印でゲームを終わる
        if ((Input.GetKeyDown(KeyCode.LeftArrow) && NumSelect == 2)||hori<0&&NumSelect== 2 && PadWaitTime <= 0)
        {
            titleAudio.PlayOneShot(SE_ItemChange);
            NumSelect = 4;
            PadWaitTime = SetWaitTime;
        }


        //クレジットからオプションに移動
        if ((Input.GetKeyDown(KeyCode.LeftArrow) && NumSelect == 3)||hori<0&&NumSelect== 3 && PadWaitTime <= 0)
        {
            titleAudio.PlayOneShot(SE_ItemChange);
            NumSelect = 2;
            PadWaitTime = SetWaitTime;
        }


        //ゲームを終わるからオプションに移動
        if ((Input.GetKeyDown(KeyCode.RightArrow) && NumSelect == 4)||hori>0&&NumSelect== 4 && PadWaitTime <= 0)
        {
            titleAudio.PlayOneShot(SE_ItemChange);
            NumSelect = 2;
            PadWaitTime = SetWaitTime;
        }

        //ボタンを押してスタート以外のボタンから上矢印押したときボタンを押してスタートに移動する
        if (NumSelect == 4 || NumSelect == 3 || NumSelect == 2)
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow))||vert> 0 && PadWaitTime <= 0)
            {
                titleAudio.PlayOneShot(SE_ItemChange);
                NumSelect = 1;
                PadWaitTime = SetWaitTime;
            }
        }
    }

    void Darkening()
    {
        WaitTime += Time.deltaTime;

        if (DarkeningAlpha < 1 )
        {
            DarkeningAlpha += 0.8f * Time.deltaTime;
        }

        if(DarkeningAlpha >= 1  )
        {
            switch (NumSelect)
            {
                case 1:
                    SceneManager.LoadScene("StageSelect");
                    Push = false;
                    FeedEffect.FlgEffect = true;
                    break;
                case 2:
                    SettingScene();
                    TitleOther = true;
                    Push = false;
                    DarkeningAlpha = 0.0f;
                    break;
                case 3:
                    CreditScene();
                    TitleOther = true;
                    Push = false;
                    DarkeningAlpha = 0.0f;
                    break;
                case 4:
                    break;
            }
        }
        DarkeningImg.alpha = DarkeningAlpha;
    }
}
