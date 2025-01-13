using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimeCapsule : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private GameObject timeCapsuleDesk;
    [SerializeField] private GameObject[] timecapsuleObjects;

    private int index = 0;

    public void ShowTimeDesk(){
        rectTransform.sizeDelta = new Vector2(1450, 850);
        timeCapsuleDesk.SetActive(true);
        foreach(GameObject obj in timecapsuleObjects)
        {
            obj.SetActive(false);
        }
    }

    public void ShowTimeCapsule()
    {
        rectTransform.sizeDelta = new Vector2(1050 * 0.7f + 10.0f, 1400 * 0.7f + 10.0f);
        timeCapsuleDesk.SetActive(false);
        foreach(GameObject obj in timecapsuleObjects)
        {
            obj.SetActive(false);
        }
        timecapsuleObjects[index].SetActive(true);
    }

    public void NextTimeCapsule()
    {
        foreach(GameObject obj in timecapsuleObjects)
        {
            obj.SetActive(false);
        }
        index++;
        if (index >= timecapsuleObjects.Length)
        {
            index = 0;
        }
        timecapsuleObjects[index].SetActive(true);
    }
}
