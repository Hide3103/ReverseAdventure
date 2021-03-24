using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectCameraScript : MonoBehaviour
{
    public int SelectingStageNum = 1;
    int NowSelectingStageNum = 0;
    int StageMaxNum = 5;

    [SerializeField]
    bool MovingFlg = false;

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



        if(Input.GetKeyDown(KeyCode.X))
        {
            switch(SelectingStageNum)
            {
                case 1:
                    SceneManager.LoadScene("Stage_1");
                    break;
            }
        }
    }
}
