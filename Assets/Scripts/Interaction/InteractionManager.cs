using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    private static InteractionManager instance = null;

    public static InteractionManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    
    [SerializeField]
    private GameObject timerStartObject;

    [SerializeField]
    private GameObject timerEndObject;


    public void Init(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
    }
    
    
    public void SetTimerStartActive(bool active){
        timerStartObject.SetActive(active);
    }

    public void SetTimerEndActive(bool active){
        timerEndObject.SetActive(active);
    }
}
