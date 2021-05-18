using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public static int NumSelect = 1;

    public CanvasGroup TitleCanvasGroupstage1;
    public CanvasGroup TitleCanvasGroupstage2;

    float WaitTime = 0;
    float SetWaitTime = GameSystem.SetWaitTime;

    AudioSource gameOverAudio;
    public AudioClip SE_Enter;
    public AudioClip SE_Select;
    public AudioClip SE_Cancel;

    public GameObject RetryImg;
    public GameObject StageselectImg;

    public GameObject Open_RetryImg;
    public GameObject Open_StageselectImg;
    // Start is called before the first frame update
    void Start()
    {
        gameOverAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(WaitTime);
        Debug.Log(NumSelect);
        float hori = Input.GetAxis("Horizontal");
        if (WaitTime > 0)
        {
            WaitTime -= Time.unscaledDeltaTime;
        }

        if (NumSelect==1&&Input.GetKeyDown(KeyCode.RightArrow) || (hori < 0 && NumSelect > 1 && WaitTime <= 0))
        {
            gameOverAudio.PlayOneShot(SE_Select);

            RetryImg.SetActive(false);
            Open_RetryImg.SetActive(true);

            Open_StageselectImg.SetActive(false);
            StageselectImg.SetActive(true);
            NumSelect--;
            WaitTime = SetWaitTime; ;
        }
        if(NumSelect==2&&Input.GetKeyDown(KeyCode.LeftArrow) || (hori > 0 && NumSelect < 2 && WaitTime <= 0))
        {
            gameOverAudio.PlayOneShot(SE_Select);

            RetryImg.SetActive(true);
            Open_RetryImg.SetActive(false);

            Open_StageselectImg.SetActive(true);
            StageselectImg.SetActive(false);

            NumSelect++;
            WaitTime = SetWaitTime; ;

        }

        switch (NumSelect)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
                {
                    gameOverAudio.PlayOneShot(SE_Enter);
                    switch (GameSystem.WasPlayStage)
                    {
                        case 1:
                            SceneManager.LoadScene("Stage_1");
                            break;
                        case 2:
                            SceneManager.LoadScene("Stage_2");
                            break;
                        case 3:
                            SceneManager.LoadScene("Stage_3");
                            break;
                    }
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
                {
                    gameOverAudio.PlayOneShot(SE_Enter);
                    SceneManager.LoadScene("StageSelect");
                }
                break;

        }
    }
}
