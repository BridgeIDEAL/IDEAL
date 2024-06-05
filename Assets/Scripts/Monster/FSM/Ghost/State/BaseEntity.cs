using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseEntity : MonoBehaviour
{

    #region 공통으로 가져야 하는 변수
    [Header("스폰 여부")]
    [SerializeField] private bool isSpawn = true;
    [SerializeField] private bool isSetUp = false;
    public bool IsSpawn { get { return isSpawn;  } set { isSpawn = value; } }
    public bool IsSetUp { get { return isSetUp; } }
    [SerializeField] protected EntityStateType currentState = EntityStateType.Idle;
    #endregion

    #region Init Setting
    public virtual void Setup()
    {
        isSetUp = true;
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
    public abstract void ChangeState(EntityStateType _newState);
    public abstract void UpdateState();
    public abstract void IdleState();
    public abstract void TalkState();
    public abstract void QuietState();
    public virtual void ChaseState() { }
    public virtual void PenaltyState() { }
    public virtual void ExtraState() { } // Use By ChaseEntity (Use Aggressive Motion) 
    #endregion
}
