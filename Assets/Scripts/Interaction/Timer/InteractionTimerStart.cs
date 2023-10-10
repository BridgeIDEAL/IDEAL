using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTimerStart : AbstractInteraction
{
    [SerializeField] private TimerRoot timerRoot;
    protected override string GetDetectedString(){
        return "TimerStart Detected!";
    }

    protected override void ActInteraction(){
        timerRoot.StartTimer();
    }
}
