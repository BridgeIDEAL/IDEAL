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

    [Space(5)]
    [Header("FPC Property")]
    // public FirstPersonController firstPersonController;
    public ThirdPersonController thirdPersonController;
    
    [Space(5)]
    [Header("UI Scripts")]
    public UIInteraction uIInteraction;
    public UIInventory uIInventory;
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
    public Inventory inventory;
    public ActivationLogManager activationLogManager;
    public ActivationLogData activationLogData;
    public HealthPointManager healthPointManager;
    public EquipmentManager equipmentManager;
    public ConversationManager conversationManager;
    public DialogueRunner dialogueRunner;

    public MentalPointManager mentalPointManager;
    

}
