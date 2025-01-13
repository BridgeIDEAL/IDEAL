using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIArchiveLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTag;
    [SerializeField] private TextMeshProUGUI archiveText;


    public void SetNameTag(int attempt, string state){
        nameTag.text = $"{attempt}번 실종자: ";
        switch(state){
            case "실종":
                nameTag.text += "<color=#C31D1D>";
                break;
            case "사망":
                nameTag.text += "<color=#000000>";
                break;
            case "생존":
                nameTag.text += "<color=#2BAE24>";
                break;
        }
        nameTag.text += state;
        nameTag.text += "</color>";
    }

    public void SetArchiveText(string text){
        archiveText.text = text;
    }

}
