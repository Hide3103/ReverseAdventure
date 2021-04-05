using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject TitleImgPart1;
    public GameObject TitleImgPart2;
    int ImagePartRandom;


    public CanvasGroup TitleCanvasGroupstage1;

    float FlashTime = 0;
    float FlashSpeed = 0.8f;
    float WaitTime = 0;

    public float SetAlpha = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        ImagePartRandom = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        Flash();
        switch (ImagePartRandom)
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
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("StageSelect");
        }
    }

    void Flash()
    {
        FlashTime += Time.deltaTime;
        if (FlashTime < 1.5 && SetAlpha < 1)
        {
            if (SetAlpha < 1)
            {
                SetAlpha += FlashSpeed * Time.deltaTime;
            }
            TitleCanvasGroupstage1.alpha = SetAlpha;
        }
        if (FlashTime > 1.5 && SetAlpha > 0)
        {
            SetAlpha -= FlashSpeed * Time.deltaTime;
            if (SetAlpha < 0)
            {
                FlashTime = 0;
            }
        }
        TitleCanvasGroupstage1.alpha = SetAlpha;
    }
}
