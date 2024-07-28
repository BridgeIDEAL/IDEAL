using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIArchiveLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTag;
    [SerializeField] private TextMeshProUGUI archiveText;


    public void SetNameTag(int attempt, string state){
        nameTag.text = $"{attempt}번 실종자: " + state;
    }

    public void SetArchiveText(string text){
        archiveText.text = text;
    }

}
