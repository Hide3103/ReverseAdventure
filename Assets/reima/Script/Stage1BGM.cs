﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1BGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.IsGoal||MotionPlayer.m_PlayerHp==0)
        {
            Destroy(this.gameObject);
        }
    }
}
