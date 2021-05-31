using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpone : MonoBehaviour
{

    public bool RockAlive = false;
    public GameObject RockPrehub;

    public float RockDeathPosY = 0.0f;
    public float RoringSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        RockAlive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(RockAlive==false)
        {
            GameObject rockPrefub = Instantiate(RockPrehub, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            rockPrefub.GetComponent<Rock>().rockSpone = this.gameObject;
            rockPrefub.GetComponent<Rock>().DestroyPosY = RockDeathPosY;
            rockPrefub.GetComponent<Rock>().RoringSpeed = RoringSpeed;

            RockAlive = true;
        }
    }
}
