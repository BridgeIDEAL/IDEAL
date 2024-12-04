using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTimerEnd : AbstractInteraction
{
    [SerializeField] private TimerRoot timerRoot;
    public override float RequiredTime { get => 2.0f;}
    
    protected override string GetDetectedString(){
        return "Press E, End Timer!";
    }

    protected override void ActInteraction(){
        timerRoot.StopTimer();
    }
}
