using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICheckListManager : MonoBehaviour
{
    [SerializeField] private GameObject uICheckListPrefab;

    [SerializeField] private RectTransform checkListArea;

    private List<UICheckList> uICheckLists = new List<UICheckList>();

    private List<int> checkListDicKeys;

    public void Init(){
        
        GenerateCheckListUI();
    }

    public void GenerateCheckListUI(){
        checkListDicKeys = ProgressManager.Instance.GetCheckListDicKeys();
        checkListDicKeys.Sort();

        foreach(int key in checkListDicKeys){
            GameObject checkListGameObject = Instantiate(uICheckListPrefab);
            RectTransform rt = checkListGameObject.GetComponent<RectTransform>();
            rt.SetParent(checkListArea);
            UICheckList uICheckList = checkListGameObject.GetComponent<UICheckList>();
            uICheckList.SetLogText(ProgressManager.Instance.checkListStr[key]);
            uICheckList.SetImageActive(ProgressManager.Instance.checkListDic[key] == -1);
            uICheckLists.Add(uICheckList);
        }
    }

    public void UpdateCheckListUI(){
        for(int i = 0; i < uICheckLists.Count; i++){
            uICheckLists[i].SetImageActive(ProgressManager.Instance.checkListDic[checkListDicKeys[i]] == -1);
        }
    }

}
