using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivationLogManager : MonoBehaviour
{
    [SerializeField] private GameObject uIActivationLogPrefab;

    [SerializeField] private RectTransform activationLogArea;


    private List<UIActivationLog> uIActivationLogList = new List<UIActivationLog>();
    public void Init(){
        uIActivationLogList = new List<UIActivationLog>();
    }

    public void AddUIActivationLog (ActivationLog activationLog){
        GameObject logGameObject = Instantiate(uIActivationLogPrefab);
        RectTransform rt = logGameObject.GetComponent<RectTransform>();
        rt.SetParent(activationLogArea);
        UIActivationLog uIActivationLog = logGameObject.GetComponent<UIActivationLog>();
        uIActivationLog.SetLogText(activationLog.GetContentText());
        uIActivationLog.SetImageActive(true);
        uIActivationLogList.Add(uIActivationLog);
    }

    public void InActiveActivationLog(){
        for(int i = 0; i < uIActivationLogList.Count; i++){
            uIActivationLogList[i].SetImageActive(false);
        }
    }
}
