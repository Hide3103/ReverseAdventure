using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("");//()の中に移動先のシーン名
    }
}
