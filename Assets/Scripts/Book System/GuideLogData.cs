using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLogData : MonoBehaviour
{
    // 가이드 ID는 0A0B0C로 대 중 소로 구분되며 각 단위에서 99개까지 가능
    public Dictionary <int, GuideLog> guideLogDictionary; // GuideLog 저장 Dictionary


    public GuideLog GetGuideLog(int _ID){
        if(guideLogDictionary.ContainsKey(_ID)){
            GuideLog guideLog = guideLogDictionary[_ID];
            return guideLog;
        }
        else{
            return new GuideLog(-1, "None", "None");
        }
    }

    public void Init(){
        guideLogDictionary = new Dictionary<int, GuideLog>();

        GenerateGuideLog();
    } 
    private void GenerateGuideLog(){
        // 가이드 ID는 0A0B0C로 대 중 소로 구분되며 각 단위에서 99개까지 가능
        //// -  1-0-0
        guideLogDictionary.Add(000000, new GuideLog(000000, "자인고등학교 탈출을 위해 반드시\r\n아래 대전제를 명심해 주십시오.\r\n\r\n해당 고등학교는 외부인을\r\n굉장히 경계합니다.\r\n\r\n따라서 본인을 언제나 전학생으로\r\n생각하고 행동해야 합니다.\r\n\r\n학교 내의 존재들은 당신의 정체에\r\n크게 관심 가지지는 않을 것입니다.", ""));
        guideLogDictionary.Add(000100, new GuideLog(000100, "자인고등학교는 2006년 설립된\r\n사립 고등학교입니다.\r\n학비는 비싸지만 입학 조건이 없어,\r\n고위층 자녀들이 입학했습니다.\r\n그리고 해당 학교는 2014년,\r\n불의의 화재 사고로 폐교했습니다.", ""));

        guideLogDictionary.Add(010000, new GuideLog(010000, "1. 학교 정문에서 지침서를 읽고,\r\n지침서를 꼼꼼히 암기해 주십시오.\r\n\r\n지침서를 학교 내부로 반입하는 것은\r\n당신의 정체를 들킬 위험이 생깁니다.\r\n반드시 지침서 없이 진입해 주십시오.\r\n\r\n또한 지침서 옆에 놓인 통신기를 숨겨\r\n착용해 주시기를 부탁드립니다.\r\n\r\n학교 내 정보를 수집하기 위함이며,\r\n안전한 곳에서는 통신이 가능합니다.", ""));
        guideLogDictionary.Add(010100, new GuideLog(010100, "└ n번 실종자가 해당 지침을 무시한 채\r\n   지침서를 들고 학교에 진입.\r\n   이후 정문의 수위 이형체에게\r\n   진압당한 후 통신 두절.", ""));

        guideLogDictionary.Add(020000, new GuideLog(020000, "2. 현재 알려진 자인고등학교의\r\n탈출 방법은 한 가지입니다.\r\n\r\n화재의 원인을 찾아 제거한 후\r\n옥상 문을 열고 탈출하는 것입니다.\r\n\r\n두 조건을 충족하셨다면,\r\n현재의 폐교된 자인고등학교 옥상으로\r\n탈출하실 수 있습니다.", ""));
        guideLogDictionary.Add(020100, new GuideLog(020100, "└ n번 실종자가 바로 옥상 탈출을 시도,\r\n   하지만 옥상이 현재로 변하지 않음.\r\n   이후 쫓아온 수위가 옥상은 출입\r\n   금지라며 1층으로 밀어 떨어트림.", ""));
        guideLogDictionary.Add(020200, new GuideLog(020200, "\r\n2-1. 옥상의 열쇠는 해당 학교의\r\n학생회장이 관리하고 있습니다.\r\n또한, 학교 옥상은 출입금지 상태이므로\r\n직접적인 언급은 자제하십시오.", ""));

        guideLogDictionary.Add(030000, new GuideLog(030000, "3. 정문으로 진입하면 높은 확률로\r\n수위를 마주칠 수 있을 것입니다.", ""));
        guideLogDictionary.Add(030100, new GuideLog(030100, "\r\n3-1. 수위를 만났다면, 교무센터 열쇠를\r\n미리 빌리는 것을 추천합니다.\r\n교무센터는 모든 교무실의 열쇠가\r\n보관되어 있습니다.", ""));
        guideLogDictionary.Add(030101, new GuideLog(030101, "└ n번 실종자가 학교 견학을 왔다고 하자,\r\n   수위는 실종자를 경비실로 데려감.\r\n   이후 실종자와 연락 두절.", ""));
        guideLogDictionary.Add(030102, new GuideLog(030102, "└ n번 실종자가 경비실 인솔을 거절하자,\r\n   수위는 불같이 화내며 실종자를 구타.\r\n   이후 쓰레기 봉투에서 실종자의 왼손을 확인.", ""));
        guideLogDictionary.Add(030200, new GuideLog(030200, "\r\n3-2. 수위를 만나지 못했다면,\r\n우선 그대로 진행하십시오.\r\n하지만 이후 반드시 수위를 만나\r\n교무센터 열쇠를 얻어야 합니다.", ""));

        guideLogDictionary.Add(040000, new GuideLog(040000, "4. 1층에서 마주칠 수 있는 이형체는\r\n다음과 같습니다.\r\n\r\n남학생, 여학생, 교장선생님.\r\n\r\n이들은 현재 파악된 대처가 없습니다.\r\n가능하다면 접근하지 마십시오.", ""));
        guideLogDictionary.Add(040100, new GuideLog(040100, "└ 남학생의 경우 다친 곳을 물어보면\r\n   해당 부위를 공격하는 것으로 추정.\r\n   최대한 탈출에 지장이 없도록 할 것.\r\n\r\n\r\n", ""));

        guideLogDictionary.Add(050000, new GuideLog(050000, "5. 교무센터 열쇠를 얻었다면,\r\n교무센터 문을 개방한 후 들어가\r\n1학년 교무실 열쇠를 챙기십시오.\r\n\r\n1학년 교무실의 열쇠는\r\n노란색 태그로 표시되어 있습니다.", ""));
        guideLogDictionary.Add(050100, new GuideLog(050100, "\r\n5-1. 교무센터는 1층에 존재하며,\r\n정문 반대쪽 계단 앞에 있습니다.", ""));
        guideLogDictionary.Add(050101, new GuideLog(050101, "└ 실종자가 다른 곳의 문을 열려고 하자,\r\n   교무센터 열쇠가 부서짐.\r\n   소리를 듣고 다가온 수위가 열쇠 파손을\r\n   보상하라며 심장 반 개를 요구.\r\n\r\n\r\n\r\n", ""));

        guideLogDictionary.Add(060000, new GuideLog(060000, "6. 이어서 학교의 2층으로 진입하면,\r\n아래 이형체들을 마주칠 수 있습니다.\r\n\r\n학생부장, 1학년 남학생\r\n\r\n해당 이형체들에 대한 대처 방법은\r\n아래에 추가 예정입니다.", ""));
        guideLogDictionary.Add(060100, new GuideLog(060100, "└ 1학년 남학생의 경우\r\n   강하게 대응하는 것을 추천.\r\n   상냥하게 다가간 n번 실종자에게\r\n   자신 대신 맞아달라고 부탁.\r\n   해당 부탁은 거절할 수 없으며\r\n   이후 실종자는 짓뭉개진 채 발견됨.\r\n\r\n", ""));

        guideLogDictionary.Add(070000, new GuideLog(070000, "7. 1학년 교무실은 2층에 있습니다.\r\n교무실 명패를 확인하시면 됩니다.", ""));
        guideLogDictionary.Add(070100, new GuideLog(070100, "\r\n7-1. 1학년 교무실에 들어가면 높은 확률로\r\n선생님을 마주칠 수 있을 것입니다.\r\n\r\n선생님을 마주쳤다면, 자연스럽게\r\n다음 수업의 위치를 알아내십시오.", ""));
        guideLogDictionary.Add(070101, new GuideLog(070101, "└ 선생님에게 다음 수업을 물어보자,\r\n   시간표를 보지 않냐며 n번 실종자의\r\n   손바닥을 수차례 가격.\r\n   반드시 직접 수업 과목을 물어보지 말 것.\r\n└ 자인고등학교 내의 시간은 현재 시간과\r\n   다른 것으로 추정.\r\n   시간표를 보는 것은 의미가 없으니\r\n   반드시 선생님과 대화하는 것을 추천.", ""));
        guideLogDictionary.Add(070200, new GuideLog(070200, "\r\n7-2. 이때, 선생님이 당신에게 심부름을\r\n시키는 경우가 있습니다.", ""));
        guideLogDictionary.Add(070201, new GuideLog(070201, "7-2-1. 만약 선생님이 다음 수업을 칠판에\r\n적어달라고 부탁하는 경우,\r\n수락한 뒤 그대로 수행하십시오.\r\n2층의 교실 중 학생이 없는 곳의 칠판에\r\n분필로 교실을 적으면 됩니다.", ""));

        guideLogDictionary.Add(080000, new GuideLog(080000, "8. 이어서, 들은 교실을 찾아야 합니다.\r\n확인된 정보는 다음과 같습니다.\r\n└ 자습실 : 4층 가운데 큰 공간\r\n└ 미술실 : 4층 옥상 계단 바로 옆\r\n└ 음악실 : 4층 좌측 계단 맞은편", ""));
        guideLogDictionary.Add(080100, new GuideLog(080100, "\r\n8-1. 낮은 확률로 미술실과 음악실의\r\n위치가 바뀌는 경우가 있습니다.\r\n\r\n반드시 교실의 명패를 확인하고\r\n들어가는 것을 추천드립니다.\r\n\r\n\r\n", ""));

        guideLogDictionary.Add(090000, new GuideLog(090000, "9. 4층에서 마주칠 수 있는 이형체는\r\n다음과 같습니다.\r\n\r\n축구부 주장, 3학년 여학생, 과학 교사\r\n\r\n대처법은 아직 확인된 바 없습니다.", ""));
        guideLogDictionary.Add(090100, new GuideLog(090100, "└ 3학년 여학생의 경우 입시 이야기에\r\n   굉장히 민감하게 반응함.\r\n   매우 폭력적으로 변하며 실종자의\r\n   시험지를 확인하려는 태도를 보임.", ""));
        guideLogDictionary.Add(090200, new GuideLog(090200, "└ 3학년 여학생은 n번 실종자를 잡은 뒤\r\n   몸에 있는 모든 빈 공간을 확인하고 돌아감.\r\n   이후 n번 실종자의 유체는 회수하여\r\n   가족에게 인계 완료.", ""));

        guideLogDictionary.Add(100000, new GuideLog(100000, "10. 알맞은 교실에 들어갔다면,\r\n자습중인 학생 한명이 있을 것입니다.\r\n\r\n해당 학생은 학생회장의 동생이며,\r\n항상 다음 수업의 교실에 미리 와서\r\n공부하는 것으로 확인되었습니다.", ""));
        guideLogDictionary.Add(100100, new GuideLog(100100, "\r\n10-1. 자습중인 학생과의 대화는 최대한\r\n짧게 끝내는 것을 추천드립니다.\r\n해당 학생과의 대화 중 당신은\r\n매우 큰 졸음을 느낄 것이며,\r\n잠든 실종자들의 상황은\r\n아직 확인된 것이 없습니다.", ""));
        guideLogDictionary.Add(100101, new GuideLog(100101, "└ 잠든 n번 실종자는 이후 수색 중\r\n잠든 채로 발견되어 구조.\r\n현재 157시간 동안 눈을 뜨지 않음.", ""));

        guideLogDictionary.Add(110000, new GuideLog(110000, "11. 학생회장의 위치를 알아냈다면,\r\n옥상의 열쇠를 빌려야 합니다.\r\n\r\n이 때, 반드시 아래의 특징을 확인하고\r\n학생회장을 찾아야 합니다.\r\n특징 : 긴 머리, 금빛 명찰", ""));
        guideLogDictionary.Add(110100, new GuideLog(110100, "└ 학생회장 근처에 비슷한 외관의\r\n   학생이 등장하는 것으로 추정.\r\n└ 해당 학생은 착각하고 물어보는\r\n   n번 실종자의 눈을 뽑아 바닥에 던짐.", ""));
        guideLogDictionary.Add(110200, new GuideLog(110200, "\r\n11-1. 옥상 열쇠는 쉽게 빌려주지 않지만,\r\n학생회장에게 선물을 주면 빌려주는\r\n상황이 확인되었습니다.\r\n\r\n선물에 대한 내용은 부록에 별첨.", ""));
        guideLogDictionary.Add(110301, new GuideLog(110301, "└ 열쇠를 위해 학생가 된 실종자들이\r\n   조건 완료 이후에도 복귀하지 않음.\r\n   절대 학생회에 가입하지 말 것.", ""));

        guideLogDictionary.Add(120000, new GuideLog(120000, "12. 해당 과정을 완료하였다면,\r\n다음은 보건실로 가십시오.\r\n보건실은 학교의 1층에 있습니다.", ""));
        guideLogDictionary.Add(120100, new GuideLog(120100, "\r\n12-1. 단, 보건실에 입장하려면 아래의\r\n두 가지 조건을 만족해야 합니다.\r\n\r\n1. 반드시 몸에 상처를 만들어야 합니다.\r\n2. 보건실 확인증을 지참해야 합니다.\r\n\r\n조건에 대한 상세 내용은 부록에 별첨.", ""));

        guideLogDictionary.Add(130000, new GuideLog(130000, "13. 보건실에 입장하면 높은 확률로\r\n보건선생님을 만날 수 있습니다.", ""));
        guideLogDictionary.Add(130100, new GuideLog(130100, "13-1. 만약 보건선생님이 계신다면,\r\n우선 확인증을 제출하십시오.", ""));
        guideLogDictionary.Add(130101, new GuideLog(130101, "└ 확인증 없으면 절대 들어가지 말 것.\r\n   보건교사 본인이 확인증을 작성한 뒤\r\n   실종자를 그대로 만드는 것으로 보임.\r\n   전신에 긁힌 상처라고 적은 보건 교사는\r\n   n번 실종자를 갈기갈기 찢음.", ""));
        guideLogDictionary.Add(130102, new GuideLog(130102, "└ 상처 꼭 만들고 들어갈 것.\r\n   건강한 상태로 진입 시 온 몸이\r\n   다쳤다고 말하며 치료를 진행.\r\n   치료받은 n번 실종자는 몸이 부풀다가\r\n   결국 터짐. 보건교사는 잠깐 갸우뚱한 뒤\r\n   평소 상태로 돌아감.", ""));
        guideLogDictionary.Add(130103, new GuideLog(130103, "13-1-1. 확인증을 제출하셨다면, 몸이 아프니\r\n침대에 누워있다 가겠다고 말씀하시면 됩니다.\r\n\r\n이때, 반드시 가장 안쪽 침대를 사용하십시오.", ""));
        guideLogDictionary.Add(130104, new GuideLog(130104, "└ 이 때, 약을 권해도 절대 먹지 말 것.\r\n   약을 먹은 n번 실종자는 잠시 후\r\n   캑캑대는 소리와 함께 연결이 끊김.\r\n   이후 수색에서 비슷한 옷차림의 이형체를\r\n   목격한 제보 다수 발생.", ""));
        guideLogDictionary.Add(130105, new GuideLog(130105, "└ 일단 약을 받았다면 이후에는 거절이 불가.\r\n   n번 실종자가 물과 함께 먹겠다며 회피하자,\r\n   보건 교사가 물도 직접 준비함. \r\n   물을 마신 실종자는 구토를 시작했으며,\r\n   몸 속의 모든 것을 토한 뒤 그대로 사망.\r\n   해당 학교는 2014년 폐교했으므로,\r\n   내부의 그 어떤 것도 먹지 말 것.", ""));
        guideLogDictionary.Add(130106, new GuideLog(130106, "└ 그 옆의 침대에 누우려던 n번 실종자는\r\n   누워있던 학생에게 그대로 붙잡힘.\r\n   현재 사망은 확인되지 않았으나,\r\n   구출 방법 또한 확인되지 않음.", ""));
        guideLogDictionary.Add(130107, new GuideLog(130107, "13-1-2. 침대에 누워서 잠시 쉰 후 일어나면,\r\n보건선생님이 자리를 비웠을 것입니다.\r\n책상에서 붉은 라벨이 붙은 상처약을 챙긴 후\r\n즉시 보건실을 나가십시오.\r\n", ""));
        guideLogDictionary.Add(130200, new GuideLog(130200, "13-2. 만약 보건선생님이 없다면,\r\n선생님의 책상에서 약을 찾아야 합니다.\r\n\r\n상처약은 붉은색 라벨이 붙어있으며, 다른\r\n약은 현재 효능이 확인된 것이 없습니다.\r\n\r\n상처약을 챙기셨다면,\r\n즉시 보건실에서 벗어나십시오.\r\n\r\n", ""));

        guideLogDictionary.Add(140000, new GuideLog(140000, "14. 지금까지의 과정을 완료했다면,\r\n학교 3층의 컴퓨터실을 찾으십시오.\r\n\r\n컴퓨터실에 진입하였을 때 당신은\r\n두 가지 상황에 마주칠 수 있습니다.\r\n\r\n", ""));
        guideLogDictionary.Add(140100, new GuideLog(140100, "14-1. 컴퓨터의 모니터가 하나만 켜져있고,\r\n해당 자리에 학생이 앉아있을 경우\r\n\r\n해당 자리의 학생에게 대화하면 됩니다.", ""));
        guideLogDictionary.Add(140101, new GuideLog(140101, "└ 영상을 보는 중에는 섣부르게\r\n   자극하지 않는 것을 추천.\r\n   방해한 n번 실종자에게 학생은\r\n   귀가 들리지 않을 때까지 소리를 지름.", ""));
        guideLogDictionary.Add(140200, new GuideLog(140200, "\r\n14-2. 모니터가 여러 개 켜져있고,\r\n꺼진 자리에 학생이 앉아있을 경우\r\n\r\n즉시 자리를 벗어나십시오.\r\n해당 상황에 대한 대처법은\r\n아직 확인된 것이 없습니다.\r\n\r\n", ""));

        guideLogDictionary.Add(150000, new GuideLog(150000, "15. 해당 학생과의 대화를 통해\r\n다음 목적지를 알아내셨다면,\r\n해당 목적지로 곧장 이동하십시오.\r\n\r\n반드시 이 과정 이전에 모든 볼일을\r\n마치시길 강력히 권고드립니다.\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n", ""));

        guideLogDictionary.Add(160000, new GuideLog(160000, "16. 목적지에 도착하면\r\n멍이 든 학생이 있을 것입니다.\r\n어떤 방법을 써서라도 이 학생에게\r\n화재의 원인을 받아내십시오.", ""));
        guideLogDictionary.Add(160100, new GuideLog(160100, "└ 이 때, 보건실에서 얻은 상처약이\r\n   큰 도움을 주는 것으로 확인되었습니다.", ""));
        guideLogDictionary.Add(160200, new GuideLog(160200, "└ 절대 강제로 뺏거나 자극하지 말 것.\r\n   최대한 자극하지 않는 방향으로 진행.\r\n   뺏으려 한 n번 실종자는 학생에게\r\n   강타당한 후 행방불명 상태.", ""));

        guideLogDictionary.Add(170000, new GuideLog(170000, "17. 학생에게 물건을 받았다면,\r\n곧바로 옥상 문으로 탈출하십시오.\r\n밖에서 뵙겠습니다.\r\n\r\n\r\n", ""));

        guideLogDictionary.Add(180000, new GuideLog(180000, "<b>부록</b>\r\n\r\n보건실 확인증", ""));
        guideLogDictionary.Add(180100, new GuideLog(180100, "\r\n학교 3층의 교실들을 수색하면\r\n하얀색 종이를 찾을 수 있습니다.\r\n해당 종이는 보건실 확인증으로\r\n사용할 수 있는 것이 확인되었습니다.\r\n\r\n", ""));

        guideLogDictionary.Add(190000, new GuideLog(190000, "상처 만들기", ""));
        guideLogDictionary.Add(190100, new GuideLog(190100, "\r\n학교 곳곳의 이형체들은 당신에게\r\n크고 작은 상처를 입힐 것입니다.\r\n다만, 너무 큰 상처는 탈출에\r\n지장을 줄 것입니다.\r\n\r\n", ""));

        guideLogDictionary.Add(200000, new GuideLog(200000, "학생회장에게 선물", ""));
        guideLogDictionary.Add(200100, new GuideLog(200100, "\r\n해당 학교 내부의 물건을 무단으로\r\n소지하면 모두 절도로 간주됩니다.\r\n반드시 학교 내의 이형체에게\r\n빌리거나 받는 것을 추천드립니다.", ""));
        guideLogDictionary.Add(200101, new GuideLog(200101, "└ 3층의 남학생 이형체가 주는 것을 확인.\r\n   해당 물품을 학생회장에게 선물 가능.\r\n\r\n   다만, 올바르게 대응하지 못할 시 자신의\r\n   이야기를 하기 시작함.\r\n   해당 이야기는 약 52시간정도 이어짐.\r\n   이야기를 다 들은 n번 실종자는\r\n   스스로 목숨을 끊음.", ""));

    }
}
