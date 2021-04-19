using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeFloor : MonoBehaviour
{
    float BrokeTime = 0.0f;
    float SetBrokeTime = 3;
    float SpornTime = 0;
    bool ThisFall = false;

    Vector3 StartPos;
    float PosX;
    // Start is called before the first frame update
    void Start()
    {
        PosX = this.gameObject.transform.position.x;
        StartPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(BrokeTime);
        if (BrokeTime > SetBrokeTime)
        {
            ThisFall = true;
            this.gameObject.transform.position += new Vector3(0, -10 * Time.deltaTime, 0);
            SpornTime += Time.deltaTime;
        }
        if(5<SpornTime)
        {
            this.gameObject.transform.position = StartPos;
            BrokeTime = 0;
            SpornTime = 0;
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
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (ThisFall == false)
        {
            if (collision.gameObject.name == "Player")
            {
                    BrokeTime = 0;
            }
        }
    }
}
