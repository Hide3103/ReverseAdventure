using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Darkening : MonoBehaviour
{
    public CanvasGroup TitleCanvasGroup1;
    public float SetAlpha = 0.0f;
    float WaitTime = 0;
    public static bool DarkeningOn;
    float FeedTimne = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            WaitTime += Time.deltaTime;

            if (SetAlpha <1&&DarkeningOn)
            {
                SetAlpha += FeedTimne * Time.deltaTime;
            }
            if(SetAlpha<=1)
            {
            DarkeningOn = false;
            }
        TitleCanvasGroup1.alpha = SetAlpha;
    }
}
