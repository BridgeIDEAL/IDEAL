using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInteraction : MonoBehaviour
{
    private static ActiveInteraction instance = null;
    public static ActiveInteraction Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    void Awake(){
        if(Instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
    }
    [SerializeField] private GameObject medicine_01F;
    [SerializeField] private GameObject mapBook_01F;
    [SerializeField] private GameObject mapGuide_01F;

    public void Active_01F_Medicine(){
        medicine_01F.SetActive(true);
    }

    public void Active_01F_MapBook(){
        mapBook_01F.SetActive(true);
    }

    public void Active_01F_MapGuide(bool active){
        mapGuide_01F.SetActive(active);
    }
}
