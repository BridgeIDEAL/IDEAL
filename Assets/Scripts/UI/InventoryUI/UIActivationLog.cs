using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActivationLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI logText;
    [SerializeField] private Image backgroundImage;

    private float activeAlpha = 0.8f;
    private float inactiveAlpha = 0.5f;

    public void SetLogText(string _text){
        logText.text = _text;
    }

    public void SetImageActive(bool active){
        Color _color = backgroundImage.color;
        if(active){
            _color.a = activeAlpha;
        }
        else{
            _color.a = inactiveAlpha;
        }
        backgroundImage.color = _color;
    }

}
