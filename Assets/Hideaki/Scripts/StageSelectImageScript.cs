using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectImageScript : MonoBehaviour
{
    public int SelectingStageNum = 1;
    int StageMaxNum = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectingStageNum < StageMaxNum)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(5.0f, 0.0f, 0.0f);
                SelectingStageNum += 1;
            }
        }
        if (1 < SelectingStageNum)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-5.0f, 0.0f, 0.0f);
                SelectingStageNum -= 1;
            }
        }
    }
}
