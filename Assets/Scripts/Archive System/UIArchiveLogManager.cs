using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIArchiveLogManager : MonoBehaviour
{
    [SerializeField] private GameObject archivePrefab;
    [SerializeField] private RectTransform archiveLogArea;
    private List<ArchiveLog> archiveLogList;

    public void LoadArchiveLog(){
        int childCount = archiveLogArea.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--){
            Transform child = archiveLogArea.transform.GetChild(i);
            Destroy(child.gameObject);
        }
        
        archiveLogList = ArchiveLogManager.Instance.playerArchiveData.archiveLogRecordList;

        for(int i = 0; i < archiveLogList.Count; i++){
            GameObject logGameObject = Instantiate(archivePrefab);
            RectTransform rt = logGameObject.GetComponent<RectTransform>();
            rt.SetParent(archiveLogArea);
            UIArchiveLog uIArchiveLog = logGameObject.GetComponent<UIArchiveLog>();
            uIArchiveLog.SetNameTag(archiveLogList[i].GetAttempt(), archiveLogList[i].GetState());
            uIArchiveLog.SetArchiveText(archiveLogList[i].GetArchiveText());
        }
    }
}
