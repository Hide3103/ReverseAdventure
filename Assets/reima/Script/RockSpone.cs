using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpone : MonoBehaviour
{

    public static bool RockAlive = false;
    public GameObject RockPrehub;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(RockAlive==false)
        {
            Instantiate(RockPrehub, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            RockAlive = true;
        }
    }
}
