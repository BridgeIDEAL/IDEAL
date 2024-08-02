using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMap : MonoBehaviour
{
    [SerializeField] private GameObject[] mapUIObjects;
    [SerializeField] private GameObject changeMapButtonObjectLeft;
    [SerializeField] private GameObject changeMapButtonObjectRight;
    private int mapItemCode = 990;
    private int pieceMapItemCode =  99001;

    [SerializeField] private bool cheatMapGet = false;

    public void ActiveMap(){
        // 테스트 코드
        if(cheatMapGet){
            int watchMapN = ProgressManager.Instance.watchMapNum;
            for(int i = 1; i < mapUIObjects.Length; i++){
                mapUIObjects[i].SetActive(false);
            }
            mapUIObjects[watchMapN].SetActive(true);
            changeMapButtonObjectLeft.SetActive(true);
            changeMapButtonObjectRight.SetActive(true);
            if(watchMapN == 0){
                changeMapButtonObjectLeft.SetActive(false);
            }
            if(watchMapN == mapUIObjects.Length -1){
                changeMapButtonObjectRight.SetActive(false);
            }
            return;
        }
        
        
        
        if(Inventory.Instance.FindItemIndex(mapItemCode) == -1 && Inventory.Instance.FindItemIndex(pieceMapItemCode) == -1){
            // 두 아이템 모두 없는 경우
            for(int i = 0; i < mapUIObjects.Length; i++){
                mapUIObjects[i].SetActive(false);
            }
            changeMapButtonObjectLeft.SetActive(false);
            changeMapButtonObjectRight.SetActive(false);
        }
        else if(Inventory.Instance.FindItemIndex(mapItemCode) == -1 && Inventory.Instance.FindItemIndex(pieceMapItemCode) != -1){
            // 조각 아이템만 있는 경우
            mapUIObjects[0].SetActive(true);
            for(int i = 1; i < mapUIObjects.Length; i++){
                mapUIObjects[i].SetActive(false);
            }
            changeMapButtonObjectLeft.SetActive(false);
            changeMapButtonObjectRight.SetActive(false);
        }
        else if(Inventory.Instance.FindItemIndex(mapItemCode) != -1){
            // 전체 지도 아이템이 있는 경우
            ProgressManager.Instance.UpdateCheckList(202, 1);
            int watchMapN = ProgressManager.Instance.watchMapNum;
            for(int i = 1; i < mapUIObjects.Length; i++){
                mapUIObjects[i].SetActive(false);
            }
            mapUIObjects[watchMapN].SetActive(true);
            changeMapButtonObjectLeft.SetActive(true);
            changeMapButtonObjectRight.SetActive(true);
            if(watchMapN == 0){
                changeMapButtonObjectLeft.SetActive(false);
            }
            if(watchMapN == mapUIObjects.Length -1){
                changeMapButtonObjectRight.SetActive(false);
            }
        }
    }

    public void ChangeMapUI(bool left){
        int watchMapN = ProgressManager.Instance.watchMapNum;
        if(left){
            watchMapN--;
        }
        else{
            watchMapN++;
        }
        for(int i = 1; i < mapUIObjects.Length; i++){
            mapUIObjects[i].SetActive(false);
        }
        mapUIObjects[watchMapN].SetActive(true);
        changeMapButtonObjectLeft.SetActive(true);
        changeMapButtonObjectRight.SetActive(true);
        if(watchMapN == 0){
            changeMapButtonObjectLeft.SetActive(false);
        }
        if(watchMapN == mapUIObjects.Length -1){
            changeMapButtonObjectRight.SetActive(false);
        }
        ProgressManager.Instance.watchMapNum = watchMapN;
    }
}
