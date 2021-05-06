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
    float SetWaitTime = 1;
    // Start is called before the first frame update
    void Start()
    {

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
            NumSelect--;
            WaitTime = SetWaitTime = 1; ;
        }
        if(NumSelect==2&&Input.GetKeyDown(KeyCode.LeftArrow) || (hori > 0 && NumSelect < 2 && WaitTime <= 0))
        {
            NumSelect++;
            WaitTime = SetWaitTime = 1; ;

        }

        switch (NumSelect)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
                {
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
                    SceneManager.LoadScene("StageSelect");
                }
                break;

        }
    }
}
