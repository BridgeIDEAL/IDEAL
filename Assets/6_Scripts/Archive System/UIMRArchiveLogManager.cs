using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMRArchiveLogManager : MonoBehaviour
{
    [SerializeField] private bool isMonsterView = true;
    [SerializeField] private Image backGround;
    [SerializeField] private Button closeBookBtn;
    [SerializeField] private Sprite monsterBGSprite;
    [SerializeField] private Sprite roomBGSprite;
    [SerializeField] private int viewIndex = 0;

    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private TextMeshProUGUI descTMP;
    [SerializeField] private Image archiveImage;

    [SerializeField] private GameObject uIMRArchiveLogPrefab;
    [SerializeField] private RectTransform archiveLogArea;
    private List<UIMRArchiveLog> mrArchiveLogList = new List<UIMRArchiveLog>();

    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite[] monsterSprites;
    [SerializeField] private Sprite[] roomSprites;

    
    void Start(){

        UpdateScrollLogs();
        // Monster index 0에 대한 정보 보여주기
        ShowArchiveLog(0);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            closeBookBtn.onClick.Invoke();
        }
    }

    private void DestroyMRArchiveLogList(){
        foreach(UIMRArchiveLog mrlog in mrArchiveLogList){
            mrlog.DestoryThisObject();
        }
        mrArchiveLogList = new List<UIMRArchiveLog>();
    }

    private void UpdateScrollLogs(){
        DestroyMRArchiveLogList();

        if(isMonsterView){
            List<MonsterArchiveLogs> monsterArchiveList = MonsterArchiveLogManager.Instance.GetMonsterArchiveList();
            foreach(MonsterArchiveLogs logs in monsterArchiveList){
                GameObject logGameObject = Instantiate(uIMRArchiveLogPrefab);
                RectTransform rt = logGameObject.GetComponent<RectTransform>();
                rt.SetParent(archiveLogArea);
                UIMRArchiveLog uIMRArchiveLog = logGameObject.GetComponent<UIMRArchiveLog>();
                mrArchiveLogList.Add(uIMRArchiveLog);
                uIMRArchiveLog.SetManager(this, logs.monsterID);
                uIMRArchiveLog.SetNameString(logs.monsterName);
                uIMRArchiveLog.SetFontNormal();
                uIMRArchiveLog.ShowRedDot(logs.hasNewData);
            }
        }
        else{
            List<RoomArchiveLogs> roomArchiveList = RoomArchiveLogManager.Instance.GetRoomArchiveList();
            foreach(RoomArchiveLogs logs in roomArchiveList){
                GameObject logGameObject = Instantiate(uIMRArchiveLogPrefab);
                RectTransform rt = logGameObject.GetComponent<RectTransform>();
                rt.SetParent(archiveLogArea);
                UIMRArchiveLog uIMRArchiveLog = logGameObject.GetComponent<UIMRArchiveLog>();
                mrArchiveLogList.Add(uIMRArchiveLog);
                uIMRArchiveLog.SetManager(this, logs.roomID);
                uIMRArchiveLog.SetNameString(logs.roomName);
                uIMRArchiveLog.SetFontNormal();
                uIMRArchiveLog.ShowRedDot(logs.hasNewData);
            }
        }
    }

    public void ShowArchiveLog(int id){
        
        foreach(UIMRArchiveLog uimrLog in mrArchiveLogList){
            if(uimrLog.logNum == id){
                uimrLog.SetFontBig();
                uimrLog.ShowRedDot(false);
            }
            else{
                uimrLog.SetFontNormal();
            }
        }
        
        if(isMonsterView){
            MonsterArchiveLogs monsterArchiveLogs = MonsterArchiveLogManager.Instance.GetMonsterArchiveLogs(id);
            nameTMP.text = monsterArchiveLogs.monsterName;
            if(monsterArchiveLogs.isImageActive){
                archiveImage.sprite = monsterSprites[id];
            }
            else{
                archiveImage.sprite = lockedSprite;
            }

            descTMP.text = "";

            string str = "";
            foreach(MonsterArchiveLog monsterLog in monsterArchiveLogs.monsterArchiveLogs){
                if(monsterLog.GetAttempt() != 0){
                    str = MonsterArchiveLogManager.Instance.GetMonsterArchiveText(monsterLog.GetID());
                    if(monsterLog.GetAttempt() != -1){
                        str = str.Replace("$attempts", monsterLog.GetAttempt().ToString());
                        str = $"<color=#B22222>{str}</color>"; // 색상 감싸기
                    }
                    descTMP.text += str;
                    descTMP.text += "\n";
                }
            }

            // 해당 몬스터에 대한 정보를 보여줬기에 빨간점 회수
            MonsterArchiveLogManager.Instance.WatchMonsterLog(id);
        }
        else{
            RoomArchiveLogs roomArchiveLogs = RoomArchiveLogManager.Instance.GetRoomArchiveLogs(id);
            nameTMP.text = roomArchiveLogs.roomName;
            if(roomArchiveLogs.isImageActive){
                archiveImage.sprite = roomSprites[id];
            }
            else{
                archiveImage.sprite = lockedSprite;
            }

            descTMP.text = "";

            string str = "";
            foreach(RoomArchiveLog roomLog in roomArchiveLogs.roomArchiveLogs){
                if(roomLog.GetAttempt() != 0){
                    str = RoomArchiveLogManager.Instance.GetRoomArchiveText(roomLog.GetID());
                    if(roomLog.GetAttempt() != -1){
                        str = str.Replace("$attempts", roomLog.GetAttempt().ToString());
                        str = $"<color=#B22222>{str}</color>"; // 색상 감싸기
                    }
                    descTMP.text += str;
                    descTMP.text += "\n";
                }
            }

            // 해당 방에 대한 정볼르 보여줬기에 빨간점 회수
            RoomArchiveLogManager.Instance.WatchRoomLog(id);
        }
    }

    public void ChangePage(bool isMonster){
        isMonsterView = isMonster;
        if(isMonsterView){
            backGround.sprite = monsterBGSprite;
        }
        else{
            backGround.sprite = roomBGSprite;
        }
        UpdateScrollLogs();
        ShowArchiveLog(0);
    }
}
