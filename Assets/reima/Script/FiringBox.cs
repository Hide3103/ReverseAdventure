using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringBox : MonoBehaviour
{ 
    public GameObject AroowPrehub;
    float IntervalTime = 0;
    bool Go = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IntervalTime += Time.deltaTime;
        if (3 < IntervalTime)
        {
            if (Go)
            {
                Instantiate(AroowPrehub, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
                IntervalTime = 0;
                Go = false;
            }
            if(Go==false)
            {
                if(IntervalTime>3)
                {
                    Go = true;
                }
            }
        }
    }
}
