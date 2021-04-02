using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medal : MonoBehaviour
{
    float DestroyTime = 1.2f;
    float DestroyDelta = 0.0f;

    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(DestroyTime < DestroyDelta)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.transform.position = new Vector3(transform.position.x, this.transform.position.y - 5.0f * Time.unscaledDeltaTime, this.transform.position.z);
            DestroyDelta += Time.unscaledDeltaTime;
        }
    }

}
