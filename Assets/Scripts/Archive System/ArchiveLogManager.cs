using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[SerializeField]
public class PlayerArchiveData{
    public List<ArchiveLog> archiveLogRecordList = new List<ArchiveLog>();
}

public class ArchiveLogManager : MonoBehaviour
{
    private static ArchiveLogManager instance = null;
    public static ArchiveLogManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    private string archiveDataPath = "Assets/Resources/Data/PlayerArchiveLogData.json";

    public PlayerArchiveData playerArchiveData;

    // UI 선언 필요

    private string[] archiveTexts = {
        "$attempts번 실종자는 복도를 돌아다니던 중 교장선생님과 마주친 후 신호가 끊김. 이후 자습실 구석 자리에서 피에 젖은 책 더미 외에는 해당 실종자와 관련된 흔적을 찾을 수 없었음.",
        "$attempts번 실종자는 갑자기 ‘교무실에서 왜 사람이...  분명 아무도 없었는데’라는 말을 반복하던 중 신호가 끊김. \n아직 정확한 생사는 불명.",
        "$attempts번 실종자는 목이 뒤로 꺾인 여학생과 조우한 후로 신호가 끊김. \n이후 머리 없이 척추가 뒤로 꺾인 시체가 발견되었는데, 해당 실종자로 추정됨.",
        "$attempts번 실종자는 교무실에서 열쇠를 집은 뒤 나오려는 순간 신호가 끊김. \n간신히 복원한 기록에서는 여성으로 추정되는 비명소리가 들린 것 외에는 실종자의 흔적을 찾을 수 없었음.",
        "$attempts번 실종자는 컴퓨터실의 ■■을 무시하고 전산실에 들어가려다 갑자기 영화가 보고 싶다는 말과 함께 신호가 끊김. 이후 컴퓨터실의 켜진 컴퓨터가 한 대 늘어났다는 것을 확인한 것 외에는 실종자와 관련된 흔적을 찾을 수 없었음.",
        "$attempts번 실종자는 교실에 들어간 후 칠판에 뭔가 있다는 말을 반복하던 중 신호가 끊김. \n이후 복원할 수 있던 기록에서 해당 실종자의 이름이 칠판에 쓰여있던 것을 확인",
        "$attempts번 실종자는 갑자기 누군가 우르르 뛰어오는 소리가 들린다는 말과 함께 신호가 끊김. 이후 조사를 통해 빠르게 달려오는 트럭에 치인듯한 해당 실종자의 시체 일부를 발견.",
        "$attempts번 실종자는 눈에서 극심한 고통을 호소하던 중, 눈을 스스로 뽑아버린 것을 마지막으로 신호가 끊김.",
        "$attempts번 실종자는 입장 초기에 주어진 알약을 복용 후 누군가 실종자에게 말을 거는 듯한 소리와 함께 신호가 끊김.",
        "$attempts번 실종자는 갑자기 생긴 정문 출구로 자인고등학교를 정상적으로 탈출. 정문 출구와 자인고등학교 내부 환경에 대해서는 추가적인 조사 필요",
        "$attempts번 실종자는 종소리가 울리기 시작한 이후로 신호가 끊김. \n종소리와 학교 내부 환경의 변화에 대한 추가적인 조사 필요."
    };

    private string[] archiveStates = {
        "사망",
        "실종",
        "사망",
        "실종",
        "실종",
        "실종",
        "사망",
        "실종",
        "사망",
        "생존",
        "실종",
    };

    private void Awake(){
        if(Instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    private void Start(){
        if(File.Exists(archiveDataPath)){
            LoadArchiveData();
        }
        else{
            playerArchiveData = new PlayerArchiveData();
            SaveArchiveData();
        }

    }

    // 테스트 코드
    private int cnt = 1;
    private void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            ArchiveLog arc = new ArchiveLog(cnt, archiveStates[cnt-1], archiveTexts[cnt-1]);
            AddArchiveLog(arc);
            cnt++;
        }
    }



    private void LoadArchiveData(){
        string loadJson = File.ReadAllText(archiveDataPath);
        playerArchiveData = new PlayerArchiveData();
        playerArchiveData = JsonUtility.FromJson<PlayerArchiveData>(loadJson);
    }

    public void SaveArchiveData(){
        string json = JsonUtility.ToJson(playerArchiveData, true);
        File.WriteAllText(archiveDataPath, json);
    }

    public string GetArchiveText(int index){
        return archiveTexts[index];
    }

    public string GetArchiveState(int index){
        return archiveStates[index];
    }

    public void AddArchiveLog(ArchiveLog archiveLog){
        playerArchiveData.archiveLogRecordList.Add(archiveLog);
    }
}
