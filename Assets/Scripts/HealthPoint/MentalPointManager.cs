using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MentalPenalty{
    NoSpace,
    RandomSpace,
    ReverseString,
    ReverseWord,
}

public class MentalPointManager : MonoBehaviour
{
    private static MentalPointManager instance = null;
    public static MentalPointManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    public ScriptHub scriptHub;
    private UIActivationLogManager uIActivationLogManager;

    private int mentalPoint;

    public int MentalPoint{
        get {return mentalPoint;}
    }

    public static int maxMP = 5;
    public static int minMP = 0;

    public void Init(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }

        mentalPoint = maxMP;
    }

    public void EnterAnotherSceneInit(bool isLobby){
        if(isLobby){
            mentalPoint = maxMP;
        }
        else{
            uIActivationLogManager = scriptHub.uIActivationLogManager;
        }
    }

    public void Hurt(int damage){
        if(damage < 0) return;
        mentalPoint -= damage;
        if(mentalPoint < minMP) mentalPoint = minMP;
        UpdateMentalCondition();
    }

    public void Heal(int repair){
        if(repair < 0) return;
        mentalPoint += repair;
        if(mentalPoint > maxMP) mentalPoint = maxMP;
        UpdateMentalCondition();
    }

    // TO DO 테스트 이후에 아래 삭제 필
    void Update(){
        // if(Input.GetKeyDown(KeyCode.Alpha1)){
        //     ApplyMentalPenalty(MentalPenalty.NoSpace, !uIActivationLogManager.noSpace);
        // }
        // if(Input.GetKeyDown(KeyCode.Alpha2)){
        //     ApplyMentalPenalty(MentalPenalty.RandomSpace, !uIActivationLogManager.randomSpace);
        // }
        // if(Input.GetKeyDown(KeyCode.Alpha3)){
        //     ApplyMentalPenalty(MentalPenalty.ReverseString, !uIActivationLogManager.reverseString);
        // }
        // if(Input.GetKeyDown(KeyCode.Alpha4)){
        //     ApplyMentalPenalty(MentalPenalty.ReverseWord, !uIActivationLogManager.reverseWord);
        // }

        // if(Input.GetKeyDown(KeyCode.Alpha0)){
        //     ActivationLogManager.Instance.AddActivationLog(5);
        // }
    }

    private void UpdateMentalCondition(){
        if(mentalPoint < maxMP){
            ApplyMentalPenalty(MentalPenalty.NoSpace, true);
        }
        if(mentalPoint < maxMP - 1){
            ApplyMentalPenalty(MentalPenalty.RandomSpace, true);
        }
        if(mentalPoint < maxMP - 2){
            ApplyMentalPenalty(MentalPenalty.NoSpace, false);
            ApplyMentalPenalty(MentalPenalty.ReverseWord, true);
        }
        if(mentalPoint < maxMP - 3){
            ApplyMentalPenalty(MentalPenalty.NoSpace, false);
            ApplyMentalPenalty(MentalPenalty.ReverseString, true);
        }

    }

    private void ApplyMentalPenalty(MentalPenalty mentalPenalty, bool active){
        if(mentalPenalty >= MentalPenalty.NoSpace && mentalPenalty <= MentalPenalty.ReverseWord){
            uIActivationLogManager.ApplyMentalPenaltyActivationLog(mentalPenalty, active);
        }
    }
}
