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

    [SerializeField] private ScriptHub scriptHub;
    private UIHealthPoint uIHealthPoint;
    private FirstPersonController firstPersonController;
    private UIMoveSetting uIMoveSetting;
    private InteractionDetect interactionDetect;
    private UIIngame uIIngame;

    // 2 == 정상, 1 == 손상,  0 == 제거
    private int[] healthPoint = new int[System.Enum.GetValues(typeof(IdealBodyPart)).Length];

    public static int maxHP = 2;
    public static int minHP = 0;

    private float speedReduction = 0.15f;

    private float interactionReduction = 0.2f;
    private float visualReduction = 0.5f;   // 현재는 사용하지 않는 값
    
    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }

        uIHealthPoint = scriptHub.uIHealthPoint;
        firstPersonController = scriptHub.firstPersonController;
        uIMoveSetting = scriptHub.uIMoveSetting;
        interactionDetect = scriptHub.interactionDetect;
        uIIngame = scriptHub.uIIngame;
        Init();
    }

    public void Init(){
        for(int i = 0; i < healthPoint.Length; i++){
            healthPoint[i] = maxHP;
        }
    }
    private int GetHealthPoint(IdealBodyPart idealBodyPart){
        return healthPoint[(int)idealBodyPart];
    }

    public void Hurt(IdealBodyPart idealBodyPart, int damage){
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
        // damage는 음수 값
        int damage = healthPoint[(int)IdealBodyPart.LeftLeg] - maxHP + healthPoint[(int)IdealBodyPart.RightLeg] - maxHP;
        if(damage > 0){
            Debug.Log("UpdateLegCondition: damage > 0");
            damage = 0;
        }
        if(damage < -4) {
            Debug.Log("UpdateLegCondition: damage < -4");
            damage = -4;
        }

        firstPersonController.MoveSpeed = firstPersonController.DefaultMoveSpeed * (1.0f + damage * speedReduction);
        uIMoveSetting.UpdateMoveSpeedValueText();
        firstPersonController.SprintSpeed = firstPersonController.DefaultSprintSpeed * (1.0f + damage * speedReduction);
        uIMoveSetting.UpdateSprintSpeedValueText();
    }

    private void UpdateArmCondition(){
        // damage는 음수 값
        int damage = healthPoint[(int)IdealBodyPart.LeftArm] - maxHP + healthPoint[(int)IdealBodyPart.RightArm] - maxHP;
        if(damage > 0){
            Debug.Log("UpdateArmCondition: damage > 0");
            damage = 0;
        }
        if(damage < -4) {
            Debug.Log("UpdateArmCondition: damage < -4");
            damage = -4;
        }

        interactionDetect.requiredTimeRatio = 1.0f - damage * interactionReduction;

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
