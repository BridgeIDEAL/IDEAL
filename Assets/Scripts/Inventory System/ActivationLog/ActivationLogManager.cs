using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void InActiveActivationLog(){
        uIActivationLogManager.InActiveActivationLog();
    }


}
