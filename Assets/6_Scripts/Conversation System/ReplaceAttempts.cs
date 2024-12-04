using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReplaceAttempts : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentsText;
    private int attempt = -1;

    // Update is called once per frame
    void Update()
    {
        // $attempt가 포함되어 있다면 해당 단어를 시도 횟수로 대체함 
        if(contentsText.text.Contains("$attempt")){
            attempt = -1;
            if(CountAttempts.Instance != null){
                attempt = CountAttempts.Instance.GetAttemptCount();
            }

            contentsText.text = contentsText.text.Replace("$attempt", attempt.ToString());
        }
    }
}
