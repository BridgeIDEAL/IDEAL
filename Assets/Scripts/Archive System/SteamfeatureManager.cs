using System.IO;
using UnityEngine;

public class SteamfeatureManager : MonoBehaviour
{
    string dataPath;
    string extension = ".json";
    string[] achievementName = new string[8]
    {"/Achievement01", "/Achievement02", "/Achievement03" , "/Achievement04" ,
        "/Achievement05" , "/Achievement06" , "/Achievement07" , "/Achievement08" };

    Achievement_01 achievement_01;
    Achievement_02 achievement_02;
    Achievement_03 achievement_03;
    Achievement_04 achievement_04;
    Achievement_05 achievement_05;
    Achievement_06 achievement_06;
    Achievement_07 achievement_07;
    Achievement_08 achievement_08;

    EndingCreditData endingCreditData;

    public Achievement_01 Achievement01 { get { return achievement_01; }/*  set { achievement_01 = value; }*/}
    public Achievement_02 Achievement02 { get { return achievement_02; }/* set { achievement_02 = value; } */}
    public Achievement_03 Achievement03 { get { return achievement_03; }/* set { achievement_03 = value; } */}
    public Achievement_04 Achievement04 { get { return achievement_04; }/* set { achievement_04 = value; } */}
    public Achievement_05 Achievement05 { get { return achievement_05; }/* set { achievement_05 = value; } */}
    public Achievement_06 Achievement06 { get { return achievement_06; }/* set { achievement_06 = value; } */}
    public Achievement_07 Achievement07 { get { return achievement_07; }/* set { achievement_07 = value; } */}
    public Achievement_08 Achievement08 { get { return achievement_08; }/* set { achievement_08 = value; } */}

    public EndingCreditData EndingCreditData { get { return endingCreditData; } }
    public void Awake()
    {
        dataPath = Application.persistentDataPath;
        
        for(int i=0; i<achievementName.Length; i++)
        {
            if (File.Exists(dataPath + achievementName[i] + extension))
            {
                ReadAchievementFile(i);
            }
            else
            {
                CreateAchievementFile(i);
            }
        }

        string creditDataPath = Application.persistentDataPath + "/EndingCredit.json";

        if (File.Exists(creditDataPath))
            endingCreditData = JsonUtility.FromJson<EndingCreditData>(File.ReadAllText(creditDataPath));
        else
        {
            endingCreditData = new EndingCreditData();
            string texts = JsonUtility.ToJson(endingCreditData);
            File.WriteAllText(creditDataPath, texts);
        }
    }

    public void ReadAchievementFile(int _index)
    {
        string text = File.ReadAllText(dataPath + achievementName[_index] + extension);
       switch (_index)
        {
            case 0:
                achievement_01 = JsonUtility.FromJson<Achievement_01>(text);
                break;
            case 1:
                achievement_02 = JsonUtility.FromJson<Achievement_02>(text);
                break;
            case 2:
                achievement_03 = JsonUtility.FromJson<Achievement_03>(text);
                break;
            case 3:
                achievement_04 = JsonUtility.FromJson<Achievement_04>(text);
                break;
            case 4:
                achievement_05 = JsonUtility.FromJson<Achievement_05>(text);
                break;
            case 5:
                achievement_06 = JsonUtility.FromJson<Achievement_06>(text);
                break;
            case 6:
                achievement_07 = JsonUtility.FromJson<Achievement_07>(text);
                break;
            case 7:
                achievement_08 = JsonUtility.FromJson<Achievement_08>(text);
                break;
            default:
                break;
        }
    }

    public void CreateAchievementFile(int _index)
    {
        string text = "";
        switch (_index)
        {
            case 0:
                achievement_01 = new Achievement_01();
                text = JsonUtility.ToJson(achievement_01);
                break;
            case 1:
                achievement_02 = new Achievement_02();
                text = JsonUtility.ToJson(achievement_02);
                break;
            case 2:
                achievement_03 = new Achievement_03();
                text = JsonUtility.ToJson(achievement_03);
                break;
            case 3:
                achievement_04 = new Achievement_04();
                text = JsonUtility.ToJson(achievement_04);
                break;
            case 4:
                achievement_05 = new Achievement_05();
                text = JsonUtility.ToJson(achievement_05);
                break;
            case 5:
                achievement_06 = new Achievement_06();
                text = JsonUtility.ToJson(achievement_06);
                break;
            case 6:
                achievement_07 = new Achievement_07();
                text = JsonUtility.ToJson(achievement_07);
                break;
            case 7:
                achievement_08 = new Achievement_08();
                text = JsonUtility.ToJson(achievement_08);
                break;
            default:
                break;
        }
        File.WriteAllText(dataPath + achievementName[_index] + extension, text);
    }

    public void WriteAllText(int _index)
    {
        string text = "";
        switch (_index)
        {
            case 0:
                text = JsonUtility.ToJson(achievement_01);
                break;
            case 1:
                text = JsonUtility.ToJson(achievement_02);
                break;
            case 2:
                text = JsonUtility.ToJson(achievement_03);
                break;
            case 3:
                text = JsonUtility.ToJson(achievement_04);
                break;
            case 4:
                text = JsonUtility.ToJson(achievement_05);
                break;
            case 5:
                text = JsonUtility.ToJson(achievement_06);
                break;
            case 6:
                text = JsonUtility.ToJson(achievement_07);
                break;
            case 7:
                text = JsonUtility.ToJson(achievement_08);
                break;
            default:
                break;
        }
        File.WriteAllText(dataPath + achievementName[_index] + extension, text);
    }

    public void EndingWriteAllText()
    {
        string creditDataPath = Application.persistentDataPath + "/EndingCredit.json";
        string texts = JsonUtility.ToJson(endingCreditData);
        File.WriteAllText(creditDataPath, texts);
    }
}
