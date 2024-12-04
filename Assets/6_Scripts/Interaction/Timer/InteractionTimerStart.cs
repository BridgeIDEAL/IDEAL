using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InteractionTimerStart : AbstractInteraction
{
    [SerializeField] private TimerRoot timerRoot;

    public override float RequiredTime { get => 2.0f;}

    protected override string GetDetectedString(){
        return "Press E, Start Timer!";
    }

    protected override void ActInteraction(){
        timerRoot.StartTimer();
    }
    
}
