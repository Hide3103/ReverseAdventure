using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    bool DamageOn = true;
    public float DestroyPosY = -10.0f;

    public GameObject rockSpone;
    RockSpone rockSponeScript;

    // Start is called before the first frame update
    void Start()
    {
        rockSponeScript = rockSpone.GetComponent<RockSpone>();

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3( -1 * Time.deltaTime,0, 0);
        this.transform.Rotate(new Vector3(0, 0, +5));

        if (this.transform.position.y <= DestroyPosY)
        {
            Destroy(this.gameObject);
            rockSponeScript.RockAlive = false;
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
