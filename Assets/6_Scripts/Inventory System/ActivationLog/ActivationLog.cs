using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationLog
{
    private int ID;
    private string contentText;
    private string descText;
    public bool  isObjectViewed = false;

    public ActivationLog(int _ID, string _contentText, string _descText){
        this.ID = _ID;
        this.contentText = _contentText;
        this.descText = _descText;
    }

    public int GetID(){
        return this.ID;
    }

    public string GetContentText(){
        return this.contentText;
    }

    public string GetDescText(){
        return this.descText;
    }
}
