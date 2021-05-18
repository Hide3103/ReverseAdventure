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
                SetAlpha += 0.7f * Time.deltaTime;
            }
            TitleCanvasGroup1.alpha = SetAlpha;
    }
}
