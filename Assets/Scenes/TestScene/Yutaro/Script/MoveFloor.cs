using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [Header("移動経路")] public GameObject[] MovePoint;
    public float speed = 1.0f;
    public bool MoveFlg = false;
    public GameObject Tape;
    Tape TapeScript;

    public GameObject Movefloor;

    private Rigidbody2D rb;
    private int NowPoint = 0;
    private bool ReturnPoint = false;
    private Vector2 OldPos = Vector2.zero;
    private Vector2 MyVelocity = Vector2.zero;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(MovePoint != null && MovePoint.Length > 0 && rb != null)
        {
            rb.position = MovePoint[0].transform.position;
            OldPos = rb.position;
        }

        TapeScript = Tape.GetComponent<Tape>();


    }

    public Vector2 GetVelocity()
    {
        return MyVelocity;
    }

    void Update()
    {
        if (TapeScript.Tapeflag == true)
        {
            if (MovePoint != null && MovePoint.Length > 1 && rb != null)
            {
                if (!ReturnPoint)
                {
                    int NextPoint = NowPoint + 1;

                    if (Vector2.Distance(transform.position, MovePoint[NextPoint].transform.position) > 0.1f)
                    {
                        Vector2 toVector = Vector2.MoveTowards(transform.position, MovePoint[NextPoint].transform.position, speed * Time.deltaTime);

                        rb.MovePosition(toVector);
                    }

                    else
                    {
                        rb.MovePosition(MovePoint[NextPoint].transform.position);
                        ++NowPoint;

                        if (NowPoint + 1 >= MovePoint.Length)
                        {
                            ReturnPoint = true;
                        }
                    }
                }

                else
                {
                    int NextPoint = NowPoint - 1;

                    if (Vector2.Distance(transform.position, MovePoint[NextPoint].transform.position) > 0.1f)
                    {
                        Vector2 toVector = Vector2.MoveTowards(transform.position, MovePoint[NextPoint].transform.position, speed * Time.deltaTime);

                        rb.MovePosition(toVector);
                    }

                    else
                    {
                        rb.MovePosition(MovePoint[NextPoint].transform.position);
                        --NowPoint;

                        if (NowPoint <= 0)
                        {
                            ReturnPoint = false;
                        }
                    }
                }
                MyVelocity = (rb.position - OldPos) / Time.deltaTime;
            }
        }
    }
}
