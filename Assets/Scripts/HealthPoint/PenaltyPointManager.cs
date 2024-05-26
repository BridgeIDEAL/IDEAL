using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

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

    private int penaltyPoint = 0;
    public static int minPP = 0;

    private float eyeObjectRespawnTime = 60.0f;
    private float eyePenaltyStepTimer = 55.0f;
    private float eyeWatchingGameOverTime = 3.0f;
    private float eyeWatchingTimer = 0.0f;

    
    private float soundPenaltyRespawnTime = 90.0f;
    private float soundPenaltyStepTimer = 85.0f;
    private bool isSoundHearing = false;
    private float soundHearingGameOverTime = 6.0f;
    private float soundHearingTimer = 0.0f;
    private bool insideSafeZone = false;

    public int penaltyGrade(){
        if(penaltyPoint >= 9 ) return 3;
        if(penaltyPoint >= 5) return 2;
        if(penaltyPoint >= 1) return 1;
        return 0;
    }


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
        }
    }

    private void InitPenaltyPoint(){
        penaltyPoint = minPP;
        eyePenaltyStepTimer = 55.0f;
        eyeWatchingTimer = 0.0f;

        soundPenaltyStepTimer = 85.0f;
        isSoundHearing = false;
        soundHearingTimer = 0.0f;
        insideSafeZone = false;
    }

    public int GetPenaltyPoint(){
        return penaltyPoint;
    }

    public void AddPenaltyPoint(int addPoint = 1){
        penaltyPoint += addPoint;
        Debug.Log("Add PenaltyPoint, PP: " + penaltyPoint);
    }

    void Update(){
        // Test Code
        if(Input.GetKeyDown(KeyCode.Y)){
            AddPenaltyPoint();
        }

        if(penaltyGrade() >= 1){
            if(eyePenaltyStepTimer >= eyeObjectRespawnTime){
                // 1. 패널티 오브젝트 생성 / 쿨이 돌았을 때만
                eyePenaltyObject = eyePenaltyManager.ActiveEyePenaltyObject();
                if(eyePenaltyObject != null){
                    eyePenaltyStepTimer = 0.0f;
                }
            }
            eyePenaltyStepTimer += Time.deltaTime;

            
            // 패널티 오브젝트가 존재하는 경우
            if(eyePenaltyObject != null && eyePenaltyObject.gameObject.activeSelf){
                Vector3 targetDir = (eyePenaltyObject.transform.position - playerTransform.position).normalized;
                float angle = Vector3.Angle(targetDir, cameraTransform.forward);

                // 보고 있는 경우
                if(angle < 60.0f){
                    eyeWatchingTimer += Time.deltaTime;
                }

                uIIngame.SetGreenVisualFilter(eyeWatchingTimer / eyeWatchingGameOverTime);

                // 제한 시간 보다 더 보는 경우 게임 오버
                if(eyeWatchingTimer >= eyeWatchingGameOverTime){
                    eyeWatchingTimer = 0.0f;
                    penaltyPoint = 0;
                    IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver("$attempts번 생존자는 눈에서 극심한 고통을 호소하던 중,\n눈을 스스로 뽑아버린 후 신호가 끊기며 실종됨.");
                }

            }
            else{
                if(eyeWatchingTimer > 0.0f){
                    eyeWatchingTimer -= Time.deltaTime;
                    uIIngame.SetGreenVisualFilter(eyeWatchingTimer / eyeWatchingGameOverTime);
                }
                else{
                    eyeWatchingTimer = 0.0f;
                }
            }
        }

        if(penaltyGrade() >=2){
            // Sound Penalty 가능하다면 패널티 적용하기
            if(soundPenaltyStepTimer >= soundPenaltyRespawnTime){
                soundPenaltyStepTimer = 0.0f;
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.playerEffectSound.PlayEffectSound(TempEffectSounds.WarningSiren);
                isSoundHearing = true;
            }
            soundPenaltyStepTimer += Time.deltaTime;

            if(isSoundHearing){
                soundHearingTimer += Time.deltaTime;
                if(soundHearingTimer >= soundHearingGameOverTime){
                    if(!insideSafeZone){
                        IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver("$attempts번 생존자는 보이지 않는 무언가에 휩쓸려가는 소리와 함께 신호가 끊김. \n이후 온몸이 발로 심하게 짓밟힌 흔적과 함께 사망 상태로 발견.");
                    }
                    soundHearingTimer = 0.0f;
                    IdealSceneManager.Instance.CurrentGameManager.scriptHub.playerEffectSound.StopEffectSound();
                    isSoundHearing = false;
                }
            }
        }
    }

    public void GoSafeZone(bool inside){
        insideSafeZone = inside;
    }
}
