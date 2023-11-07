using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ATypeEntityStates // 추격, 경계 x
{
    Indifference,
    Interaction
}

public enum BTypeEntityStates // 추격 o, 경계 x
{
    Indifference,
    Interaction,
    Aggressive,
    Chase
}

public enum CTypeEntityStates // 추격 x, 경계 o
{
    Indifference,
    Interaction,
    Watch
}

public enum DTypeEntityStates // 추격 o, 경계 o
{
    Indifference,
    Interaction,
    Watch,
    Aggressive,
    Chase
}

public enum EventType
{
    RestInteraction, // 휴식 공간에 들어갔을 때 or 층이 변했을 때
    StartInteraction, // 상호작용 시작할 때
    SuccessInteraction, // 모든 개체들이 성공했을 때
    FailInteraction, // 실패해도 쫓아오지 않는 개체
    ChaseInteraction // 실패하면 쫓아오는 개체
}