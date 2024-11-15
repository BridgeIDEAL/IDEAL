using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePenaltyFloor : MonoBehaviour
{
    [SerializeField] private EyePenaltyGroup[] eyePenaltyGroups;    // 해당 층의 패널티 그룹

    public EyePenaltyObject ActiveEyePenaltyObject(Transform playerTransform, Transform cameraTransform){
        Debug.Log("ActiveEye Penalty");
        EyePenaltyGroup closestEyePenaltyGroup = null;
        float closestDistance =  1234567890.3f;
        for(int i = 0; i < eyePenaltyGroups.Length; i++){
            Vector3 targetDir = (eyePenaltyGroups[i].transform.position - playerTransform.position).normalized;
            float angle = Vector3.Angle(targetDir, cameraTransform.forward);

            
            float distance = Vector3.Distance(playerTransform.position, eyePenaltyGroups[i].transform.position);
            if(angle > 90.0f && distance < closestDistance){
                
                closestEyePenaltyGroup = eyePenaltyGroups[i];
                closestDistance = distance;
            }
        }

        if (closestEyePenaltyGroup == null) return null;
        return closestEyePenaltyGroup.ActiveEyePenaltyObject();
    }
}
