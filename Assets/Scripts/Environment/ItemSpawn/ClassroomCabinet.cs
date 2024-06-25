using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomCabinet : MonoBehaviour
{
    [SerializeField] Vector3 spawnPosition = new Vector3(0, 0.045f, 0);
    [SerializeField] GameObject interactionObject;

    public void SpawnNameTag()
    {
        GameObject loadGo = IdealSceneManager.Instance.CurrentGameManager.Fab_Manager.LoadPrefab("NameTag");
        if (loadGo == null)
            return;
        GameObject go = Instantiate(loadGo, transform);
        go.transform.localPosition = spawnPosition;
        ActiveInteraction();
    }

    public void ActiveInteraction()
    {
        if (interactionObject == null)
            return;
        interactionObject.SetActive(true);
    }

    public void RemoveThis()
    {
        Destroy(this);
    }
}
