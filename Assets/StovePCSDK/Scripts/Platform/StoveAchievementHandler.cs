using System;
using System.Collections;
using System.Collections.Generic;
using Stove.PCSDK.NET;
using UnityEngine;

public class StoveAchievementHandler
{
    internal static StovePCResult UnlockAchievement(String achievementId)
    {
        return StovePC.SetStat(achievementId.ToUpper(), 1);
    }
}
