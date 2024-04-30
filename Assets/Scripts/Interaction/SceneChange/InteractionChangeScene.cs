using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChangeScene : AbstractInteraction
{
    [SerializeField] private AudioClip unlockDoorAudio;
    [SerializeField] private AudioClip lockDoorAudio;
    
    [SerializeField] GameObject doorObject;
    [SerializeField] private string currentSceneName;
    [SerializeField] private string destSceneName;
    [SerializeField] private string detectedStr;
    [SerializeField] private string successInteractionStr = "";
    [SerializeField] private string failInteractionStr = "";
    
    public override float RequiredTime { get => 1.0f;}

    protected override string GetDetectedString(){
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        if(audioSource != null){
            audioSource.clip = unlockDoorAudio;
            audioSource.Play();
        }
        IdealSceneManager.Instance.ChangeAnotherGameScene(currentSceneName, destSceneName);
        if(successInteractionStr != ""){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(successInteractionStr);
        }
    }
}
