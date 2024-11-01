using System;

public class Enums 
{
    public static string GetString<T>(T _enumType) where T : Enum
    {
        return Enum.GetName(typeof(T), _enumType);
    }

    public static T GetEnum<T>(string enumString) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), enumString);
    }
}

public enum SceneNames
{
    Lobby=0,
    Prototype=1,
    Prototype_Second=2
}

public enum EntityStateType
{
    Idle=0,
    Talk=1,
    Quiet=2,
    Penalty=3,
    Chase=4,
    None=5
}

public enum SoundType
{
    Ambience,
    Effect,
    MaxSoundCnt
}

public enum PlaceTriggerType
{
    InStudyRoom,
    None
}

public enum EventNames
{
    CleanGraffiti_1F,
    CleanGraffiti_2F,
    CleanGraffiti_3F,
    Password_4F
}

public enum EventItemNames
{
    GetMedicine,
    DropKeyPiece_1F,
    DropKeyPiece_2F,
    DropKeyPiece_3F,
    CabientKeyPiece_1F,
    CabientKeyPiece_2F,
    CabientKeyPiece_3F,
}

public enum ClassroomCleanType
{
    Cabinet,
    Board,
    Floor,
    None
}

public enum RootMotionType
{
    Walk,
    Run,
    None
}

public enum ChaseEventType
{
    Last1F_APrincipal, 
    Last1F_BPrincipal,
    Last1F_Guard,
    Last3F_GirlStudent,
    Last3F_StudentOfHeadTeacher,
    Jump3F_StudentOfHeadTeacher,
    Jump2F_GirlStudent
}

public enum ClassCabinetSpawnItem
{
    BrokenKey201,
    BrokenKeyStudentRoom,
    BrokenKeyComputerRoom
}

public enum TeleportPoint
{
    None,
    BuildingB_1F,
    BuildingB_2F,
    BuildingB_3F
}

public enum EntityDialogueType
{
    CanOnlySayOnce=0,
    CanSayMayTimes=1
}