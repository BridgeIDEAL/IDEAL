using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ATypeEntityStates // �߰�, ��� x
{
    Indifference,
    Interaction,
    Speechless
}

public enum BTypeEntityStates // �߰� o, ��� x
{
    Indifference,
    Interaction,
    Aggressive,
    Chase,
    Speechless
}

public enum CTypeEntityStates // �߰� x, ��� o
{
    Indifference,
    Interaction,
    Watch,
    Speechless,
    Penalty
}

public enum DTypeEntityStates // �߰� o, ��� o
{
    Indifference,
    Interaction,
    Watch,
    Aggressive,
    Chase,
    Speechless
}

public enum StateEventType
{
    StartConversation,
    EndConversation,
    Chase,
    Rest,
    Spawn
}

public enum SoundType
{
    Ambience,
    Effect,
    MaxSoundCnt
}