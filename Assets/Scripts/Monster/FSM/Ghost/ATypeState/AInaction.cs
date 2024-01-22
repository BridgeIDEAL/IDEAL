using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AInaction : AType
{
    public override void StartConversationInteraction() { ChangeState(ATypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(ATypeEntityStates.Indifference); }
    public override void SpeechlessInteraction() { ChangeState(ATypeEntityStates.Speechless); }
}
