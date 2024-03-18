using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseEntity : MonoBehaviour
{
    #region Common Stat
    protected GameObject player;
    protected bool activeLook = false;
    [Range(0, 2)] [SerializeField] protected float lookSpeed = 1f; 
    [Range(0, 1)] [SerializeField] protected float lookWeight = 0f;
    [Range(0, 1)] [SerializeField] protected float headWeight = 1f;
    [Range(0, 1)] [SerializeField] protected float bodyWeight = 0f;
    #endregion

    #region Virtual
    public virtual void Setup()
    {
        player = GameManager.Instance.variableHub.player;
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
    }

    public abstract void UpdateBehavior();
    public virtual void StartConversationInteraction() { }
    public virtual void EndConversationInteraction() { }
    public virtual void SpeechlessInteraction() { }
    public virtual void ChaseInteraction() { }
    public virtual void LookPlayer() { }
    public virtual void LookOriginal() { }
    #endregion

    #region Coroutine

    protected void ActiveLookCor(bool _isLookPlayer = true)
    {
        if (_isLookPlayer) { StartCoroutine("LookPlayerCor"); }
        else { StartCoroutine("LookForwardCor"); }
    }
    protected IEnumerator LookPlayerCor()
    {
        activeLook = true;
        float current = 0f;
        float percent = 0f;
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / lookSpeed;
            lookWeight = Mathf.Lerp(0f, 1f, percent);
            yield return null;
        }
        yield return null;
    }

    protected IEnumerator LookForwardCor()
    {
        float current = 0f;
        float percent = 0f;
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / lookSpeed;
            lookWeight = Mathf.Lerp(1f, 0f, percent);
            yield return null;
        }
        activeLook = false;
        yield return null;
    }
    #endregion
}
