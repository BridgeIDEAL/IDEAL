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
    }

    public void StopTimer(){
        timerActive = false;
        Debug.Log("======= StopTimer spendTime:  " + spendTime);
    }
    
    void Update(){
        if(timerActive){
            spendTime += Time.deltaTime;
        }
    }
}
