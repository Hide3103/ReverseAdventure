using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{

    public Text text;
    GameObject itemmanage;
    [SerializeField]
    private float TextScrollSpeed = 30;
    //　テキストの制限位置
    [SerializeField]
    private float LimitPosition = 2000f;
    //　エンドロールが終了したかどうか
    private bool StopEndRoll;

    GameObject SceneManage;

    public float WaitTime = 0;
    public float UpPoint;

    public float SetAlpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        UpPoint = 1080 / 4;
    }

    // Update is called once per frame
    void Update()
    {
        WaitTime += Time.deltaTime;
        //　エンドロール用テキストがリミットを越えるまで動かす
        if (transform.position.y <= LimitPosition)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + TextScrollSpeed * Time.deltaTime);
        }
        else
        {
            StopEndRoll = true;
        }


        text.text =
                                "                                       \n" +
                                "獲得したもの\n" +
                                "                                      \n ";

        //TitleBack.SetActive(true);
        //if (TitleBack.transform.position.y < UpPoint)
        //{
        //    TitleBack.transform.position = new Vector3(TitleBack.transform.position.x, TitleBack.transform.position.y + 1, TitleBack.transform.position.z);
        //}
    }
}