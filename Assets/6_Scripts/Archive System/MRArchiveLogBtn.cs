using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRArchiveLogBtn : MonoBehaviour
{
    [SerializeField] private GameObject redDotObject;

    // Update is called once per frame
    void Update()
    {
        if(MonsterArchiveLogManager.Instance.HasNewData() || RoomArchiveLogManager.Instance.HasNewData()){
            redDotObject.SetActive(true);
        }
        else{
            redDotObject.SetActive(false);
        }
    }
}
