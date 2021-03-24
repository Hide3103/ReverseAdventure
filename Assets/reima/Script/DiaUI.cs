using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaUI : MonoBehaviour
{
    public Text DiaUIText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DiaUIText.text = ("× " + GameSystem.NumJewel);
    }
}
