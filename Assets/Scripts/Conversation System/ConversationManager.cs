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

    [YarnCommand("GameOver")]
    public void GameOver(){
        // TO DO
        // 게임 오버 처리 필요
        Debug.Log("======== Game Over ========");
    }

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
