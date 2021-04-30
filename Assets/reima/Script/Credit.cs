using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{

    public Text text;
    GameObject itemmanage;
    [SerializeField]
    private float TextScrollSpeed = 30;
    //　エンドロールが終了したかどうか
    private bool StopEndRoll;

    GameObject SceneManage;


    public float WaitTime = 0;
    //　テキストの制限位置
    public float UpPoint;

    public float SetAlpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        UpPoint = 1080 / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }

        WaitTime += Time.deltaTime;
        //　エンドロール用テキストがリミットを越えるまで動かす
        if (transform.position.y <= UpPoint)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + TextScrollSpeed * Time.deltaTime);
        }
        else
        {
            StopEndRoll = true;
        }


        text.text =

            "                  使用したサイト                     \n" +
            "                                       \n" +
            "                                       \n" +


                                //"                                       \n" +
                                //"リーダー　：　(名前入力)\n" +
                                //"                                      \n " +
                                //"副リーダー　：　(名前入力)\n" +
                                //"                                      \n " +
                                //"プランナー　：　(名前入力)\n" +
                                //"                                      \n " +
                                //"プログラマー　：　(名前入力)\n" +
                                //"                                      \n " +
                                //"グラフィッカー　：　(名前入力)\n" +
                                //"                                      \n " +
                                //"                                      \n " +
                                //"                                      \n " +
                                "ESCで戻る\n";
    }
}