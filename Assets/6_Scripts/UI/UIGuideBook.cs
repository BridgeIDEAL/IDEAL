using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGuideBook : MonoBehaviour
{
    [SerializeField] private GameObject[] guideUIObjects;
    [SerializeField] private GameObject setGuideUIObject;
    [SerializeField] private bool watchSetGuideUI = false;
    [SerializeField] private GameObject changeGuideUIButtonObjectLeft;
    [SerializeField] private GameObject changeGuideUIButtonObjectRight;
    [SerializeField] private RectTransform closeGuideUIRectTransform;

    private int pageNum = 0;


    public void ActiveGuide(){
        // 테스트 코드
        for(int i = 0; i < guideUIObjects.Length; i++){
            guideUIObjects[i].SetActive(false);
        }
        setGuideUIObject.SetActive(false);
        changeGuideUIButtonObjectLeft.SetActive(false);
        changeGuideUIButtonObjectRight.SetActive(false);
    
        if(watchSetGuideUI){
            setGuideUIObject.SetActive(true);
            closeGuideUIRectTransform.anchoredPosition = new Vector2(763.2f, -398.9f);
        }
        else{
            guideUIObjects[pageNum].SetActive(true);
            changeGuideUIButtonObjectLeft.SetActive(true);
            changeGuideUIButtonObjectRight.SetActive(true);
            if(pageNum == 0){
                changeGuideUIButtonObjectLeft.SetActive(false);
            }
            if(pageNum == guideUIObjects.Length -1){
                changeGuideUIButtonObjectRight.SetActive(false);
            }
            closeGuideUIRectTransform.anchoredPosition = new Vector2(573.3f, -454);
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
        changeGuideUIButtonObjectLeft.SetActive(true);
        changeGuideUIButtonObjectRight.SetActive(true);
        if(pageNum == 0){
            changeGuideUIButtonObjectLeft.SetActive(false);
        }
        if(pageNum == guideUIObjects.Length -1){
            changeGuideUIButtonObjectRight.SetActive(false);
        }
    }
}
