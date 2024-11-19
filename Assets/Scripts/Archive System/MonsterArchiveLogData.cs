using System.Collections.Generic;
public class MonsterArchiveLogData
{
    public Dictionary <int, string> monsterArchiveDictionary = new Dictionary<int, string>();

    public void GenerateDictionary(){
        // 앞에 2글자는 이형체 번호 뒤에 2개는 문장번호
        monsterArchiveDictionary.Add(0001, "1층의 이사장실에서 발견.\n눈이 트로피로 변한 모습을 하고 있다.\n자부심이 강한 모습을 보이며, 학생다운 자세에 집착한다.");

        monsterArchiveDictionary.Add(0101, "1층 전역에서 발견.\n외부인을 매우 경계하는 모습을 보임.\n<u>※신원을 확인하려 하면 즉시 도망칠 것.</u>");

        monsterArchiveDictionary.Add(0201, "2층의 보건실에서 발견 가능.\n놓인 약은 구분할 수 없으니 반드시 주는 약을 받을 것.");
        monsterArchiveDictionary.Add(0202, "<b>$attempts차 추가. 멍 치료제가 아닌 약은 극약인 것으로 추정. 반드시 멍 치료제를 받아낼 것.</b>");

        monsterArchiveDictionary.Add(0301, "2층 매점 카운터에서 발견.\n터무니없는 가격을 요구하니 능숙한 흥정이 필요.");
        monsterArchiveDictionary.Add(0302, "<b>$attempts차 추가. 과할 경우 다시 터무니없는 가격을 요구하니 주의.</b>");

        monsterArchiveDictionary.Add(0401, "과학실 내부에서 주로 발견.\n멍 치료제를 줄 경우 우호적인 모습을 보임.");
        monsterArchiveDictionary.Add(0402, "<b>$attempts차 추가. 약을 건네지 않을 경우 더욱 폭력적으로 변하니 주의.</b>");

        monsterArchiveDictionary.Add(0501, "2학년 교실 내부에서 발견. (정확한 장소 알 수 없음)\n단 맛이 나는 빵을 특히 원하는 것으로 보임.\n<u>※매점에 빵이 없을 경우의 대처 추가 바람.</u>");

        monsterArchiveDictionary.Add(0601, "학교 전역에서 발견 가능. <s><color=#272727>조우 시 주의.</s></color>\n<u>※ 학생의 사소한 것을 트집잡아 교무실로 데려가려 시도.</u>");
        monsterArchiveDictionary.Add(0602, "<b>$attempts차 추가. 이 방법 이외의 교무실 진입 방법이 없는 것으로 추정\n따라서 일부러 만난 뒤 교무실에 들어가 열쇠를 챙겨 탈출을 권장.</b>");
        monsterArchiveDictionary.Add(0603, "<b>$attempts차 추가. 3층 교무실에서 열쇠를 가지고 나올 경우 갑자기 뒤따라오는 현상 발견. 무조건 도망칠 것.</b>");

        monsterArchiveDictionary.Add(0701, "1층 교무실에서 발견.\n체벌 얘기에 민감하게 반응하는 것을 확인.\n이외의 상황에서는 크게 위협적이지 않음.");
        monsterArchiveDictionary.Add(0702, "<b>$attempts차 추가. 교무실에서 열쇠를 집은 뒤 나오려는 순간 신호가 끊긴 기록 존재.\n간신히 복원한 기록에서는 여성의 비명소리(신입선생님으로 추정) 외에는 아무 흔적도 찾을 수 없었음.\n신입선생님을 자세히 살펴보아야 할 것으로 보임.</b>");

        monsterArchiveDictionary.Add(0801, "학교 전역에서 발견 가능.\n학교 내 가장 위험한 이형체이므로 조우 시 주의를 요함.\n머리가 큰 CCTV로 변한 모습, 키도 매우 큰 것을 확인.\n<u>※즉시 도주해야 함. 추격이 멈추는 조건은 아직 확인 불가.</u>");
        monsterArchiveDictionary.Add(0802, "<b>$attempts차 추가. 자습실 내부까지는 들어오지 않는 것을 확인. 자습실로 도주 권장.</b>");

        monsterArchiveDictionary.Add(0901, "학생회실 내부에서 발견 가능.\n실종자를 학생회에 권유한다. 현재까지 학생회에 대해 밝혀진 내용은 없음.");
        monsterArchiveDictionary.Add(0902, "<b>$attempts차 추가. 학생회에 대해 밝혀진 내용은 아래와 같음.\n1. 학생회는 자인고 홍보 책자를 제작하고 배포한다.\n2. 학■회는 ■든 학■■의 ■적사■을 ■ 수 ■■.\n3. 학생■■ 아■■ 하■■지 못■■ ■는■.</b>");

        monsterArchiveDictionary.Add(1001, "컴퓨터실에서 발견 가능.\n일반적으로 실종자에게 관심이 없는 것으로 보임.\n무슨 얘기를 하든 조용히 듣고 지나가는 것을 추천.");
        monsterArchiveDictionary.Add(1002, "<b>$attempts차 추가: 컴퓨터실의 ■■을 무시하고 전산실에 들어가려다 갑자기 영화가 보고 싶다는 말과 함께 실종자의 신호가 끊김.\n이후 켜진 컴퓨터가 한 대 늘어났다는 것 외에는 실종자의 흔적을 찾을 수 없었음.</b>");
    }
}
