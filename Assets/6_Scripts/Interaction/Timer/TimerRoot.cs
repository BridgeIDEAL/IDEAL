using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerRoot : MonoBehaviour
{
    
    public bool timerActive = false;
    
    private float spendTime = 0.0f;
    

    public void StartTimer(){
        spendTime = 0.0f;
        timerActive = true;
        Debug.Log("======= StartTimer");
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.SetTimerEndActive(true);
    }

    public void StopTimer(){
        timerActive = false;
        Debug.Log("======= StopTimer spendTime:  " + spendTime);
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.SetTimerEndActive(false);
    }
    
    void Update(){
        if(timerActive){
            spendTime += Time.deltaTime;
        }
    }
}
