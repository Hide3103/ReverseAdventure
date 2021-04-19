using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public static int NumSelect = 1;

    public CanvasGroup TitleCanvasGroupstage1;
    public CanvasGroup TitleCanvasGroupstage2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(NumSelect==1&&Input.GetKeyDown(KeyCode.RightArrow))
        {
            NumSelect++;
        }
        if(NumSelect==2&&Input.GetKeyDown(KeyCode.LeftArrow))
        {
            NumSelect--;
        }

        switch (NumSelect)
        {
            case 1:
                if (Input.GetKeyDown(KeyCode.Return))
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
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("StageSelect");
                }
                break;

        }
    }
}
