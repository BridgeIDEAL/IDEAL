using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePenaltyManager : MonoBehaviour
{
    public ScriptHub scriptHub;
    public Transform playerTransform;
    public Transform cameraTransform;

    [SerializeField] private EyePenaltyFloor[] eyePenaltyFloors;    // 이후 층별로 분리
    private float floorInterval = 0.5f;

    void Awake(){
        playerTransform = scriptHub.playerArmatureObject.transform;
        cameraTransform = scriptHub.playerCamera.transform;
    }
    
    public EyePenaltyObject ActiveEyePenaltyObject(){
        EyePenaltyFloor eyePenaltyFloor = null;

        
        foreach(EyePenaltyFloor eyeF in eyePenaltyFloors){
            if(eyeF.transform.position.y >= playerTransform.position.y - floorInterval && 
            eyeF.transform.position.y <= playerTransform.position.y + floorInterval){
                eyePenaltyFloor = eyeF;
                break;
            }
        }

        if(eyePenaltyFloor == null){
            return null;
        }
        else{
            return eyePenaltyFloor.ActiveEyePenaltyObject(playerTransform, cameraTransform);
        }
    }
}
