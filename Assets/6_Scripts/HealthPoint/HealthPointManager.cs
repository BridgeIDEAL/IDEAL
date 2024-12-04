using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public enum IdealBodyPart{
    Head = 0,
    Torso,
    LeftArm,
    RightArm,
    LeftLeg,
    RightLeg
}

public class HealthPointManager : MonoBehaviour
{
    private static HealthPointManager instance = null;
    public static HealthPointManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    public ScriptHub scriptHub;
    private UIHealthPoint uIHealthPoint;
    // private FirstPersonController firstPersonController;
    private ThirdPersonController thirdPersonController;
    private UIMoveSetting uIMoveSetting;
    private InteractionDetect interactionDetect;
    private UIIngame uIIngame;

    // 2 == 정상, 1 == 손상,  0 == 제거
    private int[] healthPoint = new int[System.Enum.GetValues(typeof(IdealBodyPart)).Length];

    public static int maxHP = 2;
    public static int minHP = 0;

    public bool chased = false;


    
    public void Init(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }

        InitHealthPoint();
    }

    public void EnterAnotherSceneInit(bool isLobby){
        if(isLobby){
            InitHealthPoint();
        }
        else{
            uIHealthPoint = scriptHub.uIHealthPoint;
            thirdPersonController = scriptHub.thirdPersonController;
            uIMoveSetting = scriptHub.uIMoveSetting;
            interactionDetect = scriptHub.interactionDetect;
            uIIngame = scriptHub.uIIngame;
        }
    }

    public void InitHealthPoint(){
        for(int i = 0; i < healthPoint.Length; i++){
            healthPoint[i] = maxHP;
        }
    }
    public int GetHealthPoint(IdealBodyPart idealBodyPart){
        return healthPoint[(int)idealBodyPart];
    }

    public bool NoDamage(){
        bool nodamage = true;
        for(int i = 0; i < healthPoint.Length; i++){
            if(healthPoint[i] != maxHP){
                nodamage = false;
            }
        }
        return nodamage;
    }

    public void Hurt(IdealBodyPart idealBodyPart, int damage){
        if(damage > 0) uIIngame.HurtEffect();
        int bodyNum = (int)idealBodyPart;
        healthPoint[bodyNum] -= damage;
        if(healthPoint[bodyNum] < minHP) healthPoint[bodyNum] = minHP;
        UpdateHealthCondition(idealBodyPart, healthPoint[bodyNum]);
    }

    public bool Heal(IdealBodyPart idealBodyPart, int repair){
        int bodyNum = (int)idealBodyPart;
        if(healthPoint[bodyNum] > minHP){
            healthPoint[bodyNum] += repair;
            if(healthPoint[bodyNum] > maxHP) healthPoint[bodyNum] = maxHP;
            UpdateHealthCondition(idealBodyPart, healthPoint[bodyNum]);
            return true;
        }
        else{
            UpdateHealthCondition(idealBodyPart, healthPoint[bodyNum]);
            return false;
        }
    }

    private void UpdateHealthCondition(IdealBodyPart idealBodyPart, int hp){
        uIHealthPoint.UpdateBodyImage(idealBodyPart, hp);
        UpdateHealthEffect(idealBodyPart, hp);
    }

    public void UpdateAllHealthCondition(){
        UpdateHeadCondition();
        UpdateArmCondition();
        UpdateLegCondition();
    }

    private void UpdateHealthEffect(IdealBodyPart idealBodyPart, int hp){
        switch(idealBodyPart){
            case IdealBodyPart.Head:
                // TO DO
                // 머리의 체력에 따라 나타나는 현상
                Debug.Log($"Head HP :{hp}");
                UpdateHeadCondition();
                break;
            case IdealBodyPart.Torso:
                // TO DO
                // 몸통의 체력에 따라 나타나는 현상
                Debug.Log($"Torso HP :{hp}");
                break;
            case IdealBodyPart.LeftArm:
                // TO DO
                // 왼쪽 팔의 체력에 따라 나타나는 현상
                Debug.Log($"LeftArm HP :{hp}");
                UpdateArmCondition();
                break;
            case IdealBodyPart.RightArm:
                // TO DO
                // 오른쪽 팔의 체력에 따라 나타나는 현상
                Debug.Log($"RightArm HP :{hp}");
                UpdateArmCondition();
                break;
            case IdealBodyPart.LeftLeg:
                // TO DO
                // 왼쪽 다리의 체력에 따라 나타나는 현상
                Debug.Log($"LeftLeg HP :{hp}");
                UpdateLegCondition();
                break;
            case IdealBodyPart.RightLeg:
                // TO DO
                // 오른쪽 다리의 체력에 따라 나타나는 현상
                Debug.Log($"RightLeg HP :{hp}");
                UpdateLegCondition();
                break;
            default:
                break;
        }
    }

    private void UpdateLegCondition(){
        float speedReduction = 0.0f;
        if(maxHP - healthPoint[(int)IdealBodyPart.LeftLeg] == 1){
            speedReduction += 0.2f;
        }
        else if(maxHP - healthPoint[(int)IdealBodyPart.LeftLeg] == 2){
            speedReduction += 0.5f;
        }
        if(maxHP - healthPoint[(int)IdealBodyPart.RightLeg] == 1){
            speedReduction += 0.2f;
        }
        else if(maxHP - healthPoint[(int)IdealBodyPart.RightLeg] == 2){
            speedReduction += 0.5f;
        }

        thirdPersonController.MoveSpeed = thirdPersonController.DefaultMoveSpeed * (1.0f - speedReduction) * (chased ? 1.25f : 1.0f) * (PenaltyPointManager.Instance.isSoundHearing ? 1.25f : 1.0f);
        uIMoveSetting.UpdateMoveSpeedValueText();
        thirdPersonController.SprintSpeed = thirdPersonController.DefaultSprintSpeed * (1.0f - speedReduction)* (chased ? 1.25f : 1.0f);
        uIMoveSetting.UpdateSprintSpeedValueText();
    }

    private void UpdateArmCondition(){
        float interactionReduction = 0.0f;
        if(maxHP - healthPoint[(int)IdealBodyPart.LeftArm] == 1){
            interactionReduction += 0.2f;
        }
        else if(maxHP - healthPoint[(int)IdealBodyPart.LeftArm] == 2){
            interactionReduction += 0.5f;
        }

        if(maxHP - healthPoint[(int)IdealBodyPart.RightArm] == 1){
            interactionReduction += 0.2f;
        }
        else if(maxHP - healthPoint[(int)IdealBodyPart.RightArm] == 2){
            interactionReduction += 0.5f;
        }

        interactionDetect.requiredTimeRatio = 1.0f + interactionReduction;

        // 팔 손상으로 인한 장비 슬롯 비활성화
        if(healthPoint[(int)IdealBodyPart.LeftArm] <= minHP){
            EquipmentManager.Instance.SetHandActive(true, false);
        }
        else{
            EquipmentManager.Instance.SetHandActive(true, true);
        }

        if(healthPoint[(int)IdealBodyPart.RightArm] <= minHP){
            EquipmentManager.Instance.SetHandActive(false, false);
        }
        else{
            EquipmentManager.Instance.SetHandActive(false, true);
        }
    }

    private void UpdateHeadCondition(){
        // damage는 음수 값
        int damage = healthPoint[(int)IdealBodyPart.Head] - maxHP;
        if(damage > 0){
            Debug.Log("UpdateArmCondition: damage > 0");
            damage = 0;
        }
        if(damage < -2) {
            Debug.Log("UpdateArmCondition: damage < -4");
            damage = -2;
        }

        if(damage == 0){
            uIIngame.SetVisualFilter(0);
        }
        else if(damage == -1){
            uIIngame.SetVisualFilter(0.6f);
        }
        else{
            uIIngame.SetVisualFilter(0.8f);
        }

        
    }
}
