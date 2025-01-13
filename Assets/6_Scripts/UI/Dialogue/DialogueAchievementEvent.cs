using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DialogueEvent : MonoBehaviour
{
    //¡Þ
    //index
    //boolname

    public void GetAchievement(string _achievement)
    {
        int achCnt = _achievement.Length;
        int index = Convert.ToInt32(_achievement[1] - '0');
        string boolName = "";
        for(int i=2; i<achCnt; i++)
        {
            boolName += _achievement[i];
        }
        DivideAchievemenet(index, boolName);
    }

    public void DivideAchievemenet(int _index, string _boolName)
    {
        switch (_index)
        {
            case 2:
                if (SteamfeatureController.Instance.FeatureManager.Achievement02.isTalk == false)
                {
                    SteamfeatureController.Instance.FeatureManager.Achievement02.isTalk = true;
                    SteamfeatureController.Instance.FeatureManager.Achievement02.CheckAllConidtion();
                }
                break;
            case 5:
                switch (_boolName)
                {
                    case "chairman":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkChairman == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkChairman = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "nurse":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkNurse == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkNurse = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "shop":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkShop == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkShop = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "st1":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent1 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent1 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "st2":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent2 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent2 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "st3":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent3 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent3 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "st4":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent4 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent4 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "st5":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent5 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent5 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "st6":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent6 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent6 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "st7":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent7 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent7 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "st8":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent8 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent8 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "st9":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent9 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent9 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "st10":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent10 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent10 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "st11":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent11 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent11 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "st12":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent12 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudent12 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "stp":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudentPresident == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkStudentPresident = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "tea1":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkTeacher1 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkTeacher1 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                    case "tea2":
                        if (SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkTeacher2 == false)
                        {
                            SteamfeatureController.Instance.FeatureManager.Achievement05.isTalkTeacher2 = true;
                            SteamfeatureController.Instance.FeatureManager.Achievement05.CheckAllConidtion();
                        }
                        break;
                }
                break;
            case 7:
                if (SteamfeatureController.Instance.FeatureManager.Achievement07.isJoinStudentCouncil == false)
                {
                    SteamfeatureController.Instance.FeatureManager.Achievement07.isJoinStudentCouncil = true;
                    SteamfeatureController.Instance.FeatureManager.Achievement07.CheckAllConidtion();
                }
                break;
            case 8:
                if (SteamfeatureController.Instance.FeatureManager.Achievement08.isReadCareerMemo == false)
                {
                    SteamfeatureController.Instance.FeatureManager.Achievement08.isReadCareerMemo = true;
                    SteamfeatureController.Instance.FeatureManager.Achievement08.CheckAllConidtion();
                }
                break;
        }
    }
}
