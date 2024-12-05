using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareNoAction : JumpScare
{
    [SerializeField] protected int deathIndex;
    public override void ActiveJumpScare()
    {
        TurnOffUIs();
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOverWithVHSEffect(deathIndex);
    }

    public override void SetCameraSetting() { }
}
