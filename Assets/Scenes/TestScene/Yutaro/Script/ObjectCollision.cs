using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    public float boundHeight; //このオブジェクトを踏んだ時にはねる高さ

    public bool playerStepOn = false;

    void Start()
    {
    }

    void Update()
    {
        if(playerStepOn == false)
        {
            Debug.Log("c");
        }
        else
        {
            Debug.Log("d");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerStepOn = true;
            Debug.Log("b");
        }
        else
        {
            Debug.Log("a");
        }
    }
}
