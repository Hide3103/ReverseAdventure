using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    bool attackFlg = false;

    float attackDelta = 0.0f;
    float limmitDelta = 0.5f;

    public bool testFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf == true)
        {
            if (attackDelta < limmitDelta)
            {
                attackDelta += Time.deltaTime;
                testFlg = false;
            }
            else
            {
                gameObject.SetActive(false);
                attackDelta = 0.0f;
                testFlg = true;
            }

        }
    }
}
