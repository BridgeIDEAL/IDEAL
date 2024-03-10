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

    [SerializeField] private ScriptHub scriptHub;
    private ActivationLogData activationLogData;
    private UIActivationLogManager uIActivationLogManager;

    private List<int> activationLogList = new List<int>();

    private class ItemID_Name{
        public int itemID;
        public string itemName;
    }
    private List<ItemID_Name> itemID_NameList= new List<ItemID_Name>{
        new ItemID_Name{itemID = 1201, itemName = "알코올램프를"},
        new ItemID_Name{itemID = 1202, itemName = "분필을"},
        new ItemID_Name{itemID = 1106, itemName = "핸드크림을"},
        new ItemID_Name{itemID = 1105, itemName = "약병을"},
        new ItemID_Name{itemID = 1104, itemName = "확인증을"},
        new ItemID_Name{itemID = 1103, itemName = "옥상문 열쇠를"},
        new ItemID_Name{itemID = 1101, itemName = "교무센터 열쇠를"},
        new ItemID_Name{itemID = 1102, itemName = "1학년 교무실 열쇠를"},
        new ItemID_Name{itemID = 1110, itemName = "무전기를"},
    };

    public void Init(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }

        activationLogData = scriptHub.activationLogData;
        uIActivationLogManager = scriptHub.uIActivationLogManager;

        activationLogData.Init();
        activationLogList = new List<int>();
    }

    public void AddActivationLog(int logID){
        activationLogList.Add(logID);
        ActivationLog activationLog = activationLogData.GetActivationLog(logID);
        uIActivationLogManager.AddUIActivationLog(activationLog);
    }

    public void AddActivationLogWithItem(int itemID_, bool GetItem, string forWhat = ""){
        activationLogList.Add(GetItem ? 100000 + itemID_ : 100000 - itemID_);
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
        uIActivationLogManager.AddUIActivationLog(activationLog);
    }

    public void InActiveActivationLog(){
        uIActivationLogManager.InActiveActivationLog();
    }


}
