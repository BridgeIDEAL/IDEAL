using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class MonsterArchiveLogs{
    public int monsterID;
    public string monsterName;
    public MonsterArchiveLog[] monsterArchiveLogs;
    public bool isImageActive;

    public MonsterArchiveLogs(int _monsterID, string _monsterName, MonsterArchiveLog[] _monsterArchiveLogs, bool _isImageActive){
        this.monsterID = _monsterID;
        this.monsterName = _monsterName;
        this.monsterArchiveLogs = _monsterArchiveLogs;
        this.isImageActive = _isImageActive;
    }
}

[System.Serializable]
public class MonsterArchiveData{
    public List<MonsterArchiveLog> monsterArchiveChangeList = new List<MonsterArchiveLog>();
    public List<int> monsterImageActiveList = new List<int>();
}

public class MonsterArchiveLogManager : MonoBehaviour
{
    private static MonsterArchiveLogManager instance = null;
    public static MonsterArchiveLogManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    private string monsterArchiveDataPath;
    private MonsterArchiveData monsterArchiveData;
    private List<MonsterArchiveLogs> monsterArchiveList = new List<MonsterArchiveLogs>();

    private MonsterArchiveLogData monsterLogData = new MonsterArchiveLogData();


    private void Awake(){
        if(Instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }

        monsterArchiveDataPath = Path.Combine(Application.persistentDataPath, "MonsterArchiveLogData.json");

        monsterLogData.GenerateDictionary();
    }

    private void Start(){
        if (File.Exists(monsterArchiveDataPath)){
            LoadArchiveData();
            GenerateMonsterList();
            ApplySavedData();
        }
        else{
            monsterArchiveData = new MonsterArchiveData();
            GenerateMonsterList();
            SaveArchiveData();
        }
    }

    public void GenerateMonsterList(){  // attempts -1은 항상 보이는 것 0은 안보이는 것 1이상은 보이는데 attempts를 기록하는 것
        monsterArchiveList = new List<MonsterArchiveLogs>();
        monsterArchiveList.Add(new MonsterArchiveLogs(00, "이사장", new MonsterArchiveLog[] {
        new MonsterArchiveLog(0001, -1),
        },
        false
        ));

        monsterArchiveList.Add(new MonsterArchiveLogs(01, "수위", new MonsterArchiveLog[] {
        new MonsterArchiveLog(0101, -1),
        },
        false
        ));

        monsterArchiveList.Add(new MonsterArchiveLogs(02, "보건교사", new MonsterArchiveLog[] {
        new MonsterArchiveLog(0201, -1),
        new MonsterArchiveLog(0202, 0),
        },
        false
        ));

        monsterArchiveList.Add(new MonsterArchiveLogs(03, "매점 아주머니", new MonsterArchiveLog[] {
        new MonsterArchiveLog(0301, -1),
        new MonsterArchiveLog(0302, 0),
        },
        false
        ));

        monsterArchiveList.Add(new MonsterArchiveLogs(04, "멍이 든 학생", new MonsterArchiveLog[] {
        new MonsterArchiveLog(0401, -1),
        new MonsterArchiveLog(0402, 0),
        },
        false
        ));

        monsterArchiveList.Add(new MonsterArchiveLogs(05, "무서운 학생", new MonsterArchiveLog[] {
        new MonsterArchiveLog(0501, -1),
        },
        false
        ));

        monsterArchiveList.Add(new MonsterArchiveLogs(06, "학생주임", new MonsterArchiveLog[] {
        new MonsterArchiveLog(0601, -1),
        new MonsterArchiveLog(0602, 0),
        new MonsterArchiveLog(0603, 0),
        },
        false
        ));

        monsterArchiveList.Add(new MonsterArchiveLogs(07, "신입 선생님", new MonsterArchiveLog[] {
        new MonsterArchiveLog(0701, -1),
        new MonsterArchiveLog(0702, 0),
        },
        false
        ));

        monsterArchiveList.Add(new MonsterArchiveLogs(08, "교장 선생님", new MonsterArchiveLog[] {
        new MonsterArchiveLog(0801, -1),
        new MonsterArchiveLog(0802, 0),
        },
        false
        ));

        monsterArchiveList.Add(new MonsterArchiveLogs(09, "학생회장", new MonsterArchiveLog[] {
        new MonsterArchiveLog(0901, -1),
        new MonsterArchiveLog(0902, 0),
        },
        false
        ));

        monsterArchiveList.Add(new MonsterArchiveLogs(10, "컴퓨터실 학생", new MonsterArchiveLog[] {
        new MonsterArchiveLog(1001, -1),
        },
        false
        ));
    }

