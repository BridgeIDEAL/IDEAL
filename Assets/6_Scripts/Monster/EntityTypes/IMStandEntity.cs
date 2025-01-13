using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMStandEntity : IMovableEntity
{
    [SerializeField] protected EntitySight sight;

    /// <summary>
    /// Init : Rig, Anim, PlayerTF Component
    /// </summary>
    public override void AdditionalInit() { sight.Init(playerHeightTransform); }
    
    /// <summary>
    /// Animation : current use for only talk state
    /// </summary>
    /// <param name="_currentType"></param>
    /// <param name="_isStart"></param>
    public override void SetAnimation(EntityStateType _currentType, bool _isStart)  { anim.SetBool("IsTalk", _isStart); }

    #region Act Frame
    public override void IdleEnter() { }
    public override void IdleExecute() { }
    public override void IdleExit() { }
    public override void TalkEnter() { sight.SetRotate(true); SetAnimation(currentType, true); }
    public override void TalkExecute() { }
    public override void TalkExit() { sight.SetRotate(false); SetAnimation(currentType, false); }
    public override void QuietEnter() { }
    public override void QuietExecute() { }
    public override void QuietExit() { }
    public override void PenaltyEnter() { }
    public override void PenaltyExecute() { }
    public override void PenaltyExit() { }
    #endregion
}
