using System.Collections.Generic;
public class MonsterArchiveLogData
{
    public Dictionary <int, string> monsterArchiveDictionary = new Dictionary<int, string>();

    public void GenerateDictionary(){
        // 앞에 2글자는 이형체 번호 뒤에 2개는 문장번호
        monsterArchiveDictionary.Add(0001, "Found in the Chairman's office on the first floor.\nCan distinguish easily since her eyes are pierced with trophies.\nVery proud of herself as a chairman, obsessed with the image of a perfect student.");

        monsterArchiveDictionary.Add(0101, "Found all over the first floor.\nAppears to be very wary of outsiders.\n<u>※ Run away immediately if you he asks for your identity.</u>");

        monsterArchiveDictionary.Add(0201, "Found in the Infirmary on the second floor.\nBe sure to take the pills given to you, as they are indistinguishable.");
        monsterArchiveDictionary.Add(0202, "<b>$attempts차 추가. 멍 치료제가 아닌 약은 극약인 것으로 추정. 반드시 멍 치료제를 받아낼 것.</b>");

        monsterArchiveDictionary.Add(0301, "Found at the second floor School Shop counter.\nDemands an outrageous price, so skillful bargain is required.");
        monsterArchiveDictionary.Add(0302, "<b>record $attempts. If the bargain is too much, she'll ask for outrageous price again.</b>");

        monsterArchiveDictionary.Add(0401, "Found mostly inside the Science Room.\nFriendly if given a bruise cure.");
        monsterArchiveDictionary.Add(0402, "<b>record $attempts. Becomes more violent if medicine is not given.</b>");

        monsterArchiveDictionary.Add(0501, "Found inside a econd grade classroom. (Exact location unknown)\nSeemed to be particularly interested in sweet flavored bread.\n<u>※ Please add what to do if there is no bread in the School Shop.</u>");

        monsterArchiveDictionary.Add(0601, "Found throughout the school. <s><color=#272727>Be careful when encountered.</s></color>\n<u>※ Attempting to take students to the Student Center even by the smallest things.</u>");
        monsterArchiveDictionary.Add(0602, "<b>record $attempts. Assumed to have no other way to enter the Student Center.\nTherefore, advised to meet intentionally and enter the Student Center to grab the key.</b>");
        monsterArchiveDictionary.Add(0603, "<b>record $attempts. If you leave the Student Center with the key, you will be followed. Run away at all costs.</b>");

        monsterArchiveDictionary.Add(0701, "Found in the Student Center on the first floor.\nNoted sensitivity to talk of corporal punishment.\nNot much of a threat otherwise.");
        monsterArchiveDictionary.Add(0702, "<b>record $attempts. Missing person's signal was cut off when tried to leave the Student Center after picking up keys.\nIdentified a female scream in the barely recovered recordings, possibly a new student.\nRecommendation to always be vigilant of the teacher's behavior.</b>");

        monsterArchiveDictionary.Add(0801, "Found throughout the school.\nMost dangerous Anomaly creature in the school, use caution when encountering.\nVery tall and has a head transformed into a CCTV.\n<u>※Run away immediately. How to stop the chase has not yet been confirmed.</u>");
        monsterArchiveDictionary.Add(0802, "<b>record $attempts. Confirmed that it does not enter the Study Room. Recommend fleeing to the Study room.</b>");

        monsterArchiveDictionary.Add(0901, "Found inside the Student Council Room.\nOffer new students to join the Student Council. No records of the Student Council so far.");
        monsterArchiveDictionary.Add(0902, "<b>record $attempts. The following is what is known about the Student Council.\n1. The Student Council distributes school brochures.\n2. The Student Council can bro■se any s■ud■t fil■s.\n3. The S■u■■nt Co■■■il st■■■ an■o■e from le■v■■g the s■■ool.</b>");

        monsterArchiveDictionary.Add(1001, "Found in the Computer Room.\nGenerally does not appear to be interested in the missing person.\nRecommended to listen quietly to whatever is being said and walk away.");
        monsterArchiveDictionary.Add(1002, "<b>record $attempts: Missing person ignored ■■ in the computer lab and went missing after saying \"I want to watch a movie.\" \nNo sign of the missing person other than one more computer turned on afterward.</b>");
    }
}
