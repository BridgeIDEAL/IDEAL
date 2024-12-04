using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneColliderManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if(IdealSceneManager.Instance.isWatchingIntro) return;

        if (other.CompareTag("Rest")) {
            PenaltyPointManager.Instance.GoSafeZone(true);
        }
        if (other.CompareTag("FreezeRoom")) {
            PenaltyPointManager.Instance.GoFreezeZone(true);
        }

        if(other.CompareTag("Outside")){
            if(IdealSceneManager.Instance.CurrentGameManager != null){
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.UpdateAreaCondition(true, IdealArea.Outside);
            }
        }
        if(other.CompareTag("GuardRoom")){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.guardCCTVSound.TurnOnCCTV();
        }

        if(other.CompareTag("VHSEffectRoom")){
            // IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIIngame.VHSEffectPlay();
            // IdealSceneManager.Instance.RadialBlurActive(true);
        }

        if(other.CompareTag("MedicalRoom")){
            if(CountAttempts.Instance.GetAttemptCount() > 1){
                RoomArchiveLogManager.Instance.UpdateArchiveLogData(0702, CountAttempts.Instance.GetAttemptCount());
            }
        }

        if(other.CompareTag("TeacherRoom1F")){
            RoomArchiveLogManager.Instance.UpdateArchiveLogData(1302, CountAttempts.Instance.GetAttemptCount());
        }

        if(other.CompareTag("TeacherRoom2F")){
            RoomArchiveLogManager.Instance.UpdateArchiveLogData(1303, CountAttempts.Instance.GetAttemptCount());
        }

        if(other.CompareTag("ServerRoom")){
            RoomArchiveLogManager.Instance.UpdateArchiveLogData(1602, CountAttempts.Instance.GetAttemptCount());
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.ServerRoomStart();
        }

        if(other.CompareTag("MusicRoom")){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.MusicRoomStart();
        }
        if(other.CompareTag("BroadCastRoom")){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.BroadCastRoomStart();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(IdealSceneManager.Instance.isWatchingIntro) return;
        
        if (other.CompareTag("Rest")) {
            PenaltyPointManager.Instance.GoSafeZone(false);
        }
        if (other.CompareTag("FreezeRoom")) {
            PenaltyPointManager.Instance.GoFreezeZone(false);
        }

        if(other.CompareTag("Outside")){
            if(IdealSceneManager.Instance.CurrentGameManager != null){
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.UpdateAreaCondition(false, IdealArea.Outside);
            }   
        }
        if(other.CompareTag("GuardRoom")){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.guardCCTVSound.TurnOffCCTV();
        }
        if(other.CompareTag("VHSEffectRoom")){
            // IdealSceneManager.Instance.RadialBlurActive(true);
        }

        if(other.CompareTag("MusicRoom")){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.MusicRoomEnd();
        }
        if(other.CompareTag("ServerRoom")){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.ServerRoomEnd();
        }
        if(other.CompareTag("BroadCastRoom")){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.BroadCastRoomEnd();
        }
    }
}
