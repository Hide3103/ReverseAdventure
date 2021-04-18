using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeFloor : MonoBehaviour
{
    float BrokeTime = 0.0f;
    float SetBrokeTime = 0;

    float PosX;
    // Start is called before the first frame update
    void Start()
    {
        PosX = this.gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(BrokeTime);
        if (BrokeTime > 5)
        {
            this.gameObject.transform.position += new Vector3(0, -1 * Time.deltaTime, 0);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            BrokeTime += Time.deltaTime;
            if(BrokeTime>0)
            {
                this.transform.position = new Vector3(PosX + Mathf.PingPong(Time.time, 0.1f), this.transform.position.y, this.transform.position.z);
            }
        }
    }
}
