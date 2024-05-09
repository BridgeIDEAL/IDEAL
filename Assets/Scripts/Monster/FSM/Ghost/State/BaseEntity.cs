using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseEntity : MonoBehaviour
{

    #region Variable
    [Header("Set Name/Dialogue/Spawn Data")]
    [SerializeField] protected ScriptableEntity entityData;
    #endregion

    #region Init Setting
    public virtual void Setup(Transform _playerTransform)
    {
      
        //InteractionConversation interactionConversation = GetComponent<InteractionConversation>();
        //if (interactionConversation != null)
        //{
        //    interactionConversation.dialogueRunner = IdealSceneManager.Instance.CurrentGameManager.scriptHub.dialogueRunner;
        //    interactionConversation.conversationManager = IdealSceneManager.Instance.CurrentGameManager.scriptHub.conversationManager;
        //}
        //else
        //{
        //    InteractionNurse interactionNurese = GetComponent<InteractionNurse>();
        //    interactionNurese.dialogueRunner = IdealSceneManager.Instance.CurrentGameManager.scriptHub.dialogueRunner;
        //    interactionNurese.conversationManager = IdealSceneManager.Instance.CurrentGameManager.scriptHub.conversationManager;
        //}
        AdditionalSetup();
    }
    public virtual void AdditionalSetup() { }
    #endregion

    #region Abstract Behaviour
    public abstract void UpdateExecute();
    public abstract void StartConversation();
    public abstract void EndConversation();
    public abstract void BeCalmDown();
    public abstract void BeSilent();
    public virtual void BeChasing() { }
    public virtual void BePenalty() { }
    #endregion

    #region Get Bool Method (Virtual)
    public virtual bool IsSpawn() { return entityData.currentSpawnState; }
    public virtual void IsInRoom(bool _isInRoom) { return; }
    #endregion
}
