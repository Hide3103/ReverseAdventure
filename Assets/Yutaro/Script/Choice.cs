using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Choice : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PushBotton()
    {
        SceneManager.LoadScene("StageSelect");
    }
    //public void OnClick()
    //{
    //    SceneManager.LoadScene("StageSelect");//()の中に移動先のシーン名
    //}

}
