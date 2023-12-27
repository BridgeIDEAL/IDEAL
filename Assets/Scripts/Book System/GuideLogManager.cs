using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuideLogManager : MonoBehaviour
{
    private static GuideLogManager instance = null;
    public static GuideLogManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    [SerializeField] private GuideLogData guideLogData;
    [SerializeField] private UIGuideLogManager uIGuideLogManager;

    public List<GuideLogRecord> guideLogRecordList = new List<GuideLogRecord>();

    private void Awake(){
        if(Instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }

        guideLogData.Init();
        guideLogRecordList = new List<GuideLogRecord>();


        // TO DO 아래 테스트 코드 이후에 지우기
        GenerateGuideLogRecordList();
        GenerateBasicGuideLog();
        // GenerateTestCase();
    }

    public void AddGuideLogRecord(int logID, int attempt){
        GuideLogRecord guideLogRecord = new GuideLogRecord(logID, attempt);
        guideLogRecordList.Add(guideLogRecord);
        SortRecordList();
    }

    public void UpdateGuideLogRecord(int logID, int attempt){
        for(int i = 0; i < guideLogRecordList.Count; i++){
            if(logID == guideLogRecordList[i].GetGuideLogID()){
                if(guideLogRecordList[i].GetAttempt() <= -2){
                    guideLogRecordList[i].SetAttempt(attempt);
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
        foreach(var keyValuePair in guideLogData.guideLogDictionary){
            AddGuideLogRecord(keyValuePair.Key, -2);
        }
    }

    private void GenerateBasicGuideLog(){
        UpdateGuideLogRecord(010000, -1);
        UpdateGuideLogRecord(010100, 1);
        UpdateGuideLogRecord(020000, -1);
    }

    private void GenerateTestCase(){
        UpdateGuideLogRecord(010101, 2);
        UpdateGuideLogRecord(010102, 3);
        UpdateGuideLogRecord(010103, 4);
        UpdateGuideLogRecord(010103, -1);
        UpdateGuideLogRecord(010103, 0);
        UpdateGuideLogRecord(010103, 1);

        UpdateGuideLogRecord(010103, 2);
        UpdateGuideLogRecord(010103, 3);
        UpdateGuideLogRecord(010103, 4);
        UpdateGuideLogRecord(010103, 5);
        UpdateGuideLogRecord(010103, 6);
        UpdateGuideLogRecord(010103, 7);
        UpdateGuideLogRecord(010103, 8);
        UpdateGuideLogRecord(010103, 9);
        UpdateGuideLogRecord(010103, 10);

        UpdateGuideLogRecord(010103, 11);
        UpdateGuideLogRecord(010103, 12);
        UpdateGuideLogRecord(010103, 13);
        UpdateGuideLogRecord(010103, 14);
        UpdateGuideLogRecord(010103, 15);
        UpdateGuideLogRecord(010103, 16);
        UpdateGuideLogRecord(010103, 17);
        UpdateGuideLogRecord(010103, 18);
        UpdateGuideLogRecord(010103, 19);
        UpdateGuideLogRecord(010103, 20);
    }
}
