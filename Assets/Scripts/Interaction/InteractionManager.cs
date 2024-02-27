using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    private static InteractionManager instance = null;

    public static InteractionManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    public UIInteraction uIInteraction;

    public int[,] progressState;   // [해당 진척이 발생하는 층, 해당 진척의 번호]  1층의 2번째 사건 [1, 2], [0]은 빈 공간으로 납둠
    // 기본 값 -1  해당 progress 완료 1  이외의 상태를 나타내는 경우를 위해 int 변수 사용

    public static int floorNum = 6;
    public static int progressNum = 64;

    [Header("Test Scene Object")]
    [SerializeField] private GameObject timerStartObject;

    [SerializeField] private GameObject timerEndObject;

    
    [Header("1st Floor Object")]
    [SerializeField] private GameObject medicine_01F;
    [SerializeField] private InteractionConditionalDoor door_01F_NurseRoom;


    public void Init(){
        if(instance == null){
            instance = this;
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
    
    
    public void SetTimerStartActive(bool active){
        timerStartObject.SetActive(active);
    }

    public void SetTimerEndActive(bool active){
        timerEndObject.SetActive(active);
    }

    public void Active_01F_Medicine(){
        medicine_01F.SetActive(true);
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
}
