using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProgressManager : MonoBehaviour
{
    private static ProgressManager instance = null;
    public static ProgressManager Instance{
        get{
            if(instance == null){ return null;}
            return instance;
        }
    }

    public SortedDictionary<int, string> checkListStr = new SortedDictionary<int, string>{
        // {101, "게시판을 확인하세요."},
        {101, "Talk with the Guard."},
        {102, "Obtain the school map on the ground."},
        {103, "Obtain the steel door key from the guard room."},
        {104, "Go to the Classroom Wing."},
        {105, "Find the 3-6 key."},
        {106, "Find the 3rd grade Student Center key in 3-6."},
        {107, "Find 3 class keys in the 3rd grade Student Center."},
        {108, "Search the 3rd grade classes carefully."},
        {109, "Find the key piece for 2-1 (1)."},
        {110, "Find the key piece for 2-1 (2)."},
        {111, "Find the key piece for 2-1 (3)."},
        
        {201, "Talk with the Student President on the 2nd floor."},
        // {202, "전체 책자를 확인하세요."},
        {203, "Talk with the Bullies in 2-1."},
        {204, "Buy the right bread."},
        {205, "Steal 3 class keys in the 2nd grade Student Center."},
        {206, "Search the 2nd grade classes carefully."},
        {207, "Find the key piece for Student Council Room (1)."},
        {208, "Find the key piece for Student Council Room (2)."},
        {209, "Find the key piece for Student Council Room (3)."},
        {210, "Find the Broadcast Memo in the Student Council Room."},
        {301, "Find the medicine in the Infirmary."},

        {211, "Talk with the Bruised Boy in the Science Room."},
        {302, "Obtain a key from the Bruised Boy."},
        {303, "Steal 3 class keys in the 1st grade Student Center."},
        {304, "Search the 1st grade classes carefully."},
        {305, "Find the key piece for Server Room (1)."},
        {306, "Find the key piece for Server Room (2)."},
        {307, "Find the key piece for Server Room (3)."},
        {308, "Obtain the Broadcast Room Key from the Server Room."},

        {401, "Ring the School bell from the Broadcast Room."},

        {901, "Escape the school through the main gate."}
    };
    public SortedDictionary<int, int> checkListDic =  new SortedDictionary<int, int>{
        {101, -1},
        {102, -1},
        {103, -1},
        {104, -1},
        {105, -1},
        {106, -1},
        {107, -1},
        {108, -1},
        {109, -1},
        {110, -1},
        {111, -1},
        
        {201, -1},
        // {202, -1},
        {203, -1},
        {204, -1},
        {205, -1},
        {206, -1},
        {207, -1},
        {208, -1},
        {209, -1},
        {210, -1},
        {211, -1},

        {301, -1},
        {302, -1},
        {303, -1},
        {304, -1},
        {305, -1},
        {306, -1},
        {307, -1},
        {308, -1},

        {401, -1},

        {901, -1}
    };

    private Dictionary<int, int> monsterArchiveImageDic = new Dictionary<int, int>{
        {101, 1},
        {301, 2},
        {211, 4},
        {203, 5},
        {201, 9},
    };

    private Dictionary<int, int> monsterArchiveLogDic = new Dictionary<int, int>{
        {205, 0602},
        {303, 0603},
    };

    public Dictionary<int, int> itemState = new Dictionary<int, int>();

    public Dictionary<string, int> doorState = new Dictionary<string, int>();     // 상호작용 없을 시 코드 없음 문 열림 1

    public bool watchedMap = false;
    public int watchMapNum = 0;
    private int mapCheckListStateNum = 0;

    public bool lastRunning = false;
    public float lastRunningTime = 0.0f;

    public bool needShowChecklistIcon = false;

    public bool isTurnOnLight = false;

    private UICheckListManager uICheckListManager;
    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    void Update(){
        if(lastRunning){
            lastRunningTime += Time.deltaTime;
        }
    }

    public void UpdateCheckList(int checkListNum, int state){
        if(checkListDic.ContainsKey(checkListNum)){
            checkListDic[checkListNum] = state;
        }
        else{
            Debug.LogError("checkList Dictionary Not Contains Key!");
        }
        UpdateCheckListObject();
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uICheckListManager.UpdateCheckListUI();
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIMap.UpdateMapFloor();

        if(state != -1){
            needShowChecklistIcon = true;
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIIngame.TurnOnCheckListIcon();
        }

        if(state != -1){
            UpdateMonsterArchiveImage(checkListNum);
            UpdateMonsterArchiveLog(checkListNum);
            // UpdateRoomArchiveImage(checkListNum);
            // UpdateRoomArchieLog(checkListNum);
        }
        

    }

    private void UpdateMonsterArchiveImage(int checkListNum){
        if(monsterArchiveImageDic.ContainsKey(checkListNum)){
            MonsterArchiveLogManager.Instance.UpdateArchiveImageData(monsterArchiveImageDic[checkListNum]);
        }
        else{
            // 있으면 작동하는 거고 없어도 비정상은 아님
            // Debug.Log("Invalid checklistnum");
        }
    }

    private void UpdateMonsterArchiveLog(int checkListNum){
        if(monsterArchiveLogDic.ContainsKey(checkListNum)){
            MonsterArchiveLogManager.Instance.UpdateArchiveLogData(monsterArchiveLogDic[checkListNum], CountAttempts.Instance.GetAttemptCount());
        }
        else{
            // 있으면 작동하는 거고 없어도 비정상은 아님
            // Debug.Log("Invalid checklistnum");
        }
    }

    public void TurnOffCheckListIcon(){
        needShowChecklistIcon = false;
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIIngame.TurnOffCheckListIcon();
    }

    private void UpdateCheckListObject(){
        // 101
        
    }

    // 추가: checkListDic의 키 목록 반환 메서드
    public List<int> GetCheckListDicKeys()
    {
        return new List<int>(checkListDic.Keys);
    }

    public void EnterAnotherSceneInit(bool isLobby){
        if(isLobby){
            List<int> keys = new List<int>(checkListDic.Keys); // 키 목록을 리스트로 복사

            for (int i = 0; i < keys.Count; i++) {
                int key = keys[i];
                checkListDic[key] = -1; // 수정 작업 수행
            }

            itemState = new Dictionary<int, int>();
            doorState = new Dictionary<string, int>();
            watchMapNum = 0;
            watchedMap = false;
            mapCheckListStateNum = 0;
            lastRunning = false;
            lastRunningTime = 0.0f;
        }
        else{
            uICheckListManager = IdealSceneManager.Instance.CurrentGameManager.scriptHub.uICheckListManager;
            uICheckListManager.Init();
        }
    }


    public void SetItemLog(int itemCode, int cnt){
        if(itemState.ContainsKey(itemCode)){
            // 이미 해당 아이템이 등록 된 경우 cnt 값을 갱신
            itemState[itemCode] = itemState[itemCode] + cnt;
        }
        else{
            // 해당 아이템이 없을 경우 새로 등록
            itemState.Add(itemCode, cnt);
        }
    }
    
    public bool GetItemLogExist(int itemCode){
        return itemState.ContainsKey(itemCode);
    }

    public int GetItemLog(int itemCode){
        if(itemState.ContainsKey(itemCode)){
            return itemState[itemCode];
        }
        else{
            return -1;
        }
    }

    public void SetDoorLog(string doorName, int state){
        if(doorState.ContainsKey(doorName)){
            // 이미 문 상호작용을 한 경우 state 값을 갱신
            doorState[doorName] = state;
        }
        else{
            // 상호작용 하지 않은 문이라면 등록
            doorState.Add(doorName, state);
        }
    }

    public int GetDoorLog(string doorName){
        if(doorState.ContainsKey(doorName)){
            return doorState[doorName];
        }
        else{
            return -1;
        }
    }

    public void AddMapCheckListStateNum() {
        mapCheckListStateNum++;
    }

    public int GetMapCheckListStateNum(){
        return mapCheckListStateNum;
    }
}
