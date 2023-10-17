using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public UIManager uIManager;

    
    [SerializeField]
    private GameObject timerStartObject;

    [SerializeField]
    private GameObject timerEndObject;


    public void SetTimerStartActive(bool active){
        timerStartObject.SetActive(active);
    }

    public void SetTimerEndActive(bool active){
        timerEndObject.SetActive(active);
    }
}
