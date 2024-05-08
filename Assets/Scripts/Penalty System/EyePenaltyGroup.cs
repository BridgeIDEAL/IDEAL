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
            eyePenaltyObject_Bottom.SetPlayerTransform(eyePenaltyManager.playerTransform);
            eyePenaltyObject_Bottom.PlayActiveSound();
            return eyePenaltyObject_Bottom;
        }
        else{
            eyePenaltyObject_Top.gameObject.SetActive(true);
            eyePenaltyObject_Top.SetPlayerTransform(eyePenaltyManager.playerTransform);
            eyePenaltyObject_Top.PlayActiveSound();
            return eyePenaltyObject_Top;
        }
    }
}
