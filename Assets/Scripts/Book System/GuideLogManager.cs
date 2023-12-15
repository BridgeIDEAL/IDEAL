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
        AddGuideLogRecord(010000, -1);
        AddGuideLogRecord(010100, 1);
        AddGuideLogRecord(010101, 2);
        AddGuideLogRecord(010102, 3);

        AddGuideLogRecord(020000, -1);
        AddGuideLogRecord(010103, 4);
        AddGuideLogRecord(010103, -1);
        AddGuideLogRecord(010103, 0);
        AddGuideLogRecord(010103, 1);
        AddGuideLogRecord(010103, 2);
        AddGuideLogRecord(010103, 3);
        AddGuideLogRecord(010103, 4);
        AddGuideLogRecord(010103, 5);
        AddGuideLogRecord(010103, 6);
        AddGuideLogRecord(010103, 7);
        AddGuideLogRecord(010103, 8);
        AddGuideLogRecord(010103, 9);
        AddGuideLogRecord(010103, 10);

        AddGuideLogRecord(010103, 11);
        AddGuideLogRecord(010103, 12);
        AddGuideLogRecord(010103, 13);
        AddGuideLogRecord(010103, 14);
        AddGuideLogRecord(010103, 15);
        AddGuideLogRecord(010103, 16);
        AddGuideLogRecord(010103, 17);
        AddGuideLogRecord(010103, 18);
        AddGuideLogRecord(010103, 19);
        AddGuideLogRecord(010103, 20);
    }

    public void AddGuideLogRecord(int logID, int attempt){
        GuideLogRecord guideLogRecord = new GuideLogRecord(logID, attempt);
        guideLogRecordList.Add(guideLogRecord);
        SortRecordList();
    }

    public GuideLog GetGuideLog(int logID){
        return guideLogData.GetGuideLog(logID);
    }

    private void SortRecordList(){
        guideLogRecordList.Sort();
    }
}
