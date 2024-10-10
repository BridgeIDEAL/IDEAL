using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneColliderManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Rest")) {
            PenaltyPointManager.Instance.GoSafeZone(true);
        }
        if (other.CompareTag("FreezeRoom")) {
            PenaltyPointManager.Instance.GoFreezeZone(true);
        }

        if(other.CompareTag("Outside")){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.UpdateAreaCondition(true, IdealArea.Outside);
        }
        if(other.CompareTag("GuardRoom")){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.UpdateAreaCondition(true, IdealArea.GuardRoom);
        }

        if(other.CompareTag("VHSEffectRoom")){
            // IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIIngame.VHSEffectPlay();
            IdealSceneManager.Instance.RadialBlurActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Rest")) {
            PenaltyPointManager.Instance.GoSafeZone(false);
        }
        if (other.CompareTag("FreezeRoom")) {
            PenaltyPointManager.Instance.GoFreezeZone(false);
        }

        if(other.CompareTag("Outside")){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.UpdateAreaCondition(false, IdealArea.Outside);
        }
        if(other.CompareTag("GuardRoom")){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.UpdateAreaCondition(false, IdealArea.GuardRoom);
        }
        if(other.CompareTag("VHSEffectRoom")){
            IdealSceneManager.Instance.RadialBlurActive(true);
        }
    }
}
