using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private float ArrowSpeed = 100.0f;

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
        this.rb.AddForce(transform.right * -ArrowSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "Player")
        {
            if (DamageOn)
            {
                PlayerScript playerScript;
                playerScript = collision.gameObject.GetComponent<PlayerScript>();
                playerScript.Damage();
                DamageOn = false;
                Destroy(this.gameObject);
            }
        }
        if(collision.transform.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }

}
