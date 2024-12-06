using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using StarterAssets;



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

    private AudioClip lastBreathClip = null;

    private bool isShowEyePenaltyDeadScene = false;
    private float eyePenaltyDeadTime = 0.5f;

    private Coroutine breathCoroutine = null;
    public float breathIntensity = 0.0f;
    private float breathBlurIntensity = 50.0f;

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

    IEnumerator BreathCoroutine(){
        float breathBlur = 0.0f;
        while(true){
            if(isShowEyePenaltyDeadScene){
                yield return null;
            }

            breathIntensity = PenaltyPointManager.Instance.CurSoundPenaltyInstensity;

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

    void Update(){
        if(isShowEyePenaltyDeadScene) {
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
        StartCoroutine(ShowEyePenaltyDeadSceneCoroutine());
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
