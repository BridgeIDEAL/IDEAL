using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using System;

public class CountAttempts : MonoBehaviour
{
    private static CountAttempts instance = null;
    public static CountAttempts Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    private int attemptCount = 1;
    private string playerDataPath;

    private void Awake(){
        if(Instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }

        // 플레이어 데이터가 이미 저장되어 있다면 불러오고
        // 없다면 따로 attempt를 1로하고 따로 저장하지는 않음
        playerDataPath = Path.Combine(Application.persistentDataPath, "PlayerGuideLogData.json");
        if (File.Exists(playerDataPath))
        {
            LoadAttempt();
        }
    }

    public void AddAttemptCount(){
        attemptCount++;
        // Attempt 저장은 GuideLogManager에서 PlayerData를 저장할 때 같이
    }

    public int GetAttemptCount(){
        return attemptCount;
    }

    private void LoadAttempt()
    {
        string loadJson = File.ReadAllText(playerDataPath);
        PlayerSaveData playerSaveData = new PlayerSaveData();
        playerSaveData = JsonUtility.FromJson<PlayerSaveData>(loadJson);

        if (playerSaveData != null)
        {
            attemptCount = playerSaveData.nowAttempt;
        }
    }

    // Attempt 저장은 GuideLogManager에서 PlayerData를 저장할 때 같이
}
