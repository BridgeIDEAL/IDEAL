using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePenaltyManager : MonoBehaviour
{
    public ScriptHub scriptHub;
    public Transform playerTransform;
    public Transform cameraTransform;

    [SerializeField] private EyePenaltyGroup[] eyePenaltyGroups;    // 이후 층별로 분리

    void Awake(){
        playerTransform = scriptHub.playerArmatureObject.transform;
        cameraTransform = scriptHub.playerCameraRootObject.transform;
    }
    
    public EyePenaltyObject ActiveEyePenaltyObject(){
        Debug.Log("ActiveEye Penalty");
        EyePenaltyGroup closestEyePenaltyGroup = null;
        float closestDistance =  1234567890.3f;
        for(int i = 0; i < eyePenaltyGroups.Length; i++){
            Vector3 targetDir = (eyePenaltyGroups[i].transform.position - playerTransform.position).normalized;
            float angle = Vector3.Angle(targetDir, cameraTransform.forward);

            
            float distance = Vector3.Distance(playerTransform.position, eyePenaltyGroups[i].transform.position);
            if(distance < closestDistance && angle > 90.0f){
                
                closestEyePenaltyGroup = eyePenaltyGroups[i];
                closestDistance = distance;
            }
        }

        if (closestEyePenaltyGroup == null) return null;
        return closestEyePenaltyGroup.ActiveEyePenaltyObject();
    }
}
