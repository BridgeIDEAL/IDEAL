using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class PlayerSaveData
{
    public int nowAttempt = 1;
    public List<GuideLogRecord> guideLogRecordList = new List<GuideLogRecord>();
}


public class GuideLogManager : MonoBehaviour
{
    private static GuideLogManager instance = null;
    public static GuideLogManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }
    private string playerDataPath = "Assets/Resources/Data/PlayerGuideLogData.json";

    [SerializeField] private GuideLogData guideLogData;
    [SerializeField] private UIGuideLogManager uIGuideLogManager;

    public List<GuideLogRecord> guideLogRecordList = new List<GuideLogRecord>();

    public bool guideLogUpdated = false;

    private void Awake(){
        if (Instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        guideLogData.Init();
    }

    void Start(){
        // 플레이어 데이터가 이미 저장되어 있다면 불러오고
        // 저장되어 있지 않다면 새로 생성
        // 플레이어 데이터는 Datas 폴더 속 PlayerGuideLogData.json으로 저장
        if (File.Exists(playerDataPath))
        {
            LoadGuideLogRecordList();
        }
        else
        {
            GenerateGuideLogRecordList();
            SavePlayerSaveData();
        }
        // 현재는 CountAttempt에서 Null이 뜨는 문제로 해당 부분을 Start로 미뤄둠
    }

    public void AddGuideLogRecord(int logID, int attempt){
        GuideLogRecord guideLogRecord = new GuideLogRecord(logID, attempt);
        guideLogRecordList.Add(guideLogRecord);
        SortRecordList();
    }

    public void UpdateGuideLogRecord(int logID, int attempt){
        for (int i = 0; i < guideLogRecordList.Count; i++){
            if(logID == guideLogRecordList[i].GetGuideLogID()){
                if(guideLogRecordList[i].GetAttempt() < 0){
                    guideLogRecordList[i].SetAttempt(attempt);
                    guideLogUpdated = true;
                }
            }
        }
    }


    public GuideLog GetGuideLog(int logID){
        return guideLogData.GetGuideLog(logID);
    }

    private void SortRecordList(){
        guideLogRecordList.Sort();
    }

    private void GenerateGuideLogRecordList(){
        guideLogRecordList = new List<GuideLogRecord>();
        foreach (var keyValuePair in guideLogData.guideLogDictionary)
        {
            AddGuideLogRecord(keyValuePair.Key, -2);
        }
        GenerateTransparentGuideLog();
        guideLogUpdated = false;
    }

    private void GenerateTransparentGuideLog(){
        UpdateGuideLogRecord(010100, -1);
        UpdateGuideLogRecord(020100, -1);
        UpdateGuideLogRecord(030101, -1);
        UpdateGuideLogRecord(030102, -1);
        UpdateGuideLogRecord(040100, -1);
        UpdateGuideLogRecord(060100, -1);
        UpdateGuideLogRecord(070101, -1);
        UpdateGuideLogRecord(090100, -1);
        UpdateGuideLogRecord(090200, -1);
        UpdateGuideLogRecord(100101, -1);
        UpdateGuideLogRecord(110100, -1);
        UpdateGuideLogRecord(110301, -1);
        UpdateGuideLogRecord(130101, -1);
        UpdateGuideLogRecord(130102, -1);
        UpdateGuideLogRecord(130104, -1);
        UpdateGuideLogRecord(130105, -1);
        UpdateGuideLogRecord(130106, -1);
        UpdateGuideLogRecord(140101, -1);
        UpdateGuideLogRecord(160200, -1);
        UpdateGuideLogRecord(200101, -1);

    }


    private void LoadGuideLogRecordList()
    {
        string loadJson = File.ReadAllText(playerDataPath);
        PlayerSaveData playerSaveData = new PlayerSaveData();
        playerSaveData = JsonUtility.FromJson<PlayerSaveData>(loadJson);

        if (playerSaveData != null)
        {
            for (int i = 0; i < playerSaveData.guideLogRecordList.Count; i++)
            {
                GuideLogRecord tempGuideLogRecord = playerSaveData.guideLogRecordList[i];
                AddGuideLogRecord(tempGuideLogRecord.GetGuideLogID(), tempGuideLogRecord.GetAttempt());
            }
        }
    }
    public void SavePlayerSaveData()
    {
        PlayerSaveData playerSaveData = new PlayerSaveData();
        for (int i = 0; i < guideLogRecordList.Count; i++)
        {
            playerSaveData.guideLogRecordList.Add(guideLogRecordList[i]);
        }
        playerSaveData.nowAttempt = CountAttempts.Instance.GetAttemptCount();
        string json = JsonUtility.ToJson(playerSaveData, true);
        File.WriteAllText(playerDataPath, json);
    }
}
