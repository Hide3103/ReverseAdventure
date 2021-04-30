using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeBlock : MonoBehaviour
{
    private GameObject Player;
    private GameObject SetBlockPos;
    private bool have = false;
    private float BlockSetPos = 1.0f;
    bool Set;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        SetBlockPos = GameObject.Find("Player/BlockPos");
        var rb = this.transform.gameObject.GetComponent<Rigidbody2D>();
        if (have == false)
        {
            rb.simulated = true;
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var rb = this.transform.gameObject.GetComponent<Rigidbody2D>();
        if (have)
        {
            rb.freezeRotation = true;
            this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + BlockSetPos, Player.transform.position.z);
        }
        if (have && (Input.GetKeyDown(KeyCode.V)))
        {
            rb.isKinematic = false;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector3(100, 0, 0));
            have = false;
            //this.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + BlockSetPos, Player.transform.position.z)
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.B) && have == false)
            {
                have = true;
            }
        }
    }
}
