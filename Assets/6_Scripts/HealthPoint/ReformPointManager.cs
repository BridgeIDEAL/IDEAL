using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReformPointManager : MonoBehaviour
{
    private static ReformPointManager instance;
    public static ReformPointManager Instance{
        get {
            if(instance == null){
                return null;
            }
            return instance;
        }
    }

    private int reformPoint = 0;
    public static int minRP = 0;

    public ScriptHub scriptHub;

    public void Init(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }

        InitReformPoint();
    }

    public void EnterAnotherSceneInit(bool isLobby){
        if(isLobby){
            InitReformPoint();
        }
        else{
        }
    }

    private void InitReformPoint(){
        reformPoint = minRP;
    }

    public int GetReformPoint(){
        return reformPoint;
    }

    public void AddReformPoint(int addPoint = 0){
        reformPoint += addPoint;
    }
}
