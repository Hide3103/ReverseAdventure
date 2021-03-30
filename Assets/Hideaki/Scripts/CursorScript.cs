﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public bool ButtonPressed = false;
    SpriteRenderer spriteRenderer = null;
    Color firstColor;
    float flashLimit = 0.3f;
    float flashDelta = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        firstColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonPressed == true)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(firstColor.r, firstColor.g, level, 1);

            if(flashDelta < flashLimit)
            {
                flashDelta += Time.deltaTime;
            }
            else
            {
                flashDelta = 0.0f;
                ButtonPressed = false;
            }
        }
        else
        {
            spriteRenderer.color = firstColor;
        }
    }
}
