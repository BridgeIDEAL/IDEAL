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
        GenerateTransparentGuideLog();
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

    private void GenerateTestCase(){


    }
}
