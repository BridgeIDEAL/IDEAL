using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class RoomArchiveLogs{
    public int roomID;
    public string roomName;
    public RoomArchiveLog[] roomArchiveLogs;
    public bool isImageActive;

    public RoomArchiveLogs(int _roomID, string _roomName, RoomArchiveLog[] _roomArchiveLogs, bool _isImageActive){
        this.roomID = _roomID;
        this.roomName = _roomName;
        this.roomArchiveLogs = _roomArchiveLogs;
        this.isImageActive = _isImageActive;
    }
}

[System.Serializable]
public class RoomArchiveData{
    public List<RoomArchiveLog> roomArchiveChangeList = new List<RoomArchiveLog>();
    public List<int> roomImageActiveList = new List<int>();
}

public class RoomArchiveLogManager : MonoBehaviour
{
    private static RoomArchiveLogManager instance = null;
    public static RoomArchiveLogManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    private string roomArchiveDataPath;
    private RoomArchiveData roomArchiveData;
    private List<RoomArchiveLogs> roomArchiveList = new List<RoomArchiveLogs>();


    private void Awake(){
        if(Instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }

        roomArchiveDataPath = Path.Combine(Application.persistentDataPath, "RoomArchiveLogData.json");
    }

    private void Start(){
        if (File.Exists(roomArchiveDataPath)){
            LoadArchiveData();
            GenerateRoomList();
            ApplySavedData();
        }
        else{
            roomArchiveData = new RoomArchiveData();
            GenerateRoomList();
            SaveArchiveData();
        }
    }

    public void GenerateRoomList(){  // attempts -1은 항상 보이는 것 0은 안보이는 것 1이상은 보이는데 attempts를 기록하는 것
        roomArchiveList = new List<RoomArchiveLogs>();
        roomArchiveList.Add(new RoomArchiveLogs(00, "수위실", new RoomArchiveLog[] {
        new RoomArchiveLog(0001, -1),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(01, "교장실", new RoomArchiveLog[] {
        new RoomArchiveLog(0101, -1),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(02, "이사장실", new RoomArchiveLog[] {
        new RoomArchiveLog(0201, -1),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(03, "행정실", new RoomArchiveLog[] {
        new RoomArchiveLog(0301, -1),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(04, "자습실", new RoomArchiveLog[] {
        new RoomArchiveLog(0401, -1),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(05, "매점", new RoomArchiveLog[] {
        new RoomArchiveLog(0501, -1),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(06, "학생회실", new RoomArchiveLog[] {
        new RoomArchiveLog(0601, -1),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(07, "보건실", new RoomArchiveLog[] {
        new RoomArchiveLog(0701, -1),
        new RoomArchiveLog(0702, 0),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(08, "과학실", new RoomArchiveLog[] {
        new RoomArchiveLog(0801, -1),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(09, "전산실", new RoomArchiveLog[] {
        new RoomArchiveLog(0901, -1),
        new RoomArchiveLog(0902, 0),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(10, "진로진학부",  new RoomArchiveLog[] {
        new RoomArchiveLog(1001, -1),
        new RoomArchiveLog(1002, 0),
        new RoomArchiveLog(1003, 0),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(11, "방송실", new RoomArchiveLog[] {
        new RoomArchiveLog(1101, -1),
        new RoomArchiveLog(1102, 0),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(12, "상담실/동아리실", new RoomArchiveLog[] {
        new RoomArchiveLog(1201, -1),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(13, "교무실", new RoomArchiveLog[] {
        new RoomArchiveLog(1301, -1),
        new RoomArchiveLog(1302, 0),
        new RoomArchiveLog(1303, 0),
        new RoomArchiveLog(1304, 0),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(14, "옥상", new RoomArchiveLog[] {
        new RoomArchiveLog(1401, -1),
        new RoomArchiveLog(1402, 0),
        new RoomArchiveLog(1403, 0),
        },
        true
        ));

        roomArchiveList.Add(new RoomArchiveLogs(15, "음악실", new RoomArchiveLog[] {
        new RoomArchiveLog(1501, -1),
        },
        true
        ));

    }

    private void LoadArchiveData(){
        string loadJson = File.ReadAllText(roomArchiveDataPath);
        roomArchiveData = JsonUtility.FromJson<RoomArchiveData>(loadJson);
    }

    public void SaveArchiveData(){
        string json = JsonUtility.ToJson(roomArchiveData, true);
        File.WriteAllText(roomArchiveDataPath, json);
    }

    private void ApplySavedData(){
        foreach(RoomArchiveLog savedLog in roomArchiveData.roomArchiveChangeList){
            int roomID_ = savedLog.GetID() / 100;
            foreach(RoomArchiveLogs roomList in roomArchiveList){
                if(roomList.roomID == roomID_){
                    foreach(RoomArchiveLog log in roomList.roomArchiveLogs){
                        if(log.GetAttempt() == 0){
                            log.SetAttempt(savedLog.GetAttempt());
                        }
                    }
                }
            }
        }

        foreach(int imageNum in roomArchiveData.roomImageActiveList){
            foreach(RoomArchiveLogs roomList in roomArchiveList){
                if(roomList.roomID == imageNum){
                    roomList.isImageActive = true;
                }
            }
        }
    }

    public void UpdateArchiveLogData(int archiveID, int _attempt){
        // Update roomArchiveList
        foreach(RoomArchiveLogs roomList in roomArchiveList){
            if(roomList.roomID == archiveID / 100){
                foreach(RoomArchiveLog log in roomList.roomArchiveLogs){
                    if(log.GetAttempt() == 0){
                        log.SetAttempt(_attempt);
                    }
                }
            }
        }

        // Update roomArchiveData
        bool noDataInSavedData = true;
        foreach(RoomArchiveLog savedLog in roomArchiveData.roomArchiveChangeList){
            if(savedLog.GetID() == archiveID){
                noDataInSavedData = false;
            }
        }

        if(noDataInSavedData){
            roomArchiveData.roomArchiveChangeList.Add(new RoomArchiveLog(archiveID, _attempt));
        }
    }

    public void UpdateArchiveImageData(int _roomID){
        // Update roomArchiveList
        foreach(RoomArchiveLogs roomList in roomArchiveList){
            if(roomList.roomID == _roomID){
                roomList.isImageActive = true;
            }
        }

        // Update roomArchiveData
        bool noDataInSavedData = true;
        foreach(int imageNum in roomArchiveData.roomImageActiveList){
            if(imageNum == _roomID){
                noDataInSavedData = false;
            }
        }

        if(noDataInSavedData){
            roomArchiveData.roomImageActiveList.Add(_roomID);
        }
    }
}
