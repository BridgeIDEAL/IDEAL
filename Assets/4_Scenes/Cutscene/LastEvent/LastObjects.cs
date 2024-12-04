using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LastObjects : MonoBehaviour
{
    [SerializeField] protected SceneNames activeSceneName;
    protected bool isSameScene = false;

    protected virtual void Awake()
    {
        if ((int)activeSceneName == SceneManager.GetActiveScene().buildIndex)
            isSameScene = true;
        else
            isSameScene = false;
    }

    public virtual void EnableObject()
    {
        if (isSameScene)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }
}
