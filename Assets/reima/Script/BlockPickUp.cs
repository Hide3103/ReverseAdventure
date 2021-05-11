using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPickUp : MonoBehaviour
{
    private GameObject Player;
    private GameObject SetBlockPos;
    private bool Have = false;
    private float BlockSetPos = 0.5f;
    bool Set;
    bool Flg_Throw;
    float WaitTime = 0;
    float SetWaitTime = 0.5f;
    float ThrowPowwer = 200;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("MotionPlayer");
        SetBlockPos = GameObject.Find("Player/BlockPos");
        var rb = this.transform.gameObject.GetComponent<Rigidbody2D>();
        if (Have == false)
        {
            rb.simulated = true;
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(WaitTime>0)
        {
            WaitTime -= Time.deltaTime;
        }
        var rb = this.transform.gameObject.GetComponent<Rigidbody2D>();
        if (Have)
        {
            this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + BlockSetPos, Player.transform.position.z);
            if ((Input.GetKeyDown(KeyCode.V)) && Have &&WaitTime<=0|| Input.GetKeyDown("joystick button 2") && Have == true && WaitTime <= 0)//X
            {

                rb.isKinematic = false;
                rb.velocity = new Vector2(0, 0);
                Have = false;
                Flg_Throw = false;

                if (Player.transform.localScale.x > 0)
                {
                    rb.AddForce(new Vector3(ThrowPowwer, 0, 0));
                }
                if (Player.transform.localScale.x < 0)
                {
                    rb.AddForce(new Vector3(-ThrowPowwer, 0, 0));
                }

                //this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + BlockSetPos, Player.transform.position.z)
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var rb = this.transform.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.V) && Have == false || Input.GetKeyDown("joystick button 2") && Have == false)
            {
                Have = true;
                rb.freezeRotation = true;
                WaitTime += SetWaitTime;
            }
        }
    }
}
