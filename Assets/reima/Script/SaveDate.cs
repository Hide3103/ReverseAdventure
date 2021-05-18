using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string SaveString = PlayerPrefs.GetString("Save_Date", "No Date");
        Debug.Log(SaveString);

        PlayerPrefs.SetString("Save_Date", "Have Date");

        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
