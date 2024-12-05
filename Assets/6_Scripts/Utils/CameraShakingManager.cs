using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class CameraShakingManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cameraPerlin;
    [SerializeField] UIManager uIManager;

    void Awake(){
        cameraPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update(){

        // EyePenaltyManager
        if(PenaltyPointManager.Instance.isTimeWatchPenalty){
            cameraPerlin.m_FrequencyGain = PenaltyPointManager.Instance.CurShakingFrequency;
            cameraPerlin.m_AmplitudeGain = PenaltyPointManager.Instance.CurShakingIntensity;
            return;
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
