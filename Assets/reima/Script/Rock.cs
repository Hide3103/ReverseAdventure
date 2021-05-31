using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    bool DamageOn = true;
    float DestroyPosY = -10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3( -1 * Time.deltaTime,0, 0);
        this.transform.Rotate(new Vector3(0, 0, +5));

        if (this.transform.position.y <= DestroyPosY)
        {
            Destroy(this.gameObject);
            RockSpone.RockAlive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (DamageOn)
            {
                GetComponent<MotionPlayer>().Damage();
                DamageOn = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (DamageOn)
            {
                collision.gameObject.GetComponent<MotionPlayer>().Damage();
                DamageOn = false;
            }
        }
    }
}
