using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDataReset : MonoBehaviour
{
    [SerializeField] GameObject endingCanvas;
    [SerializeField] GameObject endingCommand;

    void Awake()
    {
        if (endingCanvas.activeSelf) endingCanvas.SetActive(false);
        if(endingCommand.activeSelf) endingCommand.SetActive(false);


        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        int sourceCnt = sources.Length;
        for (int i = 0; i < sourceCnt; i++)
        {
            if (sources[i].mute == true)
                sources[i].mute = false;
        }

    }

    void Start()
    {
        if (SteamfeatureController.Instance.FeatureManager.EndingCreditData.ShowEndingCredit())
        {
            endingCanvas.SetActive(true);
            endingCommand.SetActive(true);
        }

        EntityDataManager.Instance.ResetData();
        EventDataManager.Instance.ClearEventDatas();


    }

    public void InActiveEndingCredit()
    {
        endingCanvas.SetActive(false);
        endingCommand.SetActive(false);
    }
}
