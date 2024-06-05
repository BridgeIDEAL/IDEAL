using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassRoomDoorActive : MonoBehaviour
{
    public enum ClassRoomName
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
    [SerializeField] InteractionDoor[] interactionDoor; // 0은 앞문, 1은 뒷문
    public ClassRoomName thisClassName;
    public Vector3 frontDest;
    public Vector3 rearDest;

    string className;
    int classKeyNum;
    public void Awake()
    {
        SetUp();
    }

    public void SetUp()
    {
        if (interactionDoor[0] == null)
            return;
        switch (thisClassName)
        {
            case ClassRoomName.Room1_1:
                SetClassInfo(1, 1);
                break;
            case ClassRoomName.Room1_2:
                SetClassInfo(1, 2);
                break;
            case ClassRoomName.Room1_3:
                SetClassInfo(1, 3);
                break;
            case ClassRoomName.Room1_4:
                SetClassInfo(1, 4);
                break;
            case ClassRoomName.Room1_5:
                SetClassInfo(1, 5);
                break;
            case ClassRoomName.Room1_6:
                SetClassInfo(1, 6);
                break;
            case ClassRoomName.Room1_7:
                SetClassInfo(1, 7);
                break;
            case ClassRoomName.Room1_8:
                SetClassInfo(1, 8);
                break;
            case ClassRoomName.Room2_1:
                SetClassInfo(2, 1);
                break;
            case ClassRoomName.Room2_2:
                SetClassInfo(2, 2);
                break;
            case ClassRoomName.Room2_3:
                SetClassInfo(2, 3);
                break;
            case ClassRoomName.Room2_4:
                SetClassInfo(2, 4);
                break;
            case ClassRoomName.Room2_5:
                SetClassInfo(2, 5);
                break;
            case ClassRoomName.Room2_6:
                SetClassInfo(2, 6);
                break;
            case ClassRoomName.Room2_7:
                SetClassInfo(2, 7);
                break;
            case ClassRoomName.Room2_8:
                SetClassInfo(2, 8);
                break;
            case ClassRoomName.Room3_1:
                SetClassInfo(3, 1);
                break;
            case ClassRoomName.Room3_2:
                SetClassInfo(3, 2);
                break;
            case ClassRoomName.Room3_3:
                SetClassInfo(3, 3);
                break;
            case ClassRoomName.Room3_4:
                SetClassInfo(3, 4);
                break;
            case ClassRoomName.Room3_5:
                SetClassInfo(3, 5);
                break;
            case ClassRoomName.Room3_6:
                SetClassInfo(3, 6);
                break;
            case ClassRoomName.Room3_7:
                SetClassInfo(3, 7);
                break;
            case ClassRoomName.Room3_8:
                SetClassInfo(3, 8);
                break;
            default:
                break;
        }
        ClassRoomDoorActive thisScript = this;
        thisScript.enabled = false;
    }

    public void SetClassInfo(int _grade, int _classNum)
    {
        classKeyNum = _grade * 100 + _classNum;
        className = $"{_grade}학년 {_classNum}반";
        // Front Door
        interactionDoor[0].DestPosition = frontDest;
        interactionDoor[0].DetectStr = className+"문이다.";
        interactionDoor[0].SuccessInteractionStr = className+"문을 열었다.";
        interactionDoor[0].FailInteractionStr = "문이 잠겨있다.";
        interactionDoor[0].NeedItem = classKeyNum;
        // Rear Door
        interactionDoor[1].DestPosition = rearDest;
        interactionDoor[1].DetectStr = className + "문이다.";
        interactionDoor[1].SuccessInteractionStr = className + "문을 열었다.";
        interactionDoor[1].FailInteractionStr = "문이 잠겨있다.";
        interactionDoor[1].NeedItem = classKeyNum;
    }
}
