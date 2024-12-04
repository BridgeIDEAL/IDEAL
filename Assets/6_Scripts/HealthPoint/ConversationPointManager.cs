using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationPointManager : MonoBehaviour
{
    private static ConversationPointManager instance = null;
    public static ConversationPointManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    private int[] convPoint = new int[System.Enum.GetValues(typeof(DialogueSeriesCharacter)).Length];

    public void Init(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }

        InitConvPoint();
    }

    public void EnterAnotherSceneInit(bool isLobby){
        if(isLobby){
            InitConvPoint();
        }
        else{
        }
    }

    public void InitConvPoint(){
        for(int i = 0; i < convPoint.Length; i++){
            convPoint[i] = 1;
        }
    }


    public int GetConvPoint(DialogueSeriesCharacter dialogueSeries){
        return convPoint[(int)dialogueSeries];
    }

    public void AddConvPoint(DialogueSeriesCharacter dialogueSeries){
        convPoint[(int)dialogueSeries]++;
    }
}
