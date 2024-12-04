using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectController : MonoBehaviour
{
    
    [SerializeField] private GameObject[] SceneObjects;

    public void SceneObjectsSetActive(bool active){
        for(int i = 0; i < SceneObjects.Length; i++){
            SceneObjects[i].SetActive(active);
        }
    }
}
