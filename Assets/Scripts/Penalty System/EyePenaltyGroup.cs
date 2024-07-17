using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePenaltyGroup : MonoBehaviour
{
    [SerializeField] private EyePenaltyManager eyePenaltyManager;
    [SerializeField] private EyePenaltyObject eyePenaltyObject_Top;
    [SerializeField] private EyePenaltyObject eyePenaltyObject_Bottom;

    public EyePenaltyObject ActiveEyePenaltyObject(){
        if(Random.Range(0, 2) == 0){
            eyePenaltyObject_Bottom.gameObject.SetActive(true);
            // 바닥에 있는 경우 플레이어 몸통의 중심으로 잡는 경우가 더 자연스러움
            eyePenaltyObject_Bottom.SetPlayerTransform(eyePenaltyManager.playerTransform);
            eyePenaltyObject_Bottom.PlayActiveSound();
            return eyePenaltyObject_Bottom;
        }
        else{
            eyePenaltyObject_Top.gameObject.SetActive(true);
            // 천장에 있는 경우 머리(카메라)를 중심으로 잡는 경우가 더 자연스러움
            eyePenaltyObject_Top.SetPlayerTransform(eyePenaltyManager.cameraTransform);
            eyePenaltyObject_Top.PlayActiveSound();
            return eyePenaltyObject_Top;
        }
    }
}
