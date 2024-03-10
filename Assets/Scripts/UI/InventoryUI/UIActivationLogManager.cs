using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivationLogManager : MonoBehaviour
{
    [SerializeField] private GameObject uIActivationLogPrefab;

    [SerializeField] private RectTransform activationLogArea;

    public bool noSpace = false;
    public bool randomSpace = false;
    public bool reverseString = false;
    public bool reverseWord = false;


    private List<UIActivationLog> uIActivationLogList = new List<UIActivationLog>();
    public void Init(){
        uIActivationLogList = new List<UIActivationLog>();
    }

    public void AddUIActivationLog (ActivationLog activationLog){
        GameObject logGameObject = Instantiate(uIActivationLogPrefab);
        RectTransform rt = logGameObject.GetComponent<RectTransform>();
        rt.SetParent(activationLogArea);
        UIActivationLog uIActivationLog = logGameObject.GetComponent<UIActivationLog>();
        uIActivationLog.uIActivationLogManager = this;
        uIActivationLog.SetLogText(activationLog.GetContentText());
        uIActivationLog.SetImageActive(true);
        uIActivationLogList.Add(uIActivationLog);
    }

    public void InActiveActivationLog(){
        for(int i = 0; i < uIActivationLogList.Count; i++){
            uIActivationLogList[i].SetImageActive(false);
        }
    }

    public void ApplyMentalPenaltyActivationLog(MentalPenalty mentalPenalty, bool active){
        switch(mentalPenalty){
            case MentalPenalty.NoSpace:
                noSpace = active;
                break;
            case MentalPenalty.RandomSpace:
                randomSpace = active;
                break;
            case MentalPenalty.ReverseString:
                reverseString = active;
                break;
            case MentalPenalty.ReverseWord:
                reverseWord = active;
                break;
            default:
                break;
        }

        for(int i = 0; i < uIActivationLogList.Count; i++){
            uIActivationLogList[i].LogUpdate();
        }
    }
}
