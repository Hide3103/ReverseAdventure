using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject TitleImgPart1;
    public GameObject TitleImgPart2;
    int ImagePartRandom;

    // Start is called before the first frame update
    void Start()
    {
        ImagePartRandom = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(ImagePartRandom);
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
}
