using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    Image Img;
    public static bool DamageEffectOn = true;

    void Start()
    {
        Img = GetComponent<Image>();
        Img.color = Color.clear;
    }

    void Update()
    {
        if (DamageEffectOn)
        {
            this.Img.color = new Color(0.5f, 0f, 0f, 0.5f);
            DamageEffectOn = false;
        }
        else
        {
            this.Img.color = Color.Lerp(this.Img.color, Color.clear, Time.deltaTime);
        }
    }
}