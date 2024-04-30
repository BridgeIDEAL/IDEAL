using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseEntity : MonoBehaviour
{
    #region Common Stat
    protected GameObject player;
    [SerializeField] protected ScriptableEntity entityData;
    public ScriptableEntity EntityData { get { return entityData; } }
    #endregion

    #region InitSetting
    public virtual void Setup()
    {
        InteractionConversation interactionConversation = GetComponent<InteractionConversation>();
        if (interactionConversation != null)
        {
            interactionConversation.dialogueRunner = GameManager.Instance.scriptHub.dialogueRunner;
            interactionConversation.conversationManager = GameManager.Instance.scriptHub.conversationManager;
        }
        else
        {
            InteractionNurse interactionNurese = GetComponent<InteractionNurse>();
            interactionNurese.dialogueRunner = GameManager.Instance.scriptHub.dialogueRunner;
            interactionNurese.conversationManager = GameManager.Instance.scriptHub.conversationManager;
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
    
    //public float CalculateAngle()
    //{
    //    Vector3 direction = player.transform.position - transform.position;
    //    float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
    //    return angle;
    //}
}
