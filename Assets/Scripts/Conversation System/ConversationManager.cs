using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using Yarn.Unity;

public class ConversationManager : MonoBehaviour
{
    [SerializeField] private ScriptHub scriptHub;
    private FirstPersonController firstPersonController;

    [SerializeField] private LineView lineView;


    [Space(8)]
    [SerializeField] private InteractionItemData teacherCenterKey;
    
    [SerializeField] private InteractionItemData roofTopKey;
    private int normalTypingSpeed = 20;
    private int acceleratedTypingSpeed = 40;
    
    public void Init(){
        firstPersonController = scriptHub.firstPersonController;
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
    [YarnCommand("AccelerateTypeSpeed")]
    public void AccelerateTypeSpeed(){
        // TO DO
        // LineView를 상속받는 클래스를 통해 typewriterEffectSpeed 값을 조정하도록 함
    }

    [YarnCommand("NormalTypeSpeed")]
    public void NormalTypeSpeed(){
        // TO DO
        // LineView를 상속받는 클래스를 통해 typewriterEffectSpeed 값을 조정하도록 함
    }
    #endregion

    #region Interaction
    // Yarn Spinner에서 매개변수 받는 것을 사이트와 동일하게 했지만 전혀 통하지 않음! 으악
    // 일단 노가다로 해결하고 이후 해결방법을 고려해보도록 하겠음
    
    [YarnCommand("GameOver")]
    public void GameOver(){
        int attempts = CountAttempts.Instance.GetAttemptCount();
        GameOverManager.Instance.GameOver($"{attempts}번 실종자는 ...");
    }
    
    [YarnCommand("GameOver_1")]
    public void GameOver_1(){
        int attempts = CountAttempts.Instance.GetAttemptCount();
        GameOverManager.Instance.GameOver($"이후 끌려간 {attempts}번 실종자는 학교 구석에서 온몸이 짓뭉개진 채 발견됨");
    }

    [YarnCommand("GameOver_2")]
    public void GameOver_2(){
        int attempts = CountAttempts.Instance.GetAttemptCount();
        GameOverManager.Instance.GameOver($"{attempts}번 실종자는 수위실에 들어간 후 통신이 끊김");
    }

    [YarnCommand("GameOver_3")]
    public void GameOver_3(){
        int attempts = CountAttempts.Instance.GetAttemptCount();
        GameOverManager.Instance.GameOver($"이후 수위 이형체가 {attempts}번 실종자를 끌고 간 후 통신이 끊김");
    }

    [YarnCommand("GameOver_4")]
    public void GameOver_4(){
        int attempts = CountAttempts.Instance.GetAttemptCount();
        GameOverManager.Instance.GameOver($"이후 {attempts}번 실종자와 통신이 끊김");
    }

    [YarnCommand("GameOver_5")]
    public void GameOver_5(){
        int attempts = CountAttempts.Instance.GetAttemptCount();
        GameOverManager.Instance.GameOver($"이후 학생회장은 {attempts}번 실종자로 ???를 만들어 학생회실 내부에 전시");
    }
    
    [YarnCommand("GameOver_6")]
    public void GameOver_6(){
        int attempts = CountAttempts.Instance.GetAttemptCount();
        GameOverManager.Instance.GameOver($"{attempts}번 실종자는 가입 이후 매우 즐거워하며 탈출을 포기. 이후 학교 내부에서 목격 증언이 다수 발생");
    }

    [YarnCommand("GameOver_7")]
    public void GameOver_7(){
        int attempts = CountAttempts.Instance.GetAttemptCount();
        GameOverManager.Instance.GameOver($"이후 {attempts}번 실종자는 수색 중 잠든 채로 발견. 현재 157시간 동안 눈을 뜨지 않음");
    }

    [YarnCommand("GameOver_Out")]
    public void GameOver_Out(){
        int attempts = CountAttempts.Instance.GetAttemptCount();
        GameOverManager.Instance.GameOver($"{attempts}번 실종자는 수색 중 학교 옥상에서 발견, 구출 성공");
    }

    [YarnCommand("ChalkBoardSuccess")]
    public void ChalkBoardSuccess(){
        // TO DO
        // 2층 자습실 성공 처리 필요
        Debug.Log("======== Success ChalkBoard");
    }

    [YarnCommand("GetAttemptsCount")]
    public int GetAttemptsCount(){
        return CountAttempts.Instance.GetAttemptCount();
    }
    #endregion

    #region HurtBodyPart
    [YarnCommand("HurtArm")]
    public void HurtArm(){
        if(HealthPointManager.Instance.GetHealthPoint(IdealBodyPart.LeftArm) > HealthPointManager.minHP){
            HealthPointManager.Instance.Hurt(IdealBodyPart.LeftArm, 1);
        }
        else{
            HealthPointManager.Instance.Hurt(IdealBodyPart.RightArm, 1);
        }
    }

    [YarnCommand("HealArm")]
    public void HealArm(){
        if(HealthPointManager.Instance.GetHealthPoint(IdealBodyPart.LeftArm) < HealthPointManager.maxHP){
            HealthPointManager.Instance.Heal(IdealBodyPart.LeftArm, 1);
        }
        else{
            HealthPointManager.Instance.Heal(IdealBodyPart.RightArm, 1);
        }
    }

    [YarnCommand("HealLeg")]
    public void HealLeg(){
        if(HealthPointManager.Instance.GetHealthPoint(IdealBodyPart.LeftLeg) < HealthPointManager.maxHP){
            HealthPointManager.Instance.Heal(IdealBodyPart.LeftLeg, 1);
        }
        else{
            HealthPointManager.Instance.Heal(IdealBodyPart.RightLeg, 1);
        }
    }

    [YarnCommand("HurtBothArm")]
    public void HurtBothArm(){
        HealthPointManager.Instance.Hurt(IdealBodyPart.LeftArm, 1);
        HealthPointManager.Instance.Hurt(IdealBodyPart.RightArm, 1);
    }

    [YarnCommand("HurtLeftArm")]
    public void HurtLeftArm(){
        HealthPointManager.Instance.Hurt(IdealBodyPart.LeftArm, 1);
    }


    [YarnCommand("HurtLeg")]
    public void HurtLeg(){
        if(HealthPointManager.Instance.GetHealthPoint(IdealBodyPart.LeftLeg) > HealthPointManager.minHP){
            HealthPointManager.Instance.Hurt(IdealBodyPart.LeftLeg, 1);
        }
        else{
            HealthPointManager.Instance.Hurt(IdealBodyPart.RightLeg, 1);
        }
    }

    [YarnCommand("HurtRightLeg")]
    public void HurtRightLeg(){
        HealthPointManager.Instance.Hurt(IdealBodyPart.RightLeg, 1);
    }

    [YarnCommand("HurtHead")]
    public void HurtHead(){
        HealthPointManager.Instance.Hurt(IdealBodyPart.Head, 1);
    }

    #endregion

    #region Activation Log
    [YarnCommand("HurtBothArmLog")]
    public void HurtBothArmLog(){
        ActivationLogManager.Instance.AddActivationLog(1002);
    }


    #endregion

    #region GetItem
    [YarnCommand("GetTeacherCenterKey")]
    public void GetTeacherCenterKey(){
        Inventory.Instance.Add(teacherCenterKey, 1);
    }
    
    [YarnCommand("GetRoofTopKey")]
    public void GetRoofTopKey(){
        Inventory.Instance.Add(roofTopKey, 1);
    }

    #endregion
}
