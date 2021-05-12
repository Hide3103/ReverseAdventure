using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject TitleLogImgPart1;
    public GameObject TitleLogImgPart2;

    public GameObject TitleImgPart1;
    public GameObject TitleImgPart2;

    public GameObject TitleCanvas;
    public GameObject SettingCanvas;

    int LogImagePartRandom;
    int TitleImagePartRandom;


    public CanvasGroup TitleCanvasGroupstage1;
    public CanvasGroup TitleCanvasGroupstage2;
    public CanvasGroup TitleCanvasGroupstage3;
    public CanvasGroup TitleCanvasGroupstage4;

    public CanvasGroup DarkeningImg;


    int NumSelect = 1;
    //1=ボタンを押してスタート
    //2=オプション
    //3=クレジット
    //4=ゲームを終わる
    float FlashTime = 0;
    float FlashSpeed = 0.8f;
    float WaitTime = 0;
    float PadWaitTime = 0;
    float SetWaitTime = 1;

    public float SetAlpha = 0.0f;
    public float DarkeningAlpha = 0.0f;

    AudioSource titleAudio;
    public AudioClip SE_Enter;
    public AudioClip SE_ItemChange;
    public AudioClip SE_Cancel;

    public static bool DarkeningOn;

    bool Push = false;
    // Start is called before the first frame update
    void Start()
    {
        LogImagePartRandom = Random.Range(1, 3);
        TitleImagePartRandom = Random.Range(1, 3);

        titleAudio = GetComponent<AudioSource>();
        DarkeningAlpha = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        MenuMove();
        switch (NumSelect)
        {
            case 1:
                StartFlash();
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
                StartFlash();
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
                StartFlash();
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
                StartFlash();
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
                {
                    titleAudio.PlayOneShot(SE_Enter);
                    Application.Quit();
                }
                break;
        }

        //タイトルロゴのランダム
        switch (LogImagePartRandom)
        {
            case 1:
                TitleLogImgPart1.SetActive(true);
                TitleLogImgPart2.SetActive(false);
                break;
            case 2:
                TitleLogImgPart1.SetActive(false);
                TitleLogImgPart2.SetActive(true);
                break;
        }

        //タイトル背景のランダム
        switch (TitleImagePartRandom)
        {
            case 1:
                TitleImgPart1.SetActive(true);
                TitleImgPart2.SetActive(false);
                break;
            case 2:
                TitleImgPart1.SetActive(false);
                TitleImgPart2.SetActive(true);
                break;
        }


    }

    void StartFlash()
    {
        FlashTime += Time.deltaTime;
        if (FlashTime < 1.5 && SetAlpha < 1)
        {
            if (SetAlpha < 1)
            {
                SetAlpha += FlashSpeed * Time.deltaTime;
            }
        }
        switch (NumSelect)
        {
            case 1:
                TitleCanvasGroupstage1.alpha = SetAlpha;
                TitleCanvasGroupstage2.alpha = 1;
                TitleCanvasGroupstage3.alpha = 1;
                TitleCanvasGroupstage4.alpha = 1;
                break;
            case 2:
                TitleCanvasGroupstage1.alpha = 1;
                TitleCanvasGroupstage2.alpha = SetAlpha;
                TitleCanvasGroupstage3.alpha = 1;
                TitleCanvasGroupstage4.alpha = 1;
                break;
            case 3:
                TitleCanvasGroupstage1.alpha = 1;
                TitleCanvasGroupstage2.alpha = 1;
                TitleCanvasGroupstage3.alpha = SetAlpha;
                TitleCanvasGroupstage4.alpha = 1;
                break;
            case 4:
                TitleCanvasGroupstage1.alpha = 1;
                TitleCanvasGroupstage2.alpha = 1;
                TitleCanvasGroupstage3.alpha = 1;
                TitleCanvasGroupstage4.alpha = SetAlpha;
                break;
        }
        if (FlashTime > 1.5 && SetAlpha > 0)
        {
            SetAlpha -= FlashSpeed * Time.deltaTime;
            if (SetAlpha < 0)
            {
                FlashTime = 0;
            }
        }
        switch (NumSelect)
        {
            case 1:
                TitleCanvasGroupstage1.alpha = SetAlpha;
                TitleCanvasGroupstage2.alpha = 1;
                TitleCanvasGroupstage3.alpha = 1;
                TitleCanvasGroupstage4.alpha = 1;
                break;
            case 2:
                TitleCanvasGroupstage1.alpha = 1;
                TitleCanvasGroupstage2.alpha = SetAlpha;
                TitleCanvasGroupstage3.alpha = 1;
                TitleCanvasGroupstage4.alpha = 1;
                break;
            case 3:
                TitleCanvasGroupstage1.alpha = 1;
                TitleCanvasGroupstage2.alpha = 1;
                TitleCanvasGroupstage3.alpha = SetAlpha;
                TitleCanvasGroupstage4.alpha = 1;
                break;
            case 4:
                TitleCanvasGroupstage1.alpha = 1;
                TitleCanvasGroupstage2.alpha = 1;
                TitleCanvasGroupstage3.alpha = 1;
                TitleCanvasGroupstage4.alpha = SetAlpha;
                break;
        }
    }
    void SettingScene()
    {
        TitleCanvas.SetActive(!TitleCanvas.activeSelf);
        SettingCanvas.SetActive(!SettingCanvas.activeSelf);
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
            DarkeningAlpha += 0.4f * Time.deltaTime;
        }

        if(SetAlpha > 1  )
        {
            switch (NumSelect)
            {
                case 1:
                    SceneManager.LoadScene("StageSelect");
                    Push = false;
                    break;
                case 2:
                    SettingScene();
                    Push = false;
                    DarkeningAlpha = 0.0f;
                    break;
                case 3:
                    SceneManager.LoadScene("CreditScene");
                    break;
                case 4:
                    break;
            }
        }
        DarkeningImg.alpha = DarkeningAlpha;
    }
}
