using System.Collections.Generic;

public class RoomArchiveLogData
{
    public Dictionary <int, string> roomArchiveDictionary = new Dictionary<int, string>();

    public void GenerateDictionary(){
        // 앞에 2글자는 이형체 번호 뒤에 2개는 문장번호
        roomArchiveDictionary.Add(0001, "1층 교문 옆에 위치.\n철문 열쇠가 보관되어 있음.\n<u>※훔치는 중 수위가 돌아오는 것에 주의</u>");

        roomArchiveDictionary.Add(0101, "1층 교문 옆에 위치.\n현재까지 교장은 교장실 내에서 발견된 적 없음.\n공허한 공간들이 굉장히 많다.");

        roomArchiveDictionary.Add(0201, "1층 교장실 옆에 위치.\n수많은 트로피와 상패들이 전시되어 있음.\n진입 시 높은 확률로 이사장과 조우하니 주의 필요.\n<u>※ 현재까지 이사장을 두번 이상 마주친 생존자는 없음. 조우 시 주의</u>");

        roomArchiveDictionary.Add(0301, "1층 복도 끝에 위치.\n진입이 불가한 곳으로 파악 중.\n현재 탈출 방법으로는 진입할 필요 없으므로 무시할 것.");

        roomArchiveDictionary.Add(0401, "교실동 각 층마다 존재.\n학생들은 발견되지 않음. 휴식 공간으로 사용할 수 있을 듯.\n<u>※ 교장이 추격해올 경우 따돌리는 용도로 사용 가능한 것을 확인.</u>");

        roomArchiveDictionary.Add(0501, "2층 계단 앞에 위치.\n많은 음식물들이 있으나 섭취하지 않는 것을 권장.\n<u>※ 해당 학교는 20XX년 폐교된 상태 그대로인 것을 명심.</u>");

        roomArchiveDictionary.Add(0601, "2층. 교장실 위에 위치.\n높은 확률로 학생회장 조우 가능.\n<u>※ 학생회 가입에 대해서 확인된 내용 없음.</u>");

        roomArchiveDictionary.Add(0701, "2층 학생회실 옆에 위치.\n약은 절대로 직접 찾거나 만지지 말 것.\n<u>※ 뒤쪽 침대에 눕기를 포함한 일체의 행동을 삼가하기 바람.</u>");
        roomArchiveDictionary.Add(0702, "<b>$attempts차 추가. 보건실의 마네킹이 실종자의 시체인 것으로 보임. 신원 확인 바람.</b>");

        roomArchiveDictionary.Add(0801, "3층 계단 앞에 위치. 진입 시 주의.\n학교 내 유일한 화기 물품인 알코올램프가 보관되어 있음.\n자인고 폐교 당시 화재의 진원지로 추정됨.");
        
        roomArchiveDictionary.Add(0901, "3층 복도 끝에 위치.\n방송실 열쇠가 보관되어 있는 것을 확인.\n추가적인 위험 요소는 현재까지 발견하지 못함.");
        roomArchiveDictionary.Add(0902, "<b>$attempts차 추가. 전산실은 반드시 컴퓨터실을 통해서만 들어갈 수 있음.</b>");

        roomArchiveDictionary.Add(1001, "4층 계단 앞에 위치.\n현재까지 진입이 불가하며, 진입 시도 시 의문의 소리가 들린다는 보고.\n<u>※일정 주기로 실내화가 생기는 것으로 보임. 이유는 파악 불가.</u>");
        roomArchiveDictionary.Add(1002, "<s><b>$attempts차 추가. 이곳에서 옥상에서 투신자살한 학생들의 신발을 모아놓는다.</b></s>");
        roomArchiveDictionary.Add(1003, "<b>$attempts차 추가. 아직까지 진로진학부에 진입한 실종자 없음. 위 기록의 신빙성 검토할 것.</b>");

        roomArchiveDictionary.Add(1101, "4층의 큰 교실.\n하교종을 울릴 수 있는 기기가 있으므로 반드시 위치를 파악할 것.\n하교종이 울리면 이형체들이 굉장히 폭력적으로 변하는 듯 보임.");

        roomArchiveDictionary.Add(1201, "4층에 위치.\n진입이 불가한 곳으로 파악 중.\n현재 탈출 방법으로는 진입할 필요 없으므로 무시할 것.");

        roomArchiveDictionary.Add(1301, "각 층 복도의 큰 방에 위치.\n교실 등의 열쇠가 보관되어 있으므로 참고하기 바람.\n<u>※직접 진입이 불가능한 경우도 있음. 불가능하다면 다른 방법을 찾을 것.</u>");
        roomArchiveDictionary.Add(1302, "<b>$attempts차 추가. 1층 교무실</b>");
        roomArchiveDictionary.Add(1303, "<b>$attempts차 추가. 2층 교무실</b>");
        roomArchiveDictionary.Add(1304, "<b>$attempts차 추가. 3층 교무실</b>");


        roomArchiveDictionary.Add(1401, "교실동 4층에 위치. 특활동에서 넘어갈 수 있음.\n<s>옥상에서 뛰어내리면 탈출할 수 있다. 가장 쉬운 방법이므로 참고.</s>");
        roomArchiveDictionary.Add(1402, "<b>$attempts차 추가. 더이상 탈출할 수 없는 것으로 보임. 이유는 파악 중.</b>");
        roomArchiveDictionary.Add(1403, "<b>$attempts차 추가. 이형체의 신발을 신고 뛰어내리면 탈출 가능했으나 진로진학부가 막혀 사용 불가한 방법.</b>");

        roomArchiveDictionary.Add(1501, "3층 계단 맞은편에 위치.\n현재는 진입은 불가하나, 피아노 소리가 들린다는 보고 다수.\n현재 탈출 방법으로는 진입할 필요 없으므로 무시할 것.");

        roomArchiveDictionary.Add(1601, "3층에 위치.\n현재 탈출 방법으로는 진입할 필요 없으므로 무시할 것.");
        roomArchiveDictionary.Add(1602, "<b>$attempts차 추가. 전산실을 가기 위해 반드시 지나야 함. 위험은 없는 것으로 추정.</b>");
    }
}
