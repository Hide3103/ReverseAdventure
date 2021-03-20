using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool IsGoal = false;
    public GameObject MainCamera;
    public GameObject GoalCamera;
    // Start is called before the first frame update
    void Start()
    {
        GoalCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsGoal)
        {
            GoalCamera.SetActive(true);
            MainCamera.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            IsGoal = true;
        }
    }
}
