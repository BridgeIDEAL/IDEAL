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

    [SerializeField] ActivationLogData activationLogData;
    [SerializeField] UIActivationLogManager uIActivationLogManager;

    private List<int> activationLogList = new List<int>();

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }

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
