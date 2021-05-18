using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{

    public GameObject TitleCanvas;
    public GameObject SettingCanvas;
    public GameObject CloudCanvas;


    Title TitleSC;
    //Start is called before the first frame update
    void Start()
    {
        TitleSC = TitleCanvas.GetComponent<Title>();
    }


    //Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown("joystick button 1"))
        {
            SettingScene();
            TitleSC.NumSelect = 1;
        }
    }
    void SettingScene()
    {
        TitleCanvas.SetActive(!TitleCanvas.activeSelf);
        SettingCanvas.SetActive(!SettingCanvas.activeSelf);
        CloudCanvas.SetActive(!CloudCanvas.activeSelf);
    }
}
