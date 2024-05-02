using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseEntity : MonoBehaviour
{
    
    #region Variable
    protected GameObject player;
    #endregion

    #region Init Setting
    public virtual void Setup(GameObject _player)
    {
        if (player != null)
            return;
        player = _player;
        InteractionConversation interactionConversation = GetComponent<InteractionConversation>();
        if (interactionConversation != null)
        {
            interactionConversation.dialogueRunner = IdealSceneManager.Instance.CurrentGameManager.scriptHub.dialogueRunner;
            interactionConversation.conversationManager = IdealSceneManager.Instance.CurrentGameManager.scriptHub.conversationManager;
        }
        else
        {
            InteractionNurse interactionNurese = GetComponent<InteractionNurse>();
            interactionNurese.dialogueRunner = IdealSceneManager.Instance.CurrentGameManager.scriptHub.dialogueRunner;
            interactionNurese.conversationManager = IdealSceneManager.Instance.CurrentGameManager.scriptHub.conversationManager;
        }
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
    #endregion

    #region Virtual Method
    public virtual bool IsSpawn() { return true; }
    #endregion
}
