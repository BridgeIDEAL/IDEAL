using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using Yarn.Unity;

public class ConversationManager : MonoBehaviour
{
    [SerializeField] private ScriptHub scriptHub;
    private FirstPersonController firstPersonController;
    private DialogueRunner dialogueRunner;

    [SerializeField] private LineView lineView;


    [Space(8)]
    [SerializeField] private InteractionItemData teacherCenterKey;
    
    [SerializeField] private InteractionItemData roofTopKey;
    private int normalTypingSpeed = 20;
    private int acceleratedTypingSpeed = 40;
    
    public void Init(){
        firstPersonController = scriptHub.firstPersonController;
        dialogueRunner = scriptHub.dialogueRunner;
        RegisterFunction();
    }
    public void VisibleMouseCursor(){
        firstPersonController.CameraRotationLock = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void InvisibleMouseCursor(){
        firstPersonController.CameraRotationLock = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    #region Typing Effect
    public void AccelerateTypeSpeed(){
        // TO DO
        // LineView를 상속받는 클래스를 통해 typewriterEffectSpeed 값을 조정하도록 함
    }

    public void NormalTypeSpeed(){
        // TO DO
        // LineView를 상속받는 클래스를 통해 typewriterEffectSpeed 값을 조정하도록 함
    }
    #endregion

    #region Interaction
    // Yarn Spinner에서 매개변수 받는 방법을 전해받음
    private void RegisterFunction(){
        dialogueRunner.AddCommandHandler<string,int>("GameOver", GameOver);
        dialogueRunner.AddCommandHandler("GameOver_Out", GameOver_Out);

        dialogueRunner.AddCommandHandler("ChalkBoardSuccess", ChalkBoardSuccess);
        dialogueRunner.AddCommandHandler("GetTeacherCenterKey", GetTeacherCenterKey);
        dialogueRunner.AddCommandHandler("GetRoofTopKey", GetRoofTopKey);

        dialogueRunner.AddCommandHandler("AccelerateTypeSpeed", AccelerateTypeSpeed);
        dialogueRunner.AddCommandHandler("NormalTypeSpeed", NormalTypeSpeed);

        dialogueRunner.AddCommandHandler("HurtLeftArm", HurtLeftArm);
        dialogueRunner.AddCommandHandler("HurtRightLeg", HurtRightLeg);
        dialogueRunner.AddCommandHandler("HurtHead", HurtHead);
        dialogueRunner.AddCommandHandler("HurtArm", HurtArm);
        dialogueRunner.AddCommandHandler("HurtBothArm", HurtBothArm);
        dialogueRunner.AddCommandHandler("HurtLeg", HurtLeg);
        dialogueRunner.AddCommandHandler("HurtBothArmLog", HurtBothArmLog);
        

        dialogueRunner.AddCommandHandler("HealArm", HealArm);
        dialogueRunner.AddCommandHandler("HealLeg", HealLeg);
    }

    public void GameOver(string str, int guideLogID = -1){
        int attempts = CountAttempts.Instance.GetAttemptCount();
        if(str.Contains("$attempts")){
            str = str.Replace("$attempts", attempts.ToString());
        }
        if(guideLogID > -1){
            GuideLogManager.Instance.UpdateGuideLogRecord(guideLogID, attempts);
        }
        GameOverManager.Instance.GameOver(str);
        
    }
    
    public void GameOver_Out(){
        int attempts = CountAttempts.Instance.GetAttemptCount();
        GameOverManager.Instance.GameOver($"{attempts}번 실종자는 수색 중 학교 옥상에서 발견, 구출 성공");
    }


    public void ChalkBoardSuccess(){
        // TO DO
        // 2층 자습실 성공 처리 필요
        Debug.Log("======== Success ChalkBoard");
        ActivationLogManager.Instance.AddActivationLog(4105);
    }

    #endregion

    #region HurtBodyPart
    public void HurtArm(){
        if(HealthPointManager.Instance.GetHealthPoint(IdealBodyPart.LeftArm) > HealthPointManager.minHP){
            HealthPointManager.Instance.Hurt(IdealBodyPart.LeftArm, 1);
        }
        else{
            HealthPointManager.Instance.Hurt(IdealBodyPart.RightArm, 1);
        }
    }

    public void HealArm(){
        if(HealthPointManager.Instance.GetHealthPoint(IdealBodyPart.LeftArm) < HealthPointManager.maxHP){
            HealthPointManager.Instance.Heal(IdealBodyPart.LeftArm, 1);
        }
        else{
            HealthPointManager.Instance.Heal(IdealBodyPart.RightArm, 1);
        }
    }

    public void HealLeg(){
        if(HealthPointManager.Instance.GetHealthPoint(IdealBodyPart.LeftLeg) < HealthPointManager.maxHP){
            HealthPointManager.Instance.Heal(IdealBodyPart.LeftLeg, 1);
        }
        else{
            HealthPointManager.Instance.Heal(IdealBodyPart.RightLeg, 1);
        }
    }


    public void HurtBothArm(){
        HealthPointManager.Instance.Hurt(IdealBodyPart.LeftArm, 1);
        HealthPointManager.Instance.Hurt(IdealBodyPart.RightArm, 1);
    }

    
    public void HurtLeftArm(){
        HealthPointManager.Instance.Hurt(IdealBodyPart.LeftArm, 1);
    }



    public void HurtLeg(){
        if(HealthPointManager.Instance.GetHealthPoint(IdealBodyPart.LeftLeg) > HealthPointManager.minHP){
            HealthPointManager.Instance.Hurt(IdealBodyPart.LeftLeg, 1);
        }
        else{
            HealthPointManager.Instance.Hurt(IdealBodyPart.RightLeg, 1);
        }
    }


    public void HurtRightLeg(){
        HealthPointManager.Instance.Hurt(IdealBodyPart.RightLeg, 1);
    }


    public void HurtHead(){
        HealthPointManager.Instance.Hurt(IdealBodyPart.Head, 1);
    }

    #endregion

    #region Activation Log
    public void HurtBothArmLog(){
        ActivationLogManager.Instance.AddActivationLog(1002);
    }


    #endregion

    #region GetItem
    public void GetTeacherCenterKey(){
        Inventory.Instance.Add(teacherCenterKey, 1);
        ActivationLogManager.Instance.AddActivationLog(4101);
    }
    
    public void GetRoofTopKey(){
        Inventory.Instance.Add(roofTopKey, 1);
        ActivationLogManager.Instance.AddActivationLog(4107);
    }

    #endregion
}
