using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

//[System.Serializable]
//public class SaveDate : ISerializationCallbackReceiver
//{


//    private static SaveDate instance = null;
//    public static SaveDate Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                Load();
//            }
//            return instance;
//        }
//    }

//    private static string _jsonText = "";
//}
//public class SaveDate : MonoBehaviour
//{

//    bool[] Stage1GetNumJewel = new bool[] { };

//    bool Stage1Jewel1;
//    bool Stage1Jewel2;
//    bool Stage1Jewel3;
//    bool Stage1Jewel4;
//    bool Stage1Jewel5;

//    void Start()
//    {

//    }

//    Update is called once per frame
//    void Update()
//    {
//        データチェック
//        if (PlayerPrefs.HasKey("NumStageClear"))
//        {
//            Debug.Log("データあり");
//            PlayerPrefs.GetInt("NumStageClear");
//        }
//        else
//        {
//            Debug.Log("データなし");
//        }
//        if (Input.GetKeyDown(KeyCode.B))
//        {
//            PlayerPrefs.DeleteKey("NumStageClear");
//            Debug.Log("データけし");
//        }
//        ステージのクリア状況を保存
//        PlayerPrefs.SetInt("NumStageClear", GameSystem.ToSaveNumData);

//        PlayerPrefs.Save();


//        //forで1ステージの取得数を取得
//        for (int i = 0; i < 5; i++)
//        {
//            Stage1GetNumJewel[i] = GameSystem.GetJuwelCollection(i, 1);
//        }

//        bool Bool_Stage1Jewel1 = Stage1GetNumJewel[1];
//        bool Bool_Stage1Jewel2 = Stage1GetNumJewel[2];
//        bool Bool_Stage1Jewel3 = Stage1GetNumJewel[3];
//        bool Bool_Stage1Jewel4 = Stage1GetNumJewel[4];
//        bool Bool_Stage1Jewel5 = Stage1GetNumJewel[5];

//        Debug.Log("Bool_Stage1Jewel1" + Bool_Stage1Jewel1);
//        Debug.Log("Bool_Stage1Jewel1" + Bool_Stage1Jewel2);
//        Debug.Log("Bool_Stage1Jewel1" + Bool_Stage1Jewel3);
//        Debug.Log("Bool_Stage1Jewel1" + Bool_Stage1Jewel4);
//        Debug.Log("Bool_Stage1Jewel1" + Bool_Stage1Jewel5);
//    }
//}
