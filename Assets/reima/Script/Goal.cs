using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject GoalCamera;
    public float StayTime = 0;

    SpriteRenderer spriteRenderer;
    public Sprite open;
    public Sprite close;

    // Start is called before the first frame update
    void Start()
    {
        GoalCamera.SetActive(false);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.IsGoal)
        {
            spriteRenderer.sprite = open;

            StayTime += Time.unscaledDeltaTime;
            if (StayTime > 3)
            {
                GoalCamera.SetActive(true);
                MainCamera.SetActive(false);
                SceneManager.LoadScene("Result");
            }
        }
        else
        {
            spriteRenderer.sprite = close;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            GameSystem.IsGoal = true;
            Time.timeScale = 0;
        }
    }
}
