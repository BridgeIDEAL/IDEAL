using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActivationLogManager : MonoBehaviour
{
    private static ActivationLogManager instance = null;

    public static ActivationLogManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    public ScriptHub scriptHub;
    [SerializeField] private ActivationLogData activationLogData;
    private UIActivationLogManager uIActivationLogManager;

    private List<ActivationLog> activationLogList = new List<ActivationLog>();

    private class ItemID_Name{
        public int itemID;
        public string itemName;
    }
    private List<ItemID_Name> itemID_NameList= new List<ItemID_Name>{
        new ItemID_Name{itemID = 10, itemName = "멍 치료제를"},
        new ItemID_Name{itemID = 11, itemName = "1학년 교무실 열쇠를"},
        new ItemID_Name{itemID = 12, itemName = "열쇠 조각을"},
        new ItemID_Name{itemID = 13, itemName = "명찰 조각을"},
        new ItemID_Name{itemID = 14, itemName = "학교 지도를"},
        new ItemID_Name{itemID = 15, itemName = "단팥빵을"},
        
        // ---------------------------------------------------------------------------------------- //
        new ItemID_Name{itemID = 1201, itemName = "알코올램프를"},
        new ItemID_Name{itemID = 1202, itemName = "분필을"},
        new ItemID_Name{itemID = 1101, itemName = "교무센터 열쇠를"},
        new ItemID_Name{itemID = 1102, itemName = "1학년 교무실 열쇠를"},
        new ItemID_Name{itemID = 1103, itemName = "옥상문 열쇠를"},
        new ItemID_Name{itemID = 1104, itemName = "확인증을"},
        new ItemID_Name{itemID = 1105, itemName = "약병을"},
        new ItemID_Name{itemID = 1106, itemName = "핸드크림을"},
        new ItemID_Name{itemID = 1107, itemName = "교과서를"},
        new ItemID_Name{itemID = 1110, itemName = "무전기를"},
        new ItemID_Name{itemID = 301, itemName = "3-1반 열쇠를"},
        new ItemID_Name{itemID = 302, itemName = "3-2반 열쇠를"},
        new ItemID_Name{itemID = 303, itemName = "3-3반 열쇠를"},
        new ItemID_Name{itemID = 304, itemName = "3-4반 열쇠를"},
        new ItemID_Name{itemID = 305, itemName = "3-5반 열쇠를"},
        new ItemID_Name{itemID = 306, itemName = "3-6반 열쇠를"},
        new ItemID_Name{itemID = 307, itemName = "3-7반 열쇠를"},
        new ItemID_Name{itemID = 308, itemName = "3-8반 열쇠를"},
        new ItemID_Name{itemID = 401, itemName = "철문 열쇠를"},
    };

    public void Init(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }

        activationLogData.Init();
        activationLogList = new List<ActivationLog>();
    }

    public void EnterAnotherSceneInit(bool isLobby){
        if(isLobby){
            ResetActivationLogList();
        }
        else{
            uIActivationLogManager = scriptHub.uIActivationLogManager;
            UpdateUIActivationLog();
        }
    }

    public void AddActivationLog(int logID){
        ActivationLog activationLog = activationLogData.GetActivationLog(logID);
        activationLogList.Add(activationLog);
        uIActivationLogManager.AddUIActivationLog(activationLog);
    }

    public void AddActivationLogWithItem(int itemID_, bool GetItem, string forWhat = ""){
        string itemName = itemID_NameList.FirstOrDefault(x => x.itemID == itemID_).itemName;
        
        string contextText, descText;
        if(GetItem){
            contextText = itemName + forWhat +" 획득했다.";
            descText = "Get Item: " + itemName;
        }
        else{
            contextText = itemName + forWhat + " 사용했다.";
            descText = "Use Item: " + itemName;
        }
        ActivationLog activationLog = new ActivationLog(GetItem ? 100000 + itemID_ : 100000 - itemID_, contextText, descText);
        activationLogList.Add(activationLog);
        uIActivationLogManager.AddUIActivationLog(activationLog);
    }

    public void InActiveActivationLog(){
        for(int i = 0; i < activationLogList.Count; i++){
            activationLogList[i].isObjectViewed = true;
        }

        UpdateUIActivationLogInActive();
    }

    public void UpdateUIActivationLog(){
        if(uIActivationLogManager.uIActivationLogList.Count < activationLogList.Count){
            for(int i = uIActivationLogManager.uIActivationLogList.Count; i < activationLogList.Count; i++){
                uIActivationLogManager.AddUIActivationLog(activationLogList[i]);
            }
        }

        UpdateUIActivationLogInActive();
    }

    private void UpdateUIActivationLogInActive(){
        for(int i = 0; i < activationLogList.Count; i++){
            uIActivationLogManager.InActiveActivationLog(i, activationLogList[i].isObjectViewed);
        }
    }

    public void ResetActivationLogList(){
        activationLogList = new List<ActivationLog>();
    }
}
