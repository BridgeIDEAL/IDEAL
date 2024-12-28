using System.Collections.Generic;

public class RoomArchiveLogData
{
    public Dictionary <int, string> roomArchiveDictionary = new Dictionary<int, string>();

    public void GenerateDictionary(){
        // 앞에 2글자는 이형체 번호 뒤에 2개는 문장번호
        roomArchiveDictionary.Add(0001, "Located on the first floor, next to the school gate.\nKeys to the Steel Gate are kept there.\n<u>※ Beware of the guard returning while stealing.</u>");

        roomArchiveDictionary.Add(0101, "Located on the first floor by the main gate.\nTo date, the Principal has not been found inside.\nThere are a lot of empty spaces.");

        roomArchiveDictionary.Add(0201, "Located on the first floor, next to the Principal's office.\nNumerous trophies and plaques on display.\nBe careful as there is a high chance of encountering the Chairman upon entering.\n<u>※ No survivor has encountered the Chairman more than once. Encounter with caution.</u>");

        roomArchiveDictionary.Add(0301, "Located at the end of the hallway on the first floor.\nDoes not seem possible to enter.\nCurrent escape methods do not require entry, so ignore.");

        roomArchiveDictionary.Add(0401, "Present on each floor of the classroom wing.\nNo students found. Looks like it could be used as a safe room.");
        roomArchiveDictionary.Add(0402, "<u><b>record $attempts. Can be used as a safe zone in case of the Principal's chase.</b></u>");

        roomArchiveDictionary.Add(0501, "Located in front of the stairs to the second floor.\nLots of food available but not recommended for consumption.\n<u>※ Keep in mind that the school has been closed since 20XX.</u>");

        roomArchiveDictionary.Add(0601, "Second floor. Located above the principal's office.\nHigh chance of meeting the Student President.");
        roomArchiveDictionary.Add(0602, "<u><b>record $attempts. Very comfortable and cozy.</b></u>");

        roomArchiveDictionary.Add(0701, "Located on the second floor next to the Student council.\nNever look for or touch medication by yourself.\n<u>※ Refrain from any behavior, including lying on the back bed.</u>");
        roomArchiveDictionary.Add(0702, "<b>record $attempts. The mannequin in the health center appears to be the body of a missing person. Please confirm identification.</b>");

        roomArchiveDictionary.Add(0801, "Located in front of the stairs to the third floor. Use caution when entering.\nContains an alcohol lamp, the only firearm in the school.\nBelieved to be the origin of the fire at the time of the closing of Jain High School.");
        
        roomArchiveDictionary.Add(0901, "Located at the end of the hallway on the third floor.\nConfirmed that the key to the broadcast room is stored there.\nNo additional hazards noted at this time.");
        roomArchiveDictionary.Add(0902, "<b>$attempts차 추가. 전산실은 반드시 컴퓨터실을 통해서만 들어갈 수 있음.</b>");

        roomArchiveDictionary.Add(1001, "Located in front of the stairwell on the 4th floor.\nCurrently inaccessible, reports of mysterious noises being heard when attempting to enter.\n<u>※ It appears that the room is being lit up at regular intervals. The reason for this is unknown.</u>");
        roomArchiveDictionary.Add(1002, "<s><b>record $attempts. This is where the shoes of students who jumped from the roof are collected.</b></s>\n<b>record $attempts. No missing persons have yet entered the Career Counseling Room. Review the reliability of the previous records.</b>");

        roomArchiveDictionary.Add(1101, "Large classroom on the fourth floor.\nThere is a device that can ring the school bell, so make sure you know where it is.");
        roomArchiveDictionary.Add(1102, "<b>record $attempts: When the bell rings, the Anomaly creatures seem to become very violent.</b>");

        roomArchiveDictionary.Add(1201, "Located on the fourth floor.\nDetermining that it is impossible to enter.\nCurrent escape methods do not require entry, so ignore.");

        roomArchiveDictionary.Add(1301, "Located in a large room in the hallway on each floor.\nPlease note that keys to classrooms are kept there.\n<u>※Direct entry may not be possible. If this is not possible, find another way.</u>");
        roomArchiveDictionary.Add(1302, "<b>record $attempts: Be careful if there is a teacher in the Student Center on the first floor.</b>");
        roomArchiveDictionary.Add(1303, "<b>record $attempts: The Student Center on the second floor is currently believed to be accessible only by being dragged.</b>");


        roomArchiveDictionary.Add(1401, "Located on the 4th floor of the Classroom Wing. Can be reached from Extracurricular Activities.\n<s>You can escape by jumping off the roof. Note that this is the easiest way.</s>");
        roomArchiveDictionary.Add(1402, "<b>record $attempts. No longer appears to be possible. The reason is being investigated.</b>");
        roomArchiveDictionary.Add(1403, "<b>record $attempts. Jumping off the roof with the Anomaly student's shoes seemed possible, but this method is unavailable since the Career Counseling Room is blocked.</b>");

        roomArchiveDictionary.Add(1501, "Located across from the stairs on the 3rd floor.\nCurrently inaccessible, but multiple reports of piano sounds.\nCurrent escape methods do not require entry, so ignore.");

        roomArchiveDictionary.Add(1601, "Located on the third floor.\nIgnore as current escape method does not require entering.");
        roomArchiveDictionary.Add(1602, "<b>record $attempts. Must be passed to reach the Server Room. Beware of the ■■.</b>");
    }
}
