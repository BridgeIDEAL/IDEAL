using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;



public class CameraEffectManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cameraPerlin;
    [SerializeField] private VolumeProfile volumeProfile;
    private DepthOfField depthOfField;
    [SerializeField] UIManager uIManager;

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

        // EyePenaltyManager
        if(PenaltyPointManager.Instance.isTimeWatchPenalty){
            cameraPerlin.m_FrequencyGain = PenaltyPointManager.Instance.CurShakingFrequency;
            cameraPerlin.m_AmplitudeGain = PenaltyPointManager.Instance.CurShakingIntensity;

            if(PenaltyPointManager.Instance.playerTransform != null && PenaltyPointManager.Instance.eyePenaltyObject != null){
                depthOfField.focusDistance.value = Vector3.Distance(PenaltyPointManager.Instance.playerTransform.position, PenaltyPointManager.Instance.eyePenaltyObject.transform.position);
            }
            depthOfField.focalLength.value = PenaltyPointManager.Instance.CurDepthOfFieldInstensity;

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


}
