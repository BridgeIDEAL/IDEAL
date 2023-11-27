using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ScriptHub : MonoBehaviour
{
    [Header("Camera Property")]
    public Camera playerCamera;
    public CinemachineVirtualCamera cinemachineVirtualCamera;

    [Space(5)]
    [Header("FPC Property")]
    public FirstPersonController firstPersonController;
    
    [Space(5)]
    [Header("UI Scripts")]
    public UIInteraction uIInteraction;
    public UIInventory uIInventory;
    public UIIngame uIIngame;
    public UIActivationLogManager uIActivationLogManager;
    public UIHealthPoint uIHealthPoint;
    public UIMoveSetting uIMoveSetting;
    public UIEquipment uIEquipment;

    [Space(5)]
    [Header("Scripts")]
    public ActivationLogData activationLogData;
    public InteractionDetect interactionDetect;
    

}
