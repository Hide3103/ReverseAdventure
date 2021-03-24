﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject door;
    public GameObject key;

    DoorManager doorScript;
    
    // Start is called before the first frame update
    void Start()
    {
        doorScript = door.GetComponent<DoorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            doorScript.SetPlayerKeyFlg(true);

            Destroy(gameObject);
            Destroy(key);
        }
    }
}
