using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (BackGoat.flg_moveToPlayer == false)
            {
                BackGoat.SetDirection(-1.0f);
            }

            if (Goat.flg_moveToPlayer == false)
            {
                Goat.SetDirection(-1.0f);
            }
        }
    }
}
