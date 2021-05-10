using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDuringJudgment : MonoBehaviour
{
    public static bool DuringWall = false;
    public GameObject Player;
    public GameObject Reference_Obj;
    private GameObject TileMap;
    private Vector3 SetAfterSwitchingPlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        TileMap = Reference_Obj.transform.Find("Grid_Back/Tilemap").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(DuringWall==true)
        {
            Player.transform.position = SetAfterSwitchingPlayerPos;
            DuringWall = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "OmoteTilemap")
        {
            DuringWall = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Judgment_Obj")
        {
            Vector3 playerPos = Player.transform.position;
            SetAfterSwitchingPlayerPos = new Vector3(playerPos.x, playerPos.y + 1.0f, playerPos.z);
        }
    }
}
