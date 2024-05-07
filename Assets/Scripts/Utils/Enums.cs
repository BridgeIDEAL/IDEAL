public class Enums { }

/// <summary>
/// Use ChaseEntity StateMachine
/// </summary>
public enum ChaseEntityStates
{
    Idle,
    Talk,
    Quiet,
    Penalty,
    Chase,
    Extra
}
/// <summary>
/// Use NonChaseEntity StateMachine
/// </summary>
public enum NonChaseEntityStates
{
    Idle,
    Talk,
    Quiet,
    Penalty,
    Extra
}

/// <summary>
/// Use EntityEventManager & EntityManager
/// </summary>
public enum EntityEventStateType
{
    StartConversation,
    EndConversation,
    BeCalmDown,
    BeSilent,
    BeChasing,
    BePenalty
}

public enum SoundType
{
    Ambience,
    Effect,
    MaxSoundCnt
}