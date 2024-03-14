using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ATypeEntityStates // 추격, 경계 x
{
    Indifference,
    Interaction,
    Speechless
}

public enum BTypeEntityStates // 추격 o, 경계 x
{
    Indifference,
    Interaction,
    Aggressive,
    Chase,
    Speechless
}

public enum CTypeEntityStates // 추격 x, 경계 o
{
    Indifference,
    Interaction,
    Watch,
    Speechless
}

public enum DTypeEntityStates // 추격 o, 경계 o
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