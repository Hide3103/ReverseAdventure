using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeedEffect : MonoBehaviour
{
    public CanvasGroup TitleCanvasGroup1;
    public float SetAlpha = 0.0f;
    float WaitTime = 0;
    public static bool DarkeningOn =false;
    float FeedTimne = 1;
    public static bool FlgEffect;
    public static bool GoScene;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(FlgEffect+ "FlgEffect");
        Debug.Log(DarkeningOn+ "DarkeningOn");
        Debug.Log(GoScene + "GoScene");
        if (FlgEffect)
        {
            if (DarkeningOn)
            {
                Darkening();
            }
            if (DarkeningOn == false)
            {
                Bright();
            }
        }
    }

    void Darkening()
    {
        WaitTime += Time.deltaTime;

        if (SetAlpha < 1 && DarkeningOn)
        {
            SetAlpha += FeedTimne * Time.deltaTime;
        }
        if (SetAlpha >= 1 )
        {
            GoScene = true;
        }
        TitleCanvasGroup1.alpha = SetAlpha;
    }

    void Bright()
    {
        WaitTime += Time.deltaTime;

        if (SetAlpha >= 0 && DarkeningOn == false)
        {
            SetAlpha -= FeedTimne * Time.deltaTime;
        }
        if(SetAlpha<0)
        {
        }
        TitleCanvasGroup1.alpha = SetAlpha;
    }
}
