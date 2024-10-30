using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Autodesk.Fbx.FbxAnimCurveDef;

public class JumpScareNoAction : JumpScare
{
    [SerializeField] protected int deathIndex;
    public override void ActiveJumpScare()
    {
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver(deathIndex);
    }

    public override void SetCameraSetting() { }
}
