using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICheckList : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI logText;
    [SerializeField] private Image checkImage;

    public void SetLogText(string _text){
        logText.text = _text;
    }

    public void SetChecked(){
        checkImage.gameObject.SetActive(true);
        logText.text = "<color=#272727><s>" + logText.text + "</s></color>";
    }
}
