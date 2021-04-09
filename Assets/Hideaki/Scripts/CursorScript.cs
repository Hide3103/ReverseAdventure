using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public bool ButtonPressed = false;
    SpriteRenderer spriteRenderer = null;
    Color firstColor;
    float flashLimit = 0.1f;
    public float flashDelta = 0.0f;

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
            //float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(level, level, level, 1);

            if(flashDelta < flashLimit)
            {
                flashDelta += Time.deltaTime;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1);
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
