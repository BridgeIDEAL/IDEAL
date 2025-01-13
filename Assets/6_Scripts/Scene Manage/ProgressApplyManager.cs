using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressApplyManager : MonoBehaviour
{
    [SerializeField] private InteractionGetItem[] interactionGetItems;
    [SerializeField] private InteractionDoor[] interactionDoors;

    public void Init(){
        for(int i = 0; i < interactionGetItems.Length; i++){
            if(ProgressManager.Instance.GetItemLogExist(interactionGetItems[i].interactionItemData.ID)){
                // 아이템을 이미 획득한 상태라면 해당 아이템 비활성화
                interactionGetItems[i].gameObject.SetActive(false);
            }
        }
    }
}
