using UnityEngine.UIElements;

public class FeatureDatas  { }

//   STUDY_ONDESK = 1,
//    TALK_CHAIRMAN = 2,
//    DEATH_ALL = 3,
//    DEATH_PILL = 4,
//    LISTEN_MYSTORY = 5,
//    ESCAPE_SCHOOL = 6,
//    STUDENT_COUNCIL = 7,
//    CAREER_ADVANCE = 8,
//    ALL_ARCHIEVE = 9

public abstract class Achievement
{
    public abstract void CheckAllConidtion();
    public virtual void GetAchievement(string _ID)
    {
        SteamfeatureController.Instance?.UnLockAchievement(_ID);
    }
}

public class Achievement_01 : Achievement
{
    public string achievementID = "ACHIEV_01";
    public bool isStudy = false;
    public override void CheckAllConidtion()
    {
        SteamfeatureController.Instance.FeatureManager.WriteAllText(0);
        if (isStudy)
        {
            GetAchievement(achievementID);
        }
    }
}
public class Achievement_02 : Achievement
{
    public string achievementID = "ACHIEV_02";
    public bool isTalk = false;
    public override void CheckAllConidtion()
    {
        SteamfeatureController.Instance.FeatureManager.WriteAllText(1);
        if (isTalk)
        {
            GetAchievement(achievementID);
        }
    }
}
public class Achievement_03 : Achievement
{
    public string achievementID = "ACHIEV_03";

    public bool isPrincipalDeath = false;
    public bool isEyePenlatyDeath = false;
    public bool isBoardDeath = false;
    public bool isOnGuardDeath_B = false;
    public bool isOnGuardDeath_F = false;
    public bool isSirenDeath = false;
    public bool isCatchTeacherDeath = false;
    public bool isCatchGirlDeath = false;
    public override void CheckAllConidtion()
    {
        SteamfeatureController.Instance.FeatureManager.WriteAllText(2);
        if (isPrincipalDeath && isEyePenlatyDeath && isBoardDeath && isOnGuardDeath_B
            && isOnGuardDeath_F && isSirenDeath && isCatchTeacherDeath && isCatchGirlDeath)
        {
            GetAchievement(achievementID);
        }
    }
}
public class Achievement_04 : Achievement
{
    public string achievementID = "ACHIEV_04";
    public bool isPillDeath = false;
    public override void CheckAllConidtion()
    {
        SteamfeatureController.Instance.FeatureManager.WriteAllText(3);
        if (isPillDeath)
        {
            GetAchievement(achievementID);
        }
    }
}
public class Achievement_05 : Achievement
{
    public string achievementID = "ACHIEV_05";

    public bool isTalkChairman = false;
    public bool isTalkNurse = false;
    public bool isTalkShop = false;
    public bool isTalkStudent1 = false;
    public bool isTalkStudent2 = false;
    public bool isTalkStudent3 = false;
    public bool isTalkStudent4 = false;
    public bool isTalkStudent5 = false;
    public bool isTalkStudent6 = false;
    public bool isTalkStudent7 = false;
    public bool isTalkStudent8 = false;
    public bool isTalkStudent9 = false;
    public bool isTalkStudent10 = false;
    public bool isTalkStudent11 = false;
    public bool isTalkStudent12 = false;
    public bool isTalkStudentPresident = false;
    public bool isTalkTeacher1 = false;
    public bool isTalkTeacher2 = false;
    public override void CheckAllConidtion()
    {
        SteamfeatureController.Instance.FeatureManager.WriteAllText(4);
        if (isTalkChairman && isTalkNurse &&
            isTalkShop && isTalkStudent1 && isTalkStudent2 &&
            isTalkStudent3 && isTalkStudent4 && isTalkStudent5 &&
            isTalkStudent6 && isTalkStudent7 && isTalkStudent8 &&
            isTalkStudent9 && isTalkStudent10 && isTalkStudent11 &&
            isTalkStudent12 && isTalkStudentPresident && isTalkTeacher1 && isTalkTeacher2)
        {
            GetAchievement(achievementID);
        }
    }
}
public class Achievement_06 : Achievement
{
    public string achievementID = "ACHIEV_06";
    public bool isEnding = false;
    public override void CheckAllConidtion()
    {
        SteamfeatureController.Instance.FeatureManager.WriteAllText(5);
        if (isEnding)
        {
            GetAchievement(achievementID);
        }
    }
}
public class Achievement_07 : Achievement
{
    public string achievementID = "ACHIEV_07";
    public bool isJoinStudentCouncil = false;
    public override void CheckAllConidtion()
    {
        SteamfeatureController.Instance.FeatureManager.WriteAllText(6);
        if (isJoinStudentCouncil)
        {
            GetAchievement(achievementID);
        }
    }
}
public class Achievement_08 : Achievement
{
    public string achievementID = "ACHIEV_08";
    public bool isReadCareerMemo = false;
    public override void CheckAllConidtion()
    {
        SteamfeatureController.Instance.FeatureManager.WriteAllText(7);
        if (isReadCareerMemo)
        {
            GetAchievement(achievementID);
        }
    }
}

public class EndingCreditData
{
    public bool isWatchEnding = false;
    public bool isEnding = false;

    public bool ShowEndingCredit()
    {
        if(isEnding== true && isWatchEnding == false)
        {
            isWatchEnding = true;
            SteamfeatureController.Instance.FeatureManager.EndingWriteAllText();
            return true;
        }
        return false;
    }
}