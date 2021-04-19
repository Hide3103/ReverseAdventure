using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private int ArrowSpeed = 3;
    private bool DamageOn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += new Vector3(-ArrowSpeed * Time.deltaTime, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            if (DamageOn)
            {
                PlayerScript.m_PlayerHp -= 1;
                DamageOn = false;
                Destroy(this.gameObject);
            }
        }
    }
}
