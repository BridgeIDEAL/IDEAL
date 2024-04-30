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
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    [Header("Scene Change Object")]
    public GameObject uICanvasObject;
    public GameObject eventSystem;
    public GameObject playerObject;

    [Space(5)]
    [Header("FPC Property")]
    // public FirstPersonController firstPersonController;
    public ThirdPersonController thirdPersonController;
    public TempEffectSound playerEffectSound;
    public AmbienceSoundManager ambienceSoundManager;
    
    [Space(5)]
    [Header("UI Scripts")]
    public UIInteraction uIInteraction;
    public UIInventory uIInventory;
    public UIRayCaster uIRayCaster;
    public UIIngame uIIngame;
    public UIActivationLogManager uIActivationLogManager;
    public UIHealthPoint uIHealthPoint;
    public UIMoveSetting uIMoveSetting;
    public UIEquipment uIEquipment;
    public UIManager uIManager;

    [Space(5)]
    [Header("Scripts")]
    public InteractionManager interactionManager;
    public InteractionDetect interactionDetect;
    public EquipmentManager equipmentManager;
    public ConversationManager conversationManager;
    public DialogueRunner dialogueRunner;
    

}
