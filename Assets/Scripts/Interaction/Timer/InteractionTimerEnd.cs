using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTimerEnd : AbstractInteraction
{
    [SerializeField] private TimerRoot timerRoot;
    
    protected override string GetDetectedString(){
        return "TimerEnd Detected!";
    }

    protected override void ActInteraction(){
        timerRoot.StopTimer();
    }
}
