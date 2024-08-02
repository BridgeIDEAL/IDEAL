using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadOfStudentTeacher : ImmovableEntity
{
    [SerializeField] DetectPlayer detectPlayer;
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
    public override void TalkEnter() { SetAnimation(currentType, true); lookPlayer.GazePlayer(playerTransform); }
    public override void TalkExecute() { }
    public override void TalkExit() { SetAnimation(currentType, false); lookPlayer.GazeFront(); Entity_Data.isSpawn = false; Controller.InActiveEntity(Entity_Data.speakerName);}
    public override void QuietEnter() { SetAnimation(currentType, true); }
    public override void QuietExecute() { }
    public override void QuietExit() { SetAnimation(currentType, false); }
    public override void PenaltyEnter() { SetAnimation(currentType, true); }
    public override void PenaltyExecute() { }
    public override void PenaltyExit() { SetAnimation(currentType, false); }
    #endregion
}
