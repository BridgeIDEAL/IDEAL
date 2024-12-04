using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChalkBoardText : MonoBehaviour
{
    private static ChalkBoardText instance = null;

    public static ChalkBoardText Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    [SerializeField] private TextMeshPro chalkBoardTMP;
    [SerializeField] private string defaultText = "다음 시간";

    void Awake(){
        if(Instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
        ResetChalkBoardText();
    }

    public void SetChalkBoardText(string text_){
        chalkBoardTMP.text = text_;
    }

    public void ResetChalkBoardText(){
        chalkBoardTMP.text = defaultText;
    }
}
