using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndingCredit : MonoBehaviour
{
    [SerializeField] float increaseSpeed;
    [SerializeField] RectTransform developerText;
    [SerializeField] RectTransform resourceText;
    [SerializeField] RectTransform endingText;
    [SerializeField] Text theEnd;
    [SerializeField] GameObject pressKeyTextObject;
    
    Color theEndColor;

    bool isEnding = false;

    [SerializeField] EntityDataReset reset;

    private void Start()
    {
        if(pressKeyTextObject.activeSelf) pressKeyTextObject.SetActive(false);
        theEndColor = theEnd.color;
        theEndColor.a =0f;
        theEnd.color = theEndColor;

        StartCoroutine(CCredit(developerText));
        StartCoroutine(CCredit(resourceText));
        StartCoroutine(CCreditEnding(endingText));
    }

    IEnumerator CCredit(RectTransform rect)
    {
        while(true)
        {
            if(rect.anchoredPosition.y > 1600f)
            {
                rect.gameObject.SetActive(false);
                yield break;
            }
            rect.anchoredPosition += Vector2.up * increaseSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator CCreditEnding(RectTransform rect)
    {
        while (true)
        {
            if (rect.anchoredPosition.y > 200f)
            {
                StartCoroutine(TextOpaqueColor());
                yield break;
            }
            rect.anchoredPosition += Vector2.up * increaseSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator TextOpaqueColor()
    {
        float time = 0f;
        while (time < 2f)
        {
            time += Time.deltaTime;
            theEndColor.a = Mathf.Lerp(0, 1f, time / 2f);
            theEnd.color = theEndColor;
            yield return null;
        }
        theEnd.color = theEndColor;
        pressKeyTextObject.SetActive(true);
        isEnding = true;
        // 게임 종료
    }

    private void Update()
    {
        if (isEnding)
        {
            if (Input.anyKeyDown)
            {
                isEnding = false;
                reset?.InActiveEndingCredit();
            }
        }
    }
}


