using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadOfStudentTeacher : IMovableEntity
{
    [SerializeField] VisibleDetectPlayer detectPlayer;
    bool once = true;

    public void Talk()
    {
        once = false;
        string talkID = Entity_Data.speakerName + Entity_Data.speakIndex;
        //ProgressManager.Instance.UpdateCheckList(102, 1);
        DialogueManager.Instance.StartDialogue(talkID, this);
        Entity_Data.isSpawn = false;
    }

    #region Act Frame
    public override void IdleEnter() { SetAnimation(currentType, true); }
    public override void IdleExecute() { if (detectPlayer.DetectExecute() && once) Talk(); }
    public override void IdleExit() { SetAnimation(currentType, false); }
    public override void TalkEnter() { SetAnimation(currentType, true); }
    public override void TalkExecute() { }
    public override void TalkExit() { SetAnimation(currentType, false); Entity_Data.isSpawn = false; IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIIngame.FadeOutInEffect(Teleport2ndTeacherOffice);}
    public override void QuietEnter() { SetAnimation(currentType, true); }
    public override void QuietExecute() { }
    public override void QuietExit() { SetAnimation(currentType, false); }
    public override void PenaltyEnter() { SetAnimation(currentType, true); }
    public override void PenaltyExecute() { }
    public override void PenaltyExit() { SetAnimation(currentType, false); }
    #endregion

    private void Teleport2ndTeacherOffice(){
        Controller.InActiveEntity(Entity_Data.speakerName);
        Vector3 destPosition = new Vector3(30f, 7.95f, 22f);
        Vector3 destRotation = new Vector3(0.0f, 90.0f, 0.0f);
        ProgressManager.Instance.SetItemLog(902, 1);
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.thirdPersonController.TelePortPositionRotation(destPosition, destRotation);
    }
}
