using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{

    public GameObject TitleCanvas;
    public GameObject SettingCanvas;

    //Start is called before the first frame update
    void Start()
    {

    }


    //Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingScene();
        }
    }
    void SettingScene()
    {
        TitleCanvas.SetActive(!TitleCanvas.activeSelf);
        SettingCanvas.SetActive(!SettingCanvas.activeSelf);
    }
}
