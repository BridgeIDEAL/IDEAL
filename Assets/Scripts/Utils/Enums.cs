public class Enums { }

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
    Last1F_Principal,
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