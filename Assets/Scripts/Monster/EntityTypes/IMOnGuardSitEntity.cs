using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMOnGuardSitEntity : IMovableEntity
{
    bool onceEvent = true;
    [SerializeField] SitSight sight;
    [SerializeField] OnGuardDetectController detectController;

    /// <summary>
    /// Init : Rig, Anim, PlayerTF Component
    /// </summary>
    public override void AdditionalInit() { sight.Init(playerHeightTransform); }

    public override void AdditionalSetup()
    {
        base.AdditionalSetup();
        if (Entity_Data.speakIndex == -1)
        {
            onceEvent = false;
            detectController.IsOnGuard = false;
            this.gameObject.layer = defaultLayer;
        }
    }

    /// <summary>
    /// Animation : current use for only talk state
    /// </summary>
    /// <param name="_currentType"></param>
    /// <param name="_isStart"></param>
    public override void SetAnimation(EntityStateType _currentType, bool _isStart) 
    {
        if (_isStart)
            anim.SetTrigger("Turn");
        else
            anim.SetTrigger("Front");
    }

    #region Act Frame
    public override void IdleEnter() { }
    public override void IdleExecute() { if (onceEvent && detectController.IsOnGuard) { ChangeState(EntityStateType.Penalty); } }
    public override void IdleExit() { }
    public override void TalkEnter() { /*sight.BodyToPlayer();*/ /*SetAnimation(currentType, true);*/ detectController.IsOnGuard = false; onceEvent = false; }
    public override void TalkExecute() { }
    public override void TalkExit() { sight.GazeFrontOnGuard(); /*SetAnimation(currentType, false);*/ }
    public override void QuietEnter() { }
    public override void QuietExecute() { }
    public override void QuietExit() { }
    public override void PenaltyEnter() { IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.LookOutStart(); sight.HeadToPlayer(); }
    public override void PenaltyExecute() { }
    public override void PenaltyExit() { IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.LookOutEnd(); }
    #endregion
}
