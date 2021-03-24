using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public Text GetCoin;
    public Text GetStar;
    public Text ClearTime;
    public Text TitleBack;
    float StayTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetCoin.gameObject.SetActive(true);
        GetStar.gameObject.SetActive(true);
        ClearTime.gameObject.SetActive(true);
        TitleBack.gameObject.SetActive(false);
        TitleBack.text = "Enterでタイトルに戻る";
    }

    // Update is called once per frame
    void Update()
    {
        GetCoin.text = "獲得コイン数　:　" + GameSystem.GetCoin;
        GetStar.text = "獲得スター数　:　" + GameSystem.GetStar;
        ClearTime.text = "クリア時間　;　" + GameSystem.ClearTime;

        StayTime += Time.deltaTime;
        if(StayTime>5)
        {
            GetCoin.gameObject.SetActive(false);
            GetStar.gameObject.SetActive(false);
            ClearTime.gameObject.SetActive(false);
            TitleBack.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}
