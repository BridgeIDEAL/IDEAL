using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGuideBook : MonoBehaviour
{
    [SerializeField] private GameObject[] guideUIObjects;
    [SerializeField] private GameObject changeMapButtonObjectLeft;
    [SerializeField] private GameObject changeMapButtonObjectRight;

    private int pageNum = 0;


    public void ActiveGuide(){
        // 테스트 코드
        for(int i = 1; i < guideUIObjects.Length; i++){
            guideUIObjects[i].SetActive(false);
        }
        guideUIObjects[pageNum].SetActive(true);
        changeMapButtonObjectLeft.SetActive(true);
        changeMapButtonObjectRight.SetActive(true);
        if(pageNum == 0){
            changeMapButtonObjectLeft.SetActive(false);
        }
        if(pageNum == guideUIObjects.Length -1){
            changeMapButtonObjectRight.SetActive(false);
        }
    }

    public void ChangeGuideUI(bool left){
        if(left){
            pageNum--;
        }
        else{
            pageNum++;
        }
        for(int i = 1; i < guideUIObjects.Length; i++){
            guideUIObjects[i].SetActive(false);
        }
        guideUIObjects[pageNum].SetActive(true);
        changeMapButtonObjectLeft.SetActive(true);
        changeMapButtonObjectRight.SetActive(true);
        if(pageNum == 0){
            changeMapButtonObjectLeft.SetActive(false);
        }
        if(pageNum == guideUIObjects.Length -1){
            changeMapButtonObjectRight.SetActive(false);
        }
    }
}
