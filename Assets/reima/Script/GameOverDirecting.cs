using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDirecting : MonoBehaviour
{
    public CanvasGroup TitleCanvasGroupstage1;
    public CanvasGroup TitleCanvasGroupstage2;
    float PlusTime = 0.5f;
    float SetAlpha1 = 0.0f;
    float SetAlpha2 = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        TitleCanvasGroupstage1.alpha = 0;
}

    // Update is called once per frame
    void Update()
    {
        if (SetAlpha1 < 1)
        {
            SetAlpha1 += PlusTime * Time.deltaTime;
        }
        TitleCanvasGroupstage1.alpha = SetAlpha1;
        if(TitleCanvasGroupstage1.alpha>=1)
        {
            SetAlpha2 += PlusTime * Time.deltaTime;
        }
        TitleCanvasGroupstage2.alpha = SetAlpha2;
    }
}
