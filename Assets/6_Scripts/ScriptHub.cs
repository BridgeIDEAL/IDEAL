using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using Yarn.Unity;

public class ScriptHub : MonoBehaviour
{
    [Header("Camera Property")]
    public Camera playerCamera;
    public GameObject playerCameraRootObject;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    [Header("Scene Change Object")]
    public GameObject uICanvasObject;
    public GameObject eventSystem;
    public GameObject playerObject;

    [Space(5)]
    [Header("FPC Property")]
    // public FirstPersonController firstPersonController;
    public GameObject playerArmatureObject;
    public ThirdPersonController thirdPersonController;
    public TempEffectSound playerEffectSound;
    public AmbienceSoundManager ambienceSoundManager;
    public PlayerHandLight playerHandLight;
    
    [Space(5)]
    [Header("UI Scripts")]
    public UIInteraction uIInteraction;
    public UIInventory uIInventory;
    public UIRayCaster uIRayCaster;
    public UIIngame uIIngame;
    public UIMap uIMap;
    public UIGuideBook uIGuideBook;
    public UIActivationLogManager uIActivationLogManager;
    public UICheckListManager uICheckListManager;
    public UIHealthPoint uIHealthPoint;
    public UIMoveSetting uIMoveSetting;
    public UIEquipment uIEquipment;
    public UIManager uIManager;

    [Space(5)]
    [Header("Scripts")]
    public InteractionManager interactionManager;
    public InteractionDetect interactionDetect;
    public ConversationManager conversationManager;
    public DialogueRunner dialogueRunner;

    public EyePenaltyManager eyePenaltyManager;

    public GameOverManager gameOverManager;
    

}
