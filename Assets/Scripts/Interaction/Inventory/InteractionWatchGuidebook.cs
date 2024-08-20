using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWatchGuidebook : AbstractInteraction
{
    [SerializeField] private string detectedStr;
    [SerializeField] private string afterInteractionStr = "";
    [SerializeField] private int activationLogNum = -1;
    // [SerializeField] private int checkListNum = -1;
    [SerializeField] private float requiredTime = 0.5f;
    public override float RequiredTime { get => requiredTime;}

    protected override string GetDetectedString(){
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction() {
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIManager.ActiveGuideBook();
        
        if (activationLogNum != -1) {
            //ActivationLogManager.Instance.AddActivationLog(activationLogNum);
        }
        // if(checkListNum != -1){
        //     ProgressManager.Instance.UpdateCheckList(checkListNum, 1);
        // }
        if (afterInteractionStr != "") {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(afterInteractionStr);
        }   
    }
}
