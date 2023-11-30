using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CountAttempts : MonoBehaviour
{
    private static CountAttempts instance = null;
    public static CountAttempts Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    private int attemptCount = 0;

    private void Awake(){
        if(Instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    public void AddAttemptCount(){
        attemptCount++;
    }

    public int GetAttemptCount(){
        return attemptCount;
    }
}
