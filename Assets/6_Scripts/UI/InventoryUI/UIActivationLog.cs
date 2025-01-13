using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActivationLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI logText;
    [SerializeField] private Image backgroundImage;

    private float activeAlpha = 0.8f;
    private float inactiveAlpha = 0.5f;
    private string defaultText = "";

    public UIActivationLogManager uIActivationLogManager;


    public void SetLogText(string _text){
        defaultText = _text;
        LogUpdate();
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

    public void LogUpdate(){
        string resultString = "";
        if(!uIActivationLogManager.noSpace && !uIActivationLogManager.randomSpace 
        && !uIActivationLogManager.reverseString && !uIActivationLogManager.reverseWord){
            logText.text = defaultText;
            return;
        }
        if(defaultText == "") return;
        string[] parseString = defaultText.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
        if(uIActivationLogManager.reverseWord){
            for (int i  = 0 ; i < parseString.Length; i++){
                string word_  = parseString[i];
                if(word_.Length > 2){
                    char firstChar = word_[0];
                    char lastChar = word_[word_.Length - 1];
                    string middle = word_.Substring(1, word_.Length - 2);

                    string reverseMiddle = new string(middle.Reverse().ToArray());

                    // 첫 글자, 섞인 중간 부분, 마지막 글자를 결합
                    string newWord = firstChar + reverseMiddle + lastChar;
                    parseString[i] = newWord;
                }
            }
        }

        if(uIActivationLogManager.reverseString){
            string[] reverseString = parseString.Reverse().ToArray();
            parseString = reverseString;
        }

        if(uIActivationLogManager.noSpace){
            for(int i = 0 ; i < parseString.Length; i++){
                resultString += parseString[i];
            }
        }
        else{
            for(int i = 0 ; i < parseString.Length; i++){
                resultString += parseString[i];
                if(i == parseString.Length - 1) break;
                resultString += " ";
            }
        }

        if(uIActivationLogManager.randomSpace){
            int x = 0;
            while(x < resultString.Length){
                x += Random.Range(1, 4);
                if(x >= resultString.Length) break;
                resultString = resultString.Insert(x, " ");
            }
        }
        
        logText.text = resultString;
    }

}
