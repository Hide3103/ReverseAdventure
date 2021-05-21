using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brightlyEffect : MonoBehaviour
{
    public CanvasGroup TitleCanvasGroup1;
    public float SetAlpha = 0.0f;
    float WaitTime = 0;
    float FeedTimne = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        WaitTime += Time.deltaTime;

        if (SetAlpha >= 0 && Darkening.DarkeningOn==false)
        {
            SetAlpha -= FeedTimne * Time.deltaTime;
        }
        TitleCanvasGroup1.alpha = SetAlpha;


    }
}
