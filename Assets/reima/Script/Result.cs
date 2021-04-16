using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public Text GetJewel;
    public Text ClearTime;
    float StayTime = 0;
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


    // Start is called before the first frame update
    void Start()
    {
        GetJewel.gameObject.SetActive(true);
        ClearTime.gameObject.SetActive(true);
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
    }

    // Update is called once per frame
    void Update()
    {
        GetJewel.text = "獲得宝石数　:　" + GameSystem.NumJewel+"個";
        ClearTime.text = "クリアタイム　:　" + Mathf.CeilToInt(GameSystem.ClearTime)+"秒";
        //GameSystem.SetClearTime(Mathf.CeilToInt(GameSystem.ClearTime));
        GameSystem.SetStageCleared(true, GameSystem.WasPlayStage);


        StayTime += Time.deltaTime;
        SelectSystem();
    }


    //セレクトするときのシステム
    void SelectSystem()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && NowSelect < 3)
        {
            NowSelect++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && NowSelect > 1)
        {
            NowSelect--;
        }

        //セレクトシステム
        switch (NowSelect)
        {

            case 1:
                if (NowSelect == 1)
                {
                    TitleCanvasGroupstage3.alpha = 1.0f;
                    FlashTime += Time.deltaTime;
                    if (FlashTime< 1.5 && SetAlpha< 1)
                    {
                        if (SetAlpha< 1)
                        {
                            SetAlpha += FlashSpeed * Time.deltaTime;
                        }
                    TitleCanvasGroupstage1.alpha = SetAlpha;
                    }
                    if (FlashTime > 1.5 && SetAlpha > 0.4)
                    {
                        SetAlpha -= FlashSpeed * Time.deltaTime;
                        if (SetAlpha< 0.4)
                        {
                            FlashTime = 0;
                        }
                    }
                    TitleCanvasGroupstage1.alpha = SetAlpha;
                    TitleCanvasGroupstage2.alpha = 1.0f;
                    TitleCanvasGroupstage3.alpha = 1.0f;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //次にどこのステージに行くか(ステージ追加時は必須)
                    switch (GameSystem.WasPlayStage)
                    {
                        case 0:
                            break;
                        case 1:
                            SceneManager.LoadScene("Stage_2");
                            GameSystem.WasPlayStage = 2;
                            break;
                        case 2:
                            SceneManager.LoadScene("Stage_3");
                            GameSystem.WasPlayStage = 3;
                            break;
                        case 3:
                            SceneManager.LoadScene("Stage_4");
                            GameSystem.WasPlayStage = 4;
                            break;
                        case 4:
                            SceneManager.LoadScene("Stage_5");
                            GameSystem.WasPlayStage = 5;
                            break;
                    }
                }
                break;
            case 2:
                if (NowSelect == 2)
                {
                    TitleCanvasGroupstage1.alpha = 1.0f;
                    FlashTime += Time.deltaTime;
                    if (FlashTime< 1.5 && SetAlpha< 1)
                    {
                        if (SetAlpha< 1)
                        {
                            SetAlpha += FlashSpeed * Time.deltaTime;
                        }
                        TitleCanvasGroupstage2.alpha = SetAlpha;
                    }
                    if (FlashTime > 1.5 && SetAlpha > 0.4)
                    {
                        SetAlpha -= FlashSpeed * Time.deltaTime;
                        if (SetAlpha< 0.4)
                        {
                            FlashTime = 0;
                        }
                    }
                    TitleCanvasGroupstage2.alpha = SetAlpha;
                    TitleCanvasGroupstage1.alpha = 1.0f;
                    TitleCanvasGroupstage3.alpha = 1.0f;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("StageSelect");
                }
                break;
            case 3:
                if (NowSelect == 3)
                {
                    TitleCanvasGroupstage2.alpha = 1.0f;
                    FlashTime += Time.deltaTime;
                    if (FlashTime< 1.5 && SetAlpha< 1)
                    {
                        if (SetAlpha< 1)
                            SetAlpha += FlashSpeed * Time.deltaTime;
                    }
                    TitleCanvasGroupstage3.alpha = SetAlpha;
                    if (FlashTime > 1.5 && SetAlpha > 0.4)
                    {
                        SetAlpha -= FlashSpeed * Time.deltaTime;
                        if (SetAlpha< 0.4)
                        {
                            FlashTime = 0;
                        }
                        TitleCanvasGroupstage3.alpha = SetAlpha;
                        TitleCanvasGroupstage1.alpha = 1.0f;
                        TitleCanvasGroupstage2.alpha = 1.0f;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    //もう一回同じステージを読み込む(ステージ追加時は必須)
                    switch (GameSystem.WasPlayStage)
                    {
                        case 0:
                            break;
                        case 1:
                            SceneManager.LoadScene("Stage_1");
                            break;
                        case 2:
                            SceneManager.LoadScene("Stage_2");
                            break;
                        case 3:
                            SceneManager.LoadScene("Stage_3");
                            break;
                        case 4:
                            SceneManager.LoadScene("Stage_4");
                            break;
                    }
                }
                break;

        }


    }
}
