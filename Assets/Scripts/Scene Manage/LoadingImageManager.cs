using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake(){
        if(Instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
    }

    public void ActiveLoadingImage(){
        loadingImageObject.SetActive(true);
    }

    public void DeleteLoadingCanvas(){
        Destroy(this.gameObject);
    }
}
