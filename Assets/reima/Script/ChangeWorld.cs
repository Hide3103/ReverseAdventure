using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWorld : MonoBehaviour
{
    public GameObject UraObj;
    public GameObject OmoteObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            UraObj.SetActive(!UraObj.activeSelf);
            OmoteObj.SetActive(!OmoteObj.activeSelf);
        }
        if(GameSystem.IsGoal==true)
        {
            UraObj.SetActive(false);
            OmoteObj.SetActive(true);
        }
    }
}
