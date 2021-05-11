using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPickUp : MonoBehaviour
{
    private GameObject Player;
    private GameObject SetBlockPos;
    private bool Have = false;
    private float BlockSetPos = 1.0f;
    bool Set;
    bool Flg_Throw;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        SetBlockPos = GameObject.Find("Player/BlockPos");
        var rb = this.transform.gameObject.GetComponent<Rigidbody2D>();
        if(Have ==false)
        {
            rb.simulated = true;
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var rb = this.transform.gameObject.GetComponent<Rigidbody2D>();
        if (Have)
        {
            rb.freezeRotation = true;
            this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + BlockSetPos, Player.transform.position.z);
            if(Input.GetKeyDown(KeyCode.B) && Have == true|| Input.GetKeyDown("joystick button 2") && Have == true)
            {
                Flg_Throw = true;
            }
        }
        if((Input.GetKeyDown(KeyCode.V))&&Have&&Flg_Throw|| Input.GetKeyDown("joystick button 2") && Have == true&&Flg_Throw)//X
        {
            rb.isKinematic = false;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector3(100,0,0));
            Have = false;
            Flg_Throw = false;

            //this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + BlockSetPos, Player.transform.position.z)
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.B)&&Have ==false || Input.GetKeyDown("joystick button 2") && Have == false)
            {

                Have = true;
            }
        }
    }
}
