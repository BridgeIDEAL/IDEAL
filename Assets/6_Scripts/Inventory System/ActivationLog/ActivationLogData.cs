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
        //// 아이템 획득, 사용, 피해 입기 등 ActivationLogManager에서 생성되는 정형화된 것을 제외하고 넣는 것들

        activationLogDictionary.Add(5, new ActivationLog(5, "테스트 문구 입니다. 오늘은 2024년 3월 9일 오전 11시 10분입니다. 저는 한찬희이고 개강을 했습니다. 아이 행복해~", "테스트 문구"));


        //// ------- 플레이어 관련 상처 및 피해, 특정 상대에게 피해를 입은 정도만 표시하는 게 아니라 문장 형태가 달라지므로 따로 저장
        activationLogDictionary.Add(1002, new ActivationLog(1002, "손바닥에서 약간 탄 냄새가 나는 것 같다.", "선생님께 양손 1씩 손상"));
        

        //// ------- 환경 관련              2001 부터 2999까지 할당
        activationLogDictionary.Add(2001, new ActivationLog(2001, "무언가 나올 것 같다.", "위험 공간 진입"));

        //// ------- 상호작용 관련
        activationLogDictionary.Add(4105, new ActivationLog(4105, "칠판에 글귀를 작성했다.", "분필로 칠판에 글씨 적음"));
    }
}
