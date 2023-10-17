using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTimerEnd : AbstractInteraction
{
    [SerializeField] private TimerRoot timerRoot;
    
    protected override string GetDetectedString(){
        return "Press E, End Timer!";
    }

    protected override void ActInteraction(){
        timerRoot.StopTimer();
    }
}
