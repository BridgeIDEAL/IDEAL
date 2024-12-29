using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMRArchiveLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameTMP;
    [SerializeField] private GameObject redDotObject;

    private UIMRArchiveLogManager uIMRArchiveLogManager;
    public int logNum = -1;

    private float bigFontSize = 50.0f;
    private float normalFontSize = 40.0f;

    public void SetManager(UIMRArchiveLogManager manager, int num){
        uIMRArchiveLogManager = manager;
        logNum = num;
    }

    public void SetNameString(string str){
        NameTMP.text = str; 
    }
    public void SetFontBig(){
        NameTMP.fontSizeMax = bigFontSize;
        NameTMP.fontStyle = FontStyles.Bold;
    }

    public void SetFontNormal(){
        NameTMP.fontSizeMax = normalFontSize;
        NameTMP.fontStyle = FontStyles.Normal;
    }

    public void isClicked(){
        uIMRArchiveLogManager.ShowArchiveLog(logNum);
    }

    public void DestoryThisObject(){
        Destroy(this.gameObject);
    }

    public void ShowRedDot(bool isActive){
        redDotObject.SetActive(isActive);
    }
}
