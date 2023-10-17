using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTimerStart : AbstractInteraction
{
    [SerializeField] private TimerRoot timerRoot;
    protected override string GetDetectedString(){
        return "Press E, Start Timer!";
    }

    protected override void ActInteraction(){
        timerRoot.StartTimer();
    }
}
