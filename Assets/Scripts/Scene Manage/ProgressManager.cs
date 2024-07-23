using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    private static ProgressManager instance = null;
    public static ProgressManager Instance{
        get{
            if(instance == null){ return null;}
            return instance;
        }
    }

    public static int floorNum = 6;
    public static int progressNum = 64;
    public int[,] progressState;   // [해당 진척이 발생하는 층, 해당 진척의 번호]  1층의 2번째 사건 [1, 2], [0]은 빈 공간으로 납둠
    // 기본 값 -1  해당 progress 완료 1  이외의 상태를 나타내는 경우를 위해 int 변수 사용

    public Dictionary<int, int> itemState = new Dictionary<int, int>();


    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
        progressState = new int[floorNum, progressNum];
        for(int i = 0; i < floorNum; i++){
            for(int j = 0; j < progressNum; j++){
                progressState[i, j] = -1;
            }
        }
    }

    public void UpdateProgressState(int floor, int progress, int state){
        progressState[floor, progress] = state;
        UpdateProgressObject();
    }

    private void UpdateProgressObject(){
        // 101
        if(progressState[1,1] >= 1){
            // door_01F_NurseRoom.canOpen = true;
        }
    }

    public void SetItemLog(int itemCode, int cnt){
        if(itemState.ContainsKey(itemCode)){
            // 이미 해당 아이템이 등록 된 경우 cnt 값을 갱신
            itemState[itemCode] = itemState[itemCode] + cnt;
        }
        else{
            // 해당 아이템이 없을 경우 새로 등록
            itemState.Add(itemCode, cnt);
        }
    }
    
    public bool GetItemLogExist(int itemCode){
        return itemState.ContainsKey(itemCode);
    }

    public int GetItemLog(int itemCode){
        if(itemState.ContainsKey(itemCode)){
            return itemState[itemCode];
        }
        else{
            return -1;
        }
    }
}
