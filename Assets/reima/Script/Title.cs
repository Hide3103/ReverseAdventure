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


    int NumSelect = 1;
    //1=ボタンを押してスタート
    //2=オプション
    //3=クレジット
    //4=ゲームを終わる
    float FlashTime = 0;
    float FlashSpeed = 0.8f;
    float WaitTime = 0;

    public float SetAlpha = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        LogImagePartRandom = Random.Range(1, 3);
        TitleImagePartRandom = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        MenuMove();
        switch (NumSelect)
        {
            case 1:
                StartFlash();
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("StageSelect");
                }
                break;
            case 2:
                StartFlash();
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SettingScene();
                }
                break;
            case 3:
                StartFlash();
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("CreditScene");
                }
                break;
            case 4:
                StartFlash();
                if (Input.GetKeyDown(KeyCode.Return))
                {
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
        //ボタン押してからスタートから下矢印押すとオプションに移動する
        if (Input.GetKeyDown(KeyCode.DownArrow) && NumSelect == 1)
        {
            NumSelect = 2;
        }

        //オプションから右矢印を押すとクレジットに移動する
        if (Input.GetKeyDown(KeyCode.RightArrow) && NumSelect == 2)
        {
            NumSelect = 3;
        }

        //オプションから左矢印でゲームを終わる
        if (Input.GetKeyDown(KeyCode.LeftArrow) && NumSelect == 2)
        {
            NumSelect = 4;
        }


        //クレジットからオプションに移動
        if (Input.GetKeyDown(KeyCode.LeftArrow) && NumSelect == 3)
        {
            NumSelect = 2;
        }


        //ゲームを終わるからオプションに移動
        if (Input.GetKeyDown(KeyCode.RightArrow) && NumSelect == 4)
        {
            NumSelect = 2;
        }

        //ボタンを押してスタート以外のボタンから上矢印押したときボタンを押してスタートに移動する
        if (NumSelect == 4 || NumSelect == 3 || NumSelect == 2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                NumSelect = 1;
            }
        }
    }

}
