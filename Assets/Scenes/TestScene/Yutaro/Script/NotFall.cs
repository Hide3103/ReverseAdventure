using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotFall : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "MoveFloor")
        {
            transform.SetParent(collision.transform);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "MoveFloor")
        {
            transform.SetParent(null);
        }
    }
}
