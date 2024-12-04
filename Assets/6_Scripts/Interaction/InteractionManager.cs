using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    public UIInteraction uIInteraction;


    [Header("Test Scene Object")]
    [SerializeField] private GameObject timerStartObject;

    [SerializeField] private GameObject timerEndObject;

    
    [Header("1st Floor Object")]
    [SerializeField] private GameObject medicine_01F;
    [SerializeField] private InteractionConditionalDoor door_01F_NurseRoom;


    public void Init(){
        
    }
    
    
    public void SetTimerStartActive(bool active){
        timerStartObject.SetActive(active);
    }

    public void SetTimerEndActive(bool active){
        timerEndObject.SetActive(active);
    }

    public void Active_01F_Medicine(){
        medicine_01F.SetActive(true);
    }
}
