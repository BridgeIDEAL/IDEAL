public class Enums { }

public enum EntityStateType
{
    Idle=0,
    Talk=1,
    Quiet=2,
    Penalty=3,
    Extra=4,
    Chase=5
}

public enum SoundType
{
    Ambience,
    Effect,
    MaxSoundCnt
}

public enum PlaceTriggerType
{
    RestPlace,
    GuardRoom_1F,
    StudyRoom_1F,
    None
}

public enum ClassRoomNameType
{
    Room1_1,
    Room1_2,
    Room1_3,
    Room1_4,
    Room1_5,
    Room1_6,
    Room1_7,
    Room1_8,
    Room2_1,
    Room2_2,
    Room2_3,
    Room2_4,
    Room2_5,
    Room2_6,
    Room2_7,
    Room2_8,
    Room3_1,
    Room3_2,
    Room3_3,
    Room3_4,
    Room3_5,
    Room3_6,
    Room3_7,
    Room3_8
}

public enum CleanType
{
    Cabinet,
    Board,
    Floor,
    None
}

public enum SpawnObjectType
{
    Item,
    Area,
    None
}