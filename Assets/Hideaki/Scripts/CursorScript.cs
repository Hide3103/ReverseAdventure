using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public bool ButtonPressed = false;
    SpriteRenderer spriteRenderer = null;
    Color firstColor;
    Color invisibleColor;
    float flashLimit = 0.1f;
    public float flashDelta = 0.0f;

    public bool ThisCursorIsLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        firstColor = spriteRenderer.color;
        invisibleColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(ThisCursorIsLeft == true)
        {
            if(StageSelectCameraScript.SelectingStageNum == 1 || StageSelectCameraScript.SelectingStageNum == 0)
            {
                spriteRenderer.color = invisibleColor;
            }
            else
            {
                PressedAction();
            }
        }
        else
        {
            if(StageSelectCameraScript.SelectingStageNum == StageSelectCameraScript.StageMaxNum || StageSelectCameraScript.SelectingStageNum == 0)
            {
                spriteRenderer.color = invisibleColor;
            }
            else
            {
                PressedAction();
            }
        }


    }

    void PressedAction()
    {
        if (ButtonPressed == true)
        {
            //float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(level, level, level, 1);

            if (flashDelta < flashLimit)
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
