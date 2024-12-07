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
    public Transform playerTransform;
    private Transform cameraTransform;
    public bool isTimeWatchPenalty = false;
    public bool isEyePenaltyDeath = false;

    public bool isSoundPenaltyDeath = false;
    [SerializeField] private float shakingFrequency = 10.0f;
    [SerializeField] private float shakingIntensity = 1.0f;
    public float CurShakingFrequency = 0.0f;
    public float CurShakingIntensity = 0.0f;

    [SerializeField] private float colorShiftIntensity = 0.05f;

    [SerializeField] private float depthOfFieldInstensity = 100.0f;
    public float CurDepthOfFieldInstensity = 0.0f;

    private EyePenaltyManager eyePenaltyManager;
    public EyePenaltyObject eyePenaltyObject;

    private float eyeObjectRespawnTime = 60.0f;
    public float eyePenaltyStepTimer = 0.0f;
    private const float eyeWatchingGameOverTime = 3.0f;
    private float eyeWatchingTimer = 0.0f;

    
    private float soundPenaltyRespawnTime = 100.0f;
    public float soundPenaltyStepTimer = 0.0f;
    private float soundPenaltyDelay = 20.0f;
    public bool isSoundHearing = false;
    private const float soundHearingGameOverTime = 10.0f;
    private float soundHearingTimer = 0.0f;
    public float CurSoundPenaltyInstensity = 0.0f;
    private bool insideSafeZone = false;

    private bool isTimerFreeze = false;
    private bool isChased = false;

    private bool inLobby = true;

    private bool inPrototypeSecond = false;
    public bool watchIntroEnded = false;


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
            watchIntroEnded = false;
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
        isTimeWatchPenalty = false;
        isEyePenaltyDeath = false;
        isSoundPenaltyDeath = false;
        CurShakingFrequency = 0.0f;
        CurShakingIntensity = 0.0f;
        CurDepthOfFieldInstensity = 0.0f;
        CurSoundPenaltyInstensity = 0.0f;
    }

    public void OnChangeScene(){
        // 유저가 씬을 바꿨을 때 바로 사이렌이 울리면 대처하기 어려우므로
        if(soundPenaltyRespawnTime - soundPenaltyStepTimer < soundPenaltyDelay){
            soundPenaltyStepTimer = soundPenaltyRespawnTime - soundPenaltyDelay;
        }

        // 사운드가 들리는 중이었으면 그 시점부터 다시 재생
        if(isSoundHearing){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.playerEffectSound.PlayEffectSound(TempEffectSounds.WarningSiren, soundHearingTimer);
        }
    }


    void Update(){
        if(!watchIntroEnded || scriptHub.ambienceSoundManager.currentArea == IdealArea.Outside){
            return;
        }
        if(eyePenaltyStepTimer >= eyeObjectRespawnTime){
            // 1. 패널티 오브젝트 생성 / 쿨이 돌았을 때만
            eyePenaltyObject = eyePenaltyManager.ActiveEyePenaltyObject();
            if(eyePenaltyObject != null){
                eyePenaltyStepTimer = 0.0f;
            }
        }
        if(!inLobby &&!isChased && !isTimerFreeze && !insideSafeZone
        && !IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.isEnd) {
            eyePenaltyStepTimer += Time.deltaTime;
        }

        
        // 패널티 오브젝트가 존재하는 경우
        if(eyePenaltyObject != null && eyePenaltyObject.gameObject.activeSelf){
            Vector3 targetDir = (eyePenaltyObject.transform.position - playerTransform.position).normalized;
            float angle = Vector3.Angle(targetDir, cameraTransform.forward);

            // 보고 있는 경우
            if(angle < 60.0f){
                eyeWatchingTimer += Time.deltaTime;
            }

            // uIIngame.SetGreenVisualFilter(eyeWatchingTimer / eyeWatchingGameOverTime * 0.7f);
            isTimeWatchPenalty = true;
            CurShakingFrequency = eyeWatchingTimer / eyeWatchingGameOverTime * shakingFrequency;
            CurShakingIntensity = eyeWatchingTimer / eyeWatchingGameOverTime * shakingIntensity;
            IdealSceneManager.Instance.SetColorSplitStrength(eyeWatchingTimer / eyeWatchingGameOverTime * colorShiftIntensity);
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.SetTinnitusSound(eyeWatchingTimer / eyeWatchingGameOverTime * 0.5f);

            CurDepthOfFieldInstensity = eyeWatchingTimer / eyeWatchingGameOverTime * depthOfFieldInstensity;

            
            // 제한 시간 보다 더 보는 경우 게임 오버
            if(eyeWatchingTimer >= eyeWatchingGameOverTime){
                isEyePenaltyDeath = true;
            }

        }
        else{
            if(!isEyePenaltyDeath){
                if(eyeWatchingTimer > 0.0f){
                    eyeWatchingTimer -= Time.deltaTime;
                    // uIIngame.SetGreenVisualFilter(eyeWatchingTimer / eyeWatchingGameOverTime * 0.7f);
                    CurShakingFrequency = eyeWatchingTimer / eyeWatchingGameOverTime * shakingFrequency;
                    CurShakingIntensity = eyeWatchingTimer / eyeWatchingGameOverTime * shakingIntensity;
                    IdealSceneManager.Instance.SetColorSplitStrength(eyeWatchingTimer / eyeWatchingGameOverTime * colorShiftIntensity);
                    IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.SetTinnitusSound(eyeWatchingTimer / eyeWatchingGameOverTime * 0.5f);
                }
                else{
                    eyeWatchingTimer = 0.0f;
                    isTimeWatchPenalty = false;
                    IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.OffTinnitusSound();
                }
            }
            
        }
        
        // 라스트 런에는 사운드 패널티 없음
        if(ProgressManager.Instance.lastRunning){
            soundPenaltyStepTimer = 0.0f;
        }

        // Sound Penalty 가능하다면 패널티 적용하기
        if(soundPenaltyStepTimer >= soundPenaltyRespawnTime){
            soundPenaltyStepTimer = 0.0f;
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.playerEffectSound.PlayEffectSound(TempEffectSounds.WarningSiren);
            isSoundHearing = true;
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.playerHandLight.EffectOnLight();

            EntityDataManager.Instance.Controller.InActivePrincipal();
        }
        if(!inLobby && !isChased && !isTimerFreeze && !insideSafeZone
        && !IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.isEnd) {
            soundPenaltyStepTimer += Time.deltaTime;
        }

        

        if(isSoundHearing){
            soundHearingTimer += Time.deltaTime;
            CurSoundPenaltyInstensity = soundHearingTimer / soundHearingGameOverTime * 1.0f;
            if(soundHearingTimer >= soundHearingGameOverTime){
                if(!insideSafeZone){
                    isSoundPenaltyDeath = true;
                    IdealSceneManager.Instance.CurrentGameManager.scriptHub.playerEffectSound.FadeStopEffectSound(0.5f, 4.0f);
                }
                else{
                    IdealSceneManager.Instance.CurrentGameManager.scriptHub.playerEffectSound.FadeStopEffectSound(0.5f);
                }
                
                isSoundHearing = false;
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.playerHandLight.EffectOffLight();
                
                EntityDataManager.Instance.Controller.ActivePrincipal();
            }
        }
        else{
            if(!isSoundPenaltyDeath){
                float soundDecay = isSoundPenaltyDeath ? 4.0f : 2.0f;
                if(soundHearingTimer > 0.0f){
                    soundHearingTimer -= Time.deltaTime * soundDecay;
                    CurSoundPenaltyInstensity = soundHearingTimer / soundHearingGameOverTime * 1.0f;
                }
                else{
                    soundHearingTimer = 0.0f;
                    CurSoundPenaltyInstensity = 0.0f;
                }
            }
        }
        
    }

    public void GoSafeZone(bool inside){
        insideSafeZone = inside;
    }

    public void GoFreezeZone(bool inside){
        isTimerFreeze = inside;
    }

    public void SetChase(bool chasing){
        if(chasing){
            isChased = true;
            
            // 추격 끝난 뒤 바로 사이렌 울리는 것도 좋지 않으므로
            // 유격을 주기 위해 
            OnChangeScene();
        }
        else{
            isChased = false;
        }
    }
}
