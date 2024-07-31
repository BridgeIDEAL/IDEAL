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
        {101, "게시판을 확인하세요."},
        {102, "입구로 들어가 수위와 대화하세요."},
        {103, "철문 열쇠를 찾으세요."},
        {104, "다음 동으로 넘어가세요."},
        {105, "책자 조각과 3-8열쇠를 찾으세요."},
        {106, "3-8에서 교무실 열쇠를 얻으세요. "},
        {107, "교무실에서 남은 교실들의 열쇠를 얻으세요."},
        {108, "남은 3학년 교실들을 탐방하세요. "},
        {109, "2층에서 사용할 교실 열쇠 조각을 찾으세요. (1)"},
        {110, "2층에서 사용할 교실 열쇠 조각을 찾으세요. (2)"},
        {111, "2층에서 사용할 교실 열쇠 조각을 찾으세요. (3)"},
        
        {201, "학생회장과 대화하세요."},
        {202, "전체 책자를 확인하세요."},
        {203, "2-1에서 무서운 학생과 대화하세요."},
        {204, "빵을 구매하세요."},
        {205, "2학년 교무실에서 교실 열쇠를 훔치세요."},
        {206, "남은 2학년 교실들을 탐방하세요."},
        {207, "학생회실 열쇠 조각을 찾으세요. (1)"},
        {208, "학생회실 열쇠 조각을 찾으세요. (2)"},
        {209, "학생회실 열쇠 조각을 찾으세요. (3)"},
        {210, "학생회실에서 방송실 장비 비밀번호를 획득하세요."},
        {211, "보건실에서 약을 구하세요."},

        {301, "과학실에서 의문의 학생과 대화하세요."},
        {302, "의문의 학생에게 약을 전달하고 열쇠를 얻으세요."},
        {303, "1학년 교무실에서 교실 열쇠를 훔치세요."},
        {304, "남은 1학년 교실들을 탐방하세요."},
        {305, "전산실 열쇠 조각을 찾아내세요. (1)"},
        {306, "전산실 열쇠 조각을 찾아내세요. (2)"},
        {307, "전산실 열쇠 조각을 찾아내세요. (3)"},
        {308, "전산실에서 방송실 열쇠를 획득하세요."},

        {401, "방송실에서 하교 종을 울리세요."},

        {901, "정문으로 돌아가 학교를 탈출하세요."}
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
        {202, -1},
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

    public Dictionary<int, int> itemState = new Dictionary<int, int>();

    public Dictionary<int, int> doorState = new Dictionary<int, int>();     // 상호작용 없을 시 코드 없음 문 열림 1 문 닫힘 2

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

    public void UpdateCheckList(int checkListNum, int state){
        if(checkListDic.ContainsKey(checkListNum)){
            checkListDic[checkListNum] = state;
        }
        else{
            Debug.LogError("checkList Dictionary Not Contains Key!");
        }
        UpdateCheckListObject();
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uICheckListManager.UpdateCheckListUI();
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
            doorState = new Dictionary<int, int>();
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

    public void SetDoorLog(int doorCode, int state){
        if(doorState.ContainsKey(doorCode)){
            // 이미 문 상호작용을 한 경우 state 값을 갱신
            doorState[doorCode] = state;
        }
        else{
            // 상호작용 하지 않은 문이라면 등록
            doorState.Add(doorCode, state);
        }
    }

    public int GetDoorLog(int doorCode){
        if(doorState.ContainsKey(doorCode)){
            return doorState[doorCode];
        }
        else{
            return -1;
        }
    }
}
