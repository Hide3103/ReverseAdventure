using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDirecting : MonoBehaviour
{
    public CanvasGroup TitleCanvasGroupstage1;
    public CanvasGroup TitleCanvasGroupstage2;
    public CanvasGroup TitleCanvasGroupstage3;
    public CanvasGroup TitleCanvasGroupstage4;

    float PlusTime = 0.5f;
    float SetAlpha1 = 0.0f;
    float SetAlpha2 = 0.0f;
    float SetAlpha3 = 1.0f;
    float SetAlpha4 = 0.0f;

    float FlashTime = 0;
    float FlashSpeed = 0.7f;
    float WaitTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        TitleCanvasGroupstage1.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (SetAlpha1 < 1)
        //{
        //    SetAlpha1 += PlusTime * Time.deltaTime;
        //}
        //TitleCanvasGroupstage1.alpha = SetAlpha1;
        //if (TitleCanvasGroupstage1.alpha >= 1)
        //{
        //    SetAlpha2 += PlusTime * Time.deltaTime;
        //}
        //TitleC-anvasGroupstage2.alpha = SetAlpha2;

        switch (GameOver.NumSelect)
        {
            case 1:
               // Flash();
                break;
            case 2:
              //  Flash();
                break;
        }
    }
    void Flash()
    {
        FlashTime += Time.deltaTime;
        switch (GameOver.NumSelect)
        {
            case 1:
                if (FlashTime < 1.5 && SetAlpha3 < 1)
                {
                    if (SetAlpha3 < 1)
                    {
                        SetAlpha3 += FlashSpeed * Time.unscaledDeltaTime;
                    }
                    TitleCanvasGroupstage3.alpha = SetAlpha3;
                }
                if (FlashTime > 1.5 && SetAlpha3 > 0.4)
                {
                    SetAlpha3 -= FlashSpeed * Time.unscaledDeltaTime;
                    if (SetAlpha3 < 0.4)
                    {
                        FlashTime = 0;
                    }
                }
                TitleCanvasGroupstage3.alpha = SetAlpha3;
                TitleCanvasGroupstage4.alpha = 1.0f;
                break;
            case 2:
                if (FlashTime < 1.5 && SetAlpha4 < 1)
                {
                    if (SetAlpha4 < 1)
                    {
                        SetAlpha4 += FlashSpeed * Time.unscaledDeltaTime;
                    }
                    TitleCanvasGroupstage4.alpha = SetAlpha3;
                }
                if (FlashTime > 1.5 && SetAlpha4 > 0.4)
                {
                    SetAlpha4 -= FlashSpeed * Time.unscaledDeltaTime;
                    if (SetAlpha4 < 0.4)
                    {
                        FlashTime = 0;
                    }
                }
                TitleCanvasGroupstage3.alpha = 1.0f;
                TitleCanvasGroupstage4.alpha = SetAlpha4;
                break;
        }
    }
}
