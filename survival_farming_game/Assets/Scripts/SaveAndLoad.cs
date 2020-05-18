﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class SaveData
{
    public Vector3 playerPos;
}

public class SaveAndLoad : MonoBehaviour
{


    [SerializeField] private GameObject thisgameobject;

    private SaveData saveData = new SaveData();

    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private PlayerController thePlayer;



    // Start is called before the first frame update
    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";


        if (!Directory.Exists(SAVE_DATA_DIRECTORY)) {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
        }


    }

    public void SaveData() {

        
        thePlayer = FindObjectOfType<PlayerController>();

        saveData.playerPos = thePlayer.transform.position;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY+SAVE_FILENAME,json);

        Debug.Log("저장완료");
        
    }
    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {

            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            thePlayer = FindObjectOfType<PlayerController>();

            thePlayer.transform.position = saveData.playerPos;


            Debug.Log("로드완료");
        }
        else {
            Debug.Log("세이브 파일이 없습니다.");


        }


    }


}
