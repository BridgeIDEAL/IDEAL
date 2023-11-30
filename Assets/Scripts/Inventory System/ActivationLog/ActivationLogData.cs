using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationLogData : MonoBehaviour
{
    Dictionary <int, ActivationLog> activationLogDictionary;    // ActivationLog 저장 Dictionary
    
    public ActivationLog GetActivationLog(int _ID){
        if(activationLogDictionary.ContainsKey(_ID)){
            ActivationLog activationLog = activationLogDictionary[_ID];
            return activationLog;
        }
        else{
            return new ActivationLog(-1, "None", "None");
        }
    }

    public void Init(){
        activationLogDictionary = new Dictionary<int, ActivationLog> ();

        GenerateActivationLog();
    }


    private void GenerateActivationLog(){
        //// ------- 플레이어 상태 관련     1001 부터 1999까지 할당
        /// --------  정신상태 양호 1001부터 1199까지
        activationLogDictionary.Add(1001, new ActivationLog(1001, "팔이 욱씬거린다.", "팔 손상, 정신 상태 양호"));
        activationLogDictionary.Add(1002, new ActivationLog(1002, "손바닥에서 약간 탄 냄새가 나는 것 같다.", "양손 1씩 손상"));


        /// -------- 정신상태 불안정 1201부터 1399까지
        activationLogDictionary.Add(1201, new ActivationLog(1201, "눈 두 개 다리 두 개 팔 하나. 다 있다.", "팔 손상, 정신상태 불안정"));




        //// ------- 환경 관련              2001 부터 2999까지 할당
        activationLogDictionary.Add(2001, new ActivationLog(2001, "무언가 나올 것 같다.", "위험 공간 진입"));
        
        
        

        //// ------- 이형체 상호작용 관련       3001 부터 3999까지 할당
        activationLogDictionary.Add(3001, new ActivationLog(3001, "이형체를 발견했다.", "이형체 발견"));
        activationLogDictionary.Add(3002, new ActivationLog(3002, "이형체가 만족한 것 같다.", "이형체 만족"));
        

        //// ------- 오브젝트 상호작용 관련     4001 부터 4999까지 할당
        activationLogDictionary.Add(4001, new ActivationLog(4001, "물 맛이 이상하다.", "상한 물 섭취"));
        activationLogDictionary.Add(4002, new ActivationLog(4002, "포션을 획득했다.", "포션 획득"));
        activationLogDictionary.Add(4003, new ActivationLog(4003, "포션을 사용했다.", "포션 사용"));
        activationLogDictionary.Add(4004, new ActivationLog(4004, "손전등을 획득했다.", "손전등 획득"));


        activationLogDictionary.Add(4101, new ActivationLog(4101, "교무센터 열쇠를 획득했다.", "교무센터 열쇠 획득"));
        activationLogDictionary.Add(4102, new ActivationLog(4102, "교무센터 열쇠를 사용했다.", "교무센터 열쇠 사용"));

        activationLogDictionary.Add(4103, new ActivationLog(4103, "1학년 교무실 열쇠를 획득했다.", "1학년 교무실 열쇠 획득"));
        activationLogDictionary.Add(4104, new ActivationLog(4104, "1학년 교무실 열쇠를 사용했다.", "1학년 교무실 열쇠 사용"));

        activationLogDictionary.Add(4105, new ActivationLog(4105, "칠판에 글귀를 작성했다.", "분필로 칠판에 글씨 적음"));

        activationLogDictionary.Add(4106, new ActivationLog(4106, "알코올램프를 획득했다.", "알코올램프 획득"));

        activationLogDictionary.Add(4107, new ActivationLog(4107, "옥상문 열쇠를 획득했다.", "옥상문 열쇠 획득"));
    }
}
