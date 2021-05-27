using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{

    public GameObject TitleCanvas;
    public GameObject SettingCanvas;
    public GameObject CloudCanvas;
    public GameObject CreditCanvas;


    Title TitleSC;
    //Start is called before the first frame update
    void Start()
    {
        TitleSC = TitleCanvas.GetComponent<Title>();
    }


    //Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && Title.TitleOther || Input.GetKeyDown("joystick button 1")&&Title.TitleOther)
        {
            BackTitle();
            TitleSC.NumSelect = 1;
        }
    }
    void BackTitle()
    {
        TitleCanvas.SetActive(true);
        SettingCanvas.SetActive(false);
        CreditCanvas.SetActive(false);
        CloudCanvas.SetActive(true);
        Title.TitleOther = false;

    }
}