    private void LoadArchiveData(){
        string loadJson = File.ReadAllText(monsterArchiveDataPath);
        monsterArchiveData = JsonUtility.FromJson<MonsterArchiveData>(loadJson);
    }

    public void SaveArchiveData(){
        string json = JsonUtility.ToJson(monsterArchiveData, true);
        File.WriteAllText(monsterArchiveDataPath, json);
    }

    private void ApplySavedData(){
        foreach(MonsterArchiveLog savedLog in monsterArchiveData.monsterArchiveChangeList){
            int monsterID_ = savedLog.GetID() / 100;
            foreach(MonsterArchiveLogs monsterList in monsterArchiveList){
                if(monsterList.monsterID == monsterID_){
                    foreach(MonsterArchiveLog log in monsterList.monsterArchiveLogs){
                        if(log.GetAttempt() == 0){
                            log.SetAttempt(savedLog.GetAttempt());
                        }
                    }
                }
            }
        }

        foreach(int imageNum in monsterArchiveData.monsterImageActiveList){
            foreach(MonsterArchiveLogs monsterList in monsterArchiveList){
                if(monsterList.monsterID == imageNum){
                    monsterList.isImageActive = true;
                }
            }
        }
    }

    public void UpdateArchiveLogData(int archiveID, int _attempt){
        // Update monsterArchiveList
        foreach(MonsterArchiveLogs monsterList in monsterArchiveList){
            if(monsterList.monsterID == archiveID / 100){
                foreach(MonsterArchiveLog log in monsterList.monsterArchiveLogs){
                    if(log.GetAttempt() == 0){
                        log.SetAttempt(_attempt);
                    }
                }
            }
        }

        // Update monsterArchiveData
        bool noDataInSavedData = true;
        foreach(MonsterArchiveLog savedLog in monsterArchiveData.monsterArchiveChangeList){
            if(savedLog.GetID() == archiveID){
                noDataInSavedData = false;
            }
        }

        if(noDataInSavedData){
            monsterArchiveData.monsterArchiveChangeList.Add(new MonsterArchiveLog(archiveID, _attempt));
        }
    }

    public void UpdateArchiveImageData(int _monsterID){
        // Update monsterArchiveList
        foreach(MonsterArchiveLogs monsterList in monsterArchiveList){
            if(monsterList.monsterID == _monsterID){
                monsterList.isImageActive = true;
            }
        }

        // Update monsterArchiveData
        bool noDataInSavedData = true;
        foreach(int imageNum in monsterArchiveData.monsterImageActiveList){
            if(imageNum == _monsterID){
                noDataInSavedData = false;
            }
        }

        if(noDataInSavedData){
            monsterArchiveData.monsterImageActiveList.Add(_monsterID);
        }
    }

    public MonsterArchiveLogs GetMonsterArchiveLogs(int _monsterID){
        foreach (MonsterArchiveLogs logs in monsterArchiveList){
            if(logs.monsterID == _monsterID){
                return logs;
            }
        }
        return null;
    }

    public string GetMonsterArchiveText(int logID){
        if(monsterLogData.monsterArchiveDictionary.ContainsKey(logID)){
            return monsterLogData.monsterArchiveDictionary[logID];
        }
        else{
            return "데이터가 존재하지 않습니다.";
        }
    }

    public List<MonsterArchiveLogs> GetMonsterArchiveList(){
        return monsterArchiveList;
    }
}
