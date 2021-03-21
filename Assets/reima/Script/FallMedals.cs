using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallMedals : MonoBehaviour
{
    public GameObject MedalPrefub;
    public GameObject MedalPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameSystem.IsGoal)
        {
            MedalFall();
        }
    }

    public void MedalFall()
    {
        Vector3 vector3;

        vector3 = new Vector3(Random.Range(MedalPos.transform.position.x - 5.0f, MedalPos.transform.position.x + 5.0f), MedalPos.gameObject.transform.position.y, transform.position.z);

        // Enemyを作成する
        GameObject Medals = (GameObject)Instantiate(MedalPrefub, vector3, Quaternion.identity);
    }
}
