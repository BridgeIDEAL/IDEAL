using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenaltyPointManager : MonoBehaviour
{
    private static PenaltyPointManager instance;
    public static PenaltyPointManager Instance{
        get{
            if(instance == null){
                return null;
            }
            return instance;
        }
    }

    public ScriptHub scriptHub;
    private UIIngame uIIngame;
    private ThirdPersonController thirdPersonController;
    private Transform playerTransform;
    private Transform cameraTransform;
    private EyePenaltyManager eyePenaltyManager;
    private EyePenaltyObject eyePenaltyObject;

    private float eyeObjectRespawnTime = 60.0f;
    public float eyePenaltyStepTimer = 0.0f;
    private float eyeWatchingGameOverTime = 3.0f;
    private float eyeWatchingTimer = 0.0f;

    
    private float soundPenaltyRespawnTime = 90.0f;
    private float soundPenaltyStepTimer = 0.0f;
    private bool isSoundHearing = false;
    private float soundHearingGameOverTime = 10.0f;
    private float soundHearingTimer = 0.0f;
    private bool insideSafeZone = false;

    private bool isTimerFreeze = false;
    public bool isChased = false;

    private bool inLobby = true;

    private bool inPrototypeSecond = false;

    public void Init(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }

        InitPenaltyPoint();
    }

    public void EnterAnotherSceneInit(bool isLobby){
        if(isLobby){
            InitPenaltyPoint();
        }
        else{
            uIIngame = scriptHub.uIIngame;
            thirdPersonController = scriptHub.thirdPersonController;
            eyePenaltyManager = scriptHub.eyePenaltyManager;
            playerTransform = scriptHub.playerArmatureObject.transform;
            cameraTransform = scriptHub.playerCameraRootObject.transform;

            inLobby = false;
            if(SceneManager.GetActiveScene().name == "Prototype_Second"){
                inPrototypeSecond = true;
            }
            else{
                inPrototypeSecond = false;
            }
        }
    }

    private void InitPenaltyPoint(){
        eyePenaltyStepTimer = 0.0f;
        eyeWatchingTimer = 0.0f;

        soundPenaltyStepTimer = 0.0f;
        isSoundHearing = false;
        soundHearingTimer = 0.0f;
        insideSafeZone = false;
        inLobby = true;
        inPrototypeSecond = false;
    }


    void Update(){

        if(eyePenaltyStepTimer >= eyeObjectRespawnTime){
            // 1. 패널티 오브젝트 생성 / 쿨이 돌았을 때만
            eyePenaltyObject = eyePenaltyManager.ActiveEyePenaltyObject();
            if(eyePenaltyObject != null){
                eyePenaltyStepTimer = 0.0f;
            }
        }
        if(!inPrototypeSecond && !inLobby &&!isChased && !isTimerFreeze && !insideSafeZone) eyePenaltyStepTimer += Time.deltaTime;

        
        // 패널티 오브젝트가 존재하는 경우
        if(eyePenaltyObject != null && eyePenaltyObject.gameObject.activeSelf){
            Vector3 targetDir = (eyePenaltyObject.transform.position - playerTransform.position).normalized;
            float angle = Vector3.Angle(targetDir, cameraTransform.forward);

            // 보고 있는 경우
            if(angle < 60.0f){
                eyeWatchingTimer += Time.deltaTime;
            }

            uIIngame.SetGreenVisualFilter(eyeWatchingTimer / eyeWatchingGameOverTime * 0.7f);

            // 제한 시간 보다 더 보는 경우 게임 오버
            if(eyeWatchingTimer >= eyeWatchingGameOverTime){
                eyeWatchingTimer = 0.0f;
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver(7);
            }

        }
        else{
            if(eyeWatchingTimer > 0.0f){
                eyeWatchingTimer -= Time.deltaTime;
                uIIngame.SetGreenVisualFilter(eyeWatchingTimer / eyeWatchingGameOverTime * 0.7f);
            }
            else{
                eyeWatchingTimer = 0.0f;
            }
        }
        

        // Sound Penalty 가능하다면 패널티 적용하기
        if(soundPenaltyStepTimer >= soundPenaltyRespawnTime){
            soundPenaltyStepTimer = 0.0f;
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.playerEffectSound.PlayEffectSound(TempEffectSounds.WarningSiren);
            isSoundHearing = true;
        }
        if(!inLobby && !isChased && !isTimerFreeze && !insideSafeZone) soundPenaltyStepTimer += Time.deltaTime;

        if(isSoundHearing){
            soundHearingTimer += Time.deltaTime;
            if(soundHearingTimer >= soundHearingGameOverTime){
                if(!insideSafeZone){
                    IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver(6);
                }
                soundHearingTimer = 0.0f;
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.playerEffectSound.StopEffectSound();
                isSoundHearing = false;
            }
        }
        
    }

    public void GoSafeZone(bool inside){
        insideSafeZone = inside;
    }

    public void GoFreezeZone(bool inside){
        isTimerFreeze = inside;
    }
}
