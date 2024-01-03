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
    StartInteraction, // 상호작용을 하는 상태
    EndInteraction, // 상호작용 종료, 어그로 해제된 상태
    ChaseInteraction, // 플레이어가 쫓기는 상태
}

public enum SoundType
{
    Ambience,
    Effect,
    MaxSoundCnt
}