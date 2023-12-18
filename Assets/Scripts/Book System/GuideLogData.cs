using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideLogData : MonoBehaviour
{
    // 가이드 ID는 0A0B0C로 대 중 소로 구분되며 각 단위에서 99개까지 가능
    Dictionary <int, GuideLog> guideLogDictionary; // GuideLog 저장 Dictionary


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
        guideLogDictionary.Add(010000, new GuideLog(010000, "1. 교무센터를 해금해야 한다.", "교무센터 해금 권유"));
        //// ---   1-1-0
        guideLogDictionary.Add(010100, new GuideLog(010100, "1-1. 경비원이 돌아다닐 때마다 열쇠소리가 찰랑거린다.", "경비원 힌트 제공"));
        //// ----- 1-1-1 
        guideLogDictionary.Add(010101, new GuideLog(010101, "1-1-1. 경비원을 따라 경비실에 가면 죽는다.", "경비원 힌트 제공"));
        //// ----- 1-1-2
        guideLogDictionary.Add(010102, new GuideLog(010102, "1-1-2. 경비원을 따라 경비실에 가지 않으면 죽는다.", "경비원 힌트 제공"));
        //// ----- 1-1-3
        guideLogDictionary.Add(010103, new GuideLog(010103, "1-1-3. 경비원을 따라 경비실에 가지 않은 $attempt번 실종자는 죽었다.", "경비원 힌트 제공"));



        //// -  2-0-0
        guideLogDictionary.Add(020000, new GuideLog(020000, "2. 1학년 교무실을 해금해야 한다.", "1학년 교무실 해금 권유"));
    }
}
