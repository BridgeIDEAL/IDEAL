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

    private bool isShowEyePenaltyDeadScene = false;
    private float eyePenaltyDeadTime = 0.5f;

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


        if(PenaltyPointManager.Instance.eyePenaltyObject != null){
            thirdPersonController.CameraEnforceLookAt(PenaltyPointManager.Instance.eyePenaltyObject.transform.position);
        }

        cameraPerlin.m_FrequencyGain = 15.0f;
        cameraPerlin.m_AmplitudeGain = 1.5f;
        
        yield return new WaitForSeconds(1.0f);
        
        
        // Vector3 lookAtPosition = playerCameraRootObject.transform.position 
        //                      + playerCameraRootObject.transform.forward * 1.0f // 전방으로 1만큼 이동
        //                      + new Vector3(0.0f, -1.0f, 0.0f); // 수직으로 -5만큼 이동

        //  // CameraEnforceLookAt을 해당 위치로 설정
        // thirdPersonController.CameraEnforceLookAt(lookAtPosition);

        // yield return new WaitForSeconds(0.7f);


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
