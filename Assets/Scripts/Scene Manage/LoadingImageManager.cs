using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingImageManager : MonoBehaviour
{
    private static LoadingImageManager instance;
    public static LoadingImageManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    [SerializeField] private GameObject loadingImageObject;
    public Image fadeFilter;

    private void Awake(){
        if(Instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    public void SetActiveLoadingImage(bool active){
        loadingImageObject.SetActive(active);
    }
}
