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

public enum EventType
{
    RestInteraction, // 휴식 공간에 들어갔을 때 or 층이 변했을 때
    StartInteraction, // 상호작용 시작할 때
    EndInteraction,
    ChaseInteraction // 실패하면 쫓아오는 개체
}

public enum SoundType
{
    Ambience,
    Effect,
    MaxSoundCnt
}