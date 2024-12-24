using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using StarterAssets;

[System.Serializable]
public class SoundPenaltyFootClip{
    public AudioClip[] clips;
}

public class CameraEffectManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    public GameObject playerCameraRootObject;
    private CinemachineBasicMultiChannelPerlin cameraPerlin;
    [SerializeField] private VolumeProfile volumeProfile;
    private DepthOfField depthOfField;
    [SerializeField] UIManager uIManager;
    [SerializeField] UIIngame uIIngame;
    [SerializeField] ThirdPersonController thirdPersonController;

    [SerializeField] private AudioSource breathSource;
    [SerializeField] private AudioClip[] startBreathClips;
    [SerializeField] private AudioClip[] middleBreathClips;
    [SerializeField] private AudioClip[] endBreathClips;

    [SerializeField] private AudioSource eyeAudioSource;

    [SerializeField] private AudioSource[] SoundPenaltyFootSources;
    [SerializeField] private SoundPenaltyFootClip[] SoundPenaltyFootClips;
    [SerializeField] private AudioSource SoundPenaltyBGMSource;
    [SerializeField] private AudioSource SoundPenaltyDeathSource;

    private AudioClip lastBreathClip = null;

    private bool isShowEyePenaltyDeadScene = false;
    private float eyePenaltyDeadTime = 0.5f;

    private bool isShowSoundPenaltyDeadScene = false;
    private const float moveDownTime = 0.5f;
    private const float soundPenaltyDeadTime = 3.5f;

    private Coroutine breathCoroutine = null;
    public float breathIntensity = 0.0f;
    private float breathBlurIntensity = 50.0f;

    private bool isBreathingStop = false;

    private Coroutine eyeDeathCoroutine = null;
    private Coroutine soundDeathCoroutine = null;

    private List<Transform> chasingEntities = new List<Transform>();


    void Awake(){
        cameraPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (volumeProfile.TryGet<DepthOfField>(out depthOfField))
        {
            Debug.Log("depthOfField component found in Volume Profile");
        }
        else
        {
            Debug.LogWarning("depthOfField component not found in Volume Profile");
        }

        if(breathCoroutine != null){
            StopCoroutine(breathCoroutine);
        }
        breathCoroutine = StartCoroutine(BreathCoroutine());
    }

    public void AddChasingEntities(Transform entityTransform){
        chasingEntities.Add(entityTransform);
    }

    public void RemoveChasingEntities(Transform entityTransform){
        chasingEntities.Remove(entityTransform);
    }

    IEnumerator BreathCoroutine(){
        float breathBlur = 0.0f;
        while(true){
            if(isShowEyePenaltyDeadScene || isBreathingStop){
                yield return null;
                continue;
            }
            if(PenaltyPointManager.Instance.isSoundPenaltyDeath && !isShowSoundPenaltyDeadScene){
                isShowSoundPenaltyDeadScene = true;
                if(soundDeathCoroutine != null){
                    StopCoroutine(soundDeathCoroutine);
                }
                soundDeathCoroutine = StartCoroutine(ShowSoundPenaltyDeadSceneCoroutine());
                yield return null;
                continue;
            }

            float penaltyIntensity = PenaltyPointManager.Instance.CurSoundPenaltyInstensity;
            float lastRunIntensity = ProgressManager.Instance.lastRunning ? 0.25f : 0.0f;

            float chaseInensity =0.0f;
            if(chasingEntities != null){
                foreach(var entityTransform in chasingEntities){
                    float thisInensity = 1 - Vector3.Distance(entityTransform.position, playerCameraRootObject.transform.position) / 20.0f;
                    if(thisInensity > chaseInensity){
                        chaseInensity = thisInensity;
                    }
                }
            }


            breathIntensity = Mathf.Max(penaltyIntensity, chaseInensity, lastRunIntensity, PVManager.Instance.breathValue);

            // breath Source에 재생시킬 clip 설정하기
            if(!breathSource.isPlaying){
                int i  = 0;
                while(true){
                    breathSource.clip = GetBreathClip();
                    if(breathSource.clip != lastBreathClip){
                        lastBreathClip = breathSource.clip;
                        breathSource.Play();
                        break;
                    }
                    i++;
                    if(i > 5){
                        breathSource.clip = lastBreathClip;
                        breathSource.Play();
                        break;
                    }
                }
            }
            breathSource.volume = breathIntensity;

            // breath 주기에 따라 Blur 효과 주도록

            breathBlur = GetCurveValue(breathSource.time / breathSource.clip.length) * breathIntensity  * breathBlurIntensity * 2.0f;
            if(breathBlur > breathBlurIntensity){
                breathBlur = breathBlurIntensity;
            }  

            if(breathBlur > PenaltyPointManager.Instance.CurDepthOfFieldInstensity){
                depthOfField.focusDistance.value = 1.0f;
                depthOfField.focalLength.value =  breathBlur;
            }

            cameraPerlin.m_FrequencyGain = 0.3f + breathIntensity * 6.0f;
            cameraPerlin.m_AmplitudeGain = 0.5f + breathIntensity * 1.5f;
            

            yield return null;
        }
    }

    private AudioClip GetBreathClip(){
        if(breathIntensity < 0.3f){
            return startBreathClips[Random.Range(0, startBreathClips.Length)];
        }
        else if(breathIntensity < 0.7f){
            return middleBreathClips[Random.Range(0, middleBreathClips.Length)];
        }
        else{
            return endBreathClips[Random.Range(0, endBreathClips.Length)];
        }
    }

    private float GetCurveValue(float progress)
    {
        return 1 - 4 * Mathf.Pow(progress - 0.5f, 2);
    }

    IEnumerator ShowSoundPenaltyDeadSceneCoroutine(){
        float stepTimer = 0.0f;
        while(stepTimer < moveDownTime){
            thirdPersonController.MoveSpeed = Mathf.Lerp(thirdPersonController.DefaultMoveSpeed, 0.0f, stepTimer / moveDownTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }

        // PenaltyPoint Manager 쪽에서 사운드 패널티 화면 효과는 2.5초 동안 감소하여 0이 되게 설정되어 있음

        // 3개의 발걸음 소리가 랜덤한 방향에서 들림
        SoundPenaltyFootSources[0].panStereo = Random.Range(-1.0f, 1.0f);
        SoundPenaltyFootSources[1].panStereo = Random.Range(-1.0f, 1.0f);
        SoundPenaltyFootSources[2].panStereo = Random.Range(-1.0f, 1.0f);

        SoundPenaltyFootSources[0].volume = 0.0f;
        SoundPenaltyFootSources[1].volume = 0.0f;
        SoundPenaltyFootSources[2].volume = 0.0f;
        SoundPenaltyBGMSource.volume = 0.0f;

        SoundPenaltyFootSources[0].clip = SoundPenaltyFootClips[0].clips[0];
        SoundPenaltyFootSources[1].clip = SoundPenaltyFootClips[1].clips[0];
        SoundPenaltyFootSources[2].clip = SoundPenaltyFootClips[2].clips[0];

        SoundPenaltyFootSources[0].Play();
        SoundPenaltyFootSources[1].Play();
        SoundPenaltyFootSources[2].Play();
        SoundPenaltyBGMSource.Play();
        
        IdealSceneManager.Instance.RadialBlurActive(true);

        stepTimer = 0.0f;
        while(stepTimer < soundPenaltyDeadTime){
            SoundPenaltyBGMSource.volume = Mathf.Lerp(0.0f, 1.0f, stepTimer / soundPenaltyDeadTime);
            SoundPenaltyFootSources[0].volume = Mathf.Lerp(0.0f, 1.0f, stepTimer / soundPenaltyDeadTime);
            SoundPenaltyFootSources[1].volume = Mathf.Lerp(0.0f, 1.0f, stepTimer / soundPenaltyDeadTime);
            SoundPenaltyFootSources[2].volume = Mathf.Lerp(0.0f, 1.0f, stepTimer / soundPenaltyDeadTime);

            for(int i = 0 ; i < 3; i++){
                if(SoundPenaltyFootSources[i].isPlaying == false){
                    SoundPenaltyFootSources[i].clip = SoundPenaltyFootClips[i].clips[Random.Range(0, SoundPenaltyFootClips[i].clips.Length)];
                    SoundPenaltyFootSources[i].Play();
                }
            }

            stepTimer += Time.deltaTime;
            yield return null;
        }

        isBreathingStop = true;

        stepTimer = 0.0f;
        float breathStopTime = breathSource.clip.length - breathSource.time;
        float breathVolume = breathSource.volume;
        while(stepTimer < breathStopTime){
            breathSource.volume = Mathf.Lerp(breathVolume, 0.0f, stepTimer / breathStopTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }

        uIIngame.SetActiveBlackFilter(true);
        SoundPenaltyDeathSource.Play();
        yield return new WaitForSeconds(SoundPenaltyDeathSource.clip.length);

        DeadBySoundPenalty();
    }

    private void DeadBySoundPenalty(){
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOverWithVHSEffect(6);
        if (SteamfeatureController.Instance.FeatureManager.Achievement03.isSirenDeath == false)
        {
            SteamfeatureController.Instance.FeatureManager.Achievement03.isSirenDeath = true;
            SteamfeatureController.Instance.FeatureManager.Achievement03.CheckAllConidtion();
        }
    }

    void Update(){
        if(isShowEyePenaltyDeadScene || isShowSoundPenaltyDeadScene) {
            return;
        }

        // EyePenaltyManager
        if(PenaltyPointManager.Instance.isTimeWatchPenalty){
            cameraPerlin.m_FrequencyGain = PenaltyPointManager.Instance.CurShakingFrequency;
            cameraPerlin.m_AmplitudeGain = PenaltyPointManager.Instance.CurShakingIntensity;

            if(PenaltyPointManager.Instance.playerTransform != null && PenaltyPointManager.Instance.eyePenaltyObject != null){
                depthOfField.focusDistance.value = Vector3.Distance(PenaltyPointManager.Instance.playerTransform.position, PenaltyPointManager.Instance.eyePenaltyObject.transform.position);
            }
            depthOfField.focalLength.value = PenaltyPointManager.Instance.CurDepthOfFieldInstensity;

            if(PenaltyPointManager.Instance.isEyePenaltyDeath){
                isShowEyePenaltyDeadScene = true;
                ShowEyePenaltyDeadScene();
            }

            return;
        }


        // Default DepthOfField
        if(depthOfField != null){
            depthOfField.focusDistance.value = 5.0f;
            depthOfField.focalLength.value = 0.0f;
        }

        // UIManager
        if(uIManager.CameraShake_Lock){
            cameraPerlin.m_FrequencyGain = 0.0f;
            return;
        }
        else{
            cameraPerlin.m_FrequencyGain = 0.3f;
            cameraPerlin.m_AmplitudeGain = 0.5f;
        }
    }

    private void ShowEyePenaltyDeadScene(){
        if(eyeDeathCoroutine != null){
            StopCoroutine(eyeDeathCoroutine);
        }
        eyeDeathCoroutine = StartCoroutine(ShowEyePenaltyDeadSceneCoroutine());
    }

    IEnumerator ShowEyePenaltyDeadSceneCoroutine(){
        thirdPersonController.MoveLock = true;
        uIManager.OnJumpScare();


        if(PenaltyPointManager.Instance.eyePenaltyObject != null){
            thirdPersonController.CameraEnforceLookAt(PenaltyPointManager.Instance.eyePenaltyObject.transform.position);
        }

        cameraPerlin.m_FrequencyGain = 15.0f;
        cameraPerlin.m_AmplitudeGain = 1.5f;
        
        yield return new WaitForSeconds(1.0f);
        
        
        eyeAudioSource.Play();

        float stepTimer = 0.0f;
        while(stepTimer <= eyePenaltyDeadTime){
            uIIngame.SetRedVisualFilter(stepTimer / eyePenaltyDeadTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }

        cameraPerlin.m_FrequencyGain = 0.3f;
        cameraPerlin.m_AmplitudeGain = 0.5f;

        uIIngame.SetActiveRedFilter(true);

        yield return new WaitForSeconds(0.5f);

        DeadByEyePenalty();
    }

    private void DeadByEyePenalty(){
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOverWithVHSEffect(7);
        if (SteamfeatureController.Instance.FeatureManager.Achievement03.isEyePenlatyDeath == false)
        {
            SteamfeatureController.Instance.FeatureManager.Achievement03.isEyePenlatyDeath = true;
            SteamfeatureController.Instance.FeatureManager.Achievement03.CheckAllConidtion();
        }
    }


}
