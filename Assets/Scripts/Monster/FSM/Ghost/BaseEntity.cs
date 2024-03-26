using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseEntity : MonoBehaviour
{
    #region Common Stat
    protected GameObject player;
    protected bool IsHeadRotate { get; set; } = false;
    [Range(0, 2)] [SerializeField] protected float lookSpeed = 1f;
    [Range(0, 1)] [SerializeField] protected float lookWeight = 0f;
    [Range(0, 1)] [SerializeField] protected float headWeight = 1f;
    [Range(0, 1)] [SerializeField] protected float bodyWeight = 0f;
    #endregion

    #region Setting Virtual
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
    public virtual void AdditionalSetup() { }
    #endregion

    #region Behaviour Virtual
    public abstract void UpdateBehavior();
    public virtual void StartConversationInteraction() { }
    public virtual void EndConversationInteraction() { }
    public virtual void SpeechlessInteraction() { }
    public virtual void ChaseInteraction() { }
    public virtual void LookPlayer() { StartCoroutine("LookPlayerCor"); }
    public virtual void LookFront() { StartCoroutine("LookForwardCor"); }
    #endregion

    #region Method
    public float CalculateAngle()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        return angle;
    }
    public void WatchPlayer()
    {
        float angle = CalculateAngle();
        if (angle >= -90 && angle <= 90)
            LookPlayer();
        //else if (angle < -90 && angle > -180)
        //    anim.SetBool("LEFT", true);
        //else
        //    anim.SetBool("RIGHT", true);
    }
    public void WatchFront()
    {
        float angle = CalculateAngle();
        if (angle >= -90 && angle <= 90)
            LookFront();
        //else if (angle < -90 && angle > -180)
        //    anim.SetBool("LEFT", false);
        //else
        //    anim.SetBool("RIGHT", false);
    }
    #endregion

    #region Coroutine
    protected IEnumerator LookPlayerCor()
    {
        IsHeadRotate = true;
        float current = 0f;
        float percent = 0f;
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / lookSpeed;
            lookWeight = Mathf.Lerp(0f, 1f, percent);
            yield return null;
        }
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
        IsHeadRotate = false;
        yield return null;
    }
    #endregion
}
