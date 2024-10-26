using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMap : MonoBehaviour
{
    [SerializeField] private GameObject[] mapUIObjects;
    [SerializeField] private GameObject changeMapButtonObjectLeft;
    [SerializeField] private GameObject changeMapButtonObjectRight;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private RectTransform pointRectTransform;

    [SerializeField] private UIMapCheckListGroup[] uIMapCheckListGroups;
    private int playerFloorNum = 1;
    private float[] playerFloorDivide = {6.18f, 9.67f, 13.18f, 16.68f};
    private float[] boundX_Prototype = {-4.73304f, 62.39957f};
    private float[] boundZ_Prototype = {-0.181922f, 36.48202f};
    private float[] boundX_Prototype_Second = {-17.76274f, -56.733f};
    private float[] boundZ_Prototype_Second = {7.521711f, 76.69599f};

    private float[] mapBoundX_Prototype = {-91.2f, 391.5f};
    private float[] mapBoundY_Prototype = {-288.8f, 30.5f};
    private float[] mapBoundX_Prototype_Second = {-127.3f, -395.6f};
    private float[] mapBoundY_Prototype_Second = {-217f, 377.9f};

    private int mapItemCode = 990;
    private int pieceMapItemCode =  99001;

    [SerializeField] private bool cheatMapGet = false;

    void Update(){
        UpdatePlayerFloor();
        
        if(ProgressManager.Instance.watchMapNum == playerFloorNum -1){
            pointRectTransform.gameObject.SetActive(true);
            UpdatePlayerPoint();
        }
        else{
            pointRectTransform.gameObject.SetActive(false);
        }
        
    }

    void Start(){
        UpdateAllObjects();
        UpdateMapFloor();
    }

    private void UpdatePlayerFloor(){
        for(int i = 0 ; i < playerFloorDivide.Length; i++){
            playerFloorNum = i + 1;
            if(playerTransform.localPosition.y < playerFloorDivide[i]){
                break;
            }
        }
    }

    private void UpdatePlayerPoint(){
        float playerXTransformNomalized = -1;
        float playerZTransformNomalized = -1;

        float cameraYRotation = 0.0f;

        cameraYRotation = cameraTransform.localEulerAngles.y;

        if(IdealSceneManager.Instance.GetSceneName() == "Prototype"){
            playerXTransformNomalized = (playerTransform.localPosition.x - boundX_Prototype[0]) / (boundX_Prototype[1] - boundX_Prototype[0]);
            playerZTransformNomalized = (playerTransform.localPosition.z - boundZ_Prototype[0]) / (boundZ_Prototype[1] - boundZ_Prototype[0]);
        }
        else if(IdealSceneManager.Instance.GetSceneName() == "Prototype_Second"){
            playerXTransformNomalized = (playerTransform.localPosition.x - boundX_Prototype_Second[0]) / (boundX_Prototype_Second[1] - boundX_Prototype_Second[0]);
            playerZTransformNomalized = (playerTransform.localPosition.z - boundZ_Prototype_Second[0]) / (boundZ_Prototype_Second[1] - boundZ_Prototype_Second[0]);
        }
        float mapPointX = -1, mapPointY = -1;
        if(IdealSceneManager.Instance.GetSceneName() == "Prototype"){
            mapPointX = mapBoundX_Prototype[0] + playerXTransformNomalized * (mapBoundX_Prototype[1] - mapBoundX_Prototype[0]);
            mapPointY = mapBoundY_Prototype[0] + playerZTransformNomalized * (mapBoundY_Prototype[1] - mapBoundY_Prototype[0]);
        }
        else if(IdealSceneManager.Instance.GetSceneName() == "Prototype_Second"){
            mapPointX = mapBoundX_Prototype_Second[0] + playerXTransformNomalized * (mapBoundX_Prototype_Second[1] - mapBoundX_Prototype_Second[0]);
            mapPointY = mapBoundY_Prototype_Second[0] + playerZTransformNomalized * (mapBoundY_Prototype_Second[1] - mapBoundY_Prototype_Second[0]);
        }
        
        pointRectTransform.localPosition = new Vector3(mapPointX, mapPointY, 0);
        pointRectTransform.localRotation = Quaternion.Euler(0, 0, -1.0f * cameraYRotation);
    }

    public void ActiveMap(){
        // 테스트 코드
        if(IdealSceneManager.Instance.GetSceneName() == "Prototype"){
            ActiveInteraction.Instance.Active_01F_MapGuide(false);
        }
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

    public void UpdateMapFloor(){
        while(UpdateCheckListGroup(ProgressManager.Instance.GetMapCheckListStateNum())){
            ProgressManager.Instance.AddMapCheckListStateNum();
        }
    }

    private bool UpdateCheckListGroup(int stateNum){
        if(stateNum >= uIMapCheckListGroups.Length){
            return false;
        }
        
        bool checkListDone = true;
        foreach(UIMapObject uIMapObject in uIMapCheckListGroups[stateNum].uIMapObjects){
            if(ProgressManager.Instance.checkListDic.ContainsKey(uIMapObject.checkListNum)){
                if(ProgressManager.Instance.checkListDic[uIMapObject.checkListNum] == 1){
                    uIMapObject.gameObject.SetActive(false);
                }
                else{
                    uIMapObject.gameObject.SetActive(true);
                    checkListDone = false;
                }
            }
            else{
                Debug.LogError("Contains Key Error!");
            }
        }
        return checkListDone;
    }

    private void UpdateAllObjects(){
        for(int i = 0; i < uIMapCheckListGroups.Length; i++){
            foreach(UIMapObject uIMapObject in uIMapCheckListGroups[i].uIMapObjects){
                uIMapObject.gameObject.SetActive(false);
            }
        }
    }
}
