using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private float ArrowSpeed = 50.0f;

    private bool DamageOn = true;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
         rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangeWorld.StateFront)
        {
            this.rb.AddForce(transform.right * -ArrowSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (DamageOn)
            {
                MotionPlayer playerScript;
                playerScript = collision.gameObject.GetComponent<MotionPlayer>();
                playerScript.Damage();
                DamageOn = false;
                Destroy(this.gameObject);
            }
        }
        if (collision.transform.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Player")
    //    {
    //        if (DamageOn)
    //        {
    //            CharacterControl2 playerScript;
    //            playerScript = collision.gameObject.GetComponent<CharacterControl2>();
    //            playerScript.Damage();
    //            DamageOn = false;
    //            Destroy(this.gameObject);
    //        }
    //    }

    //}
}
