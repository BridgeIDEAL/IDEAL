using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomCabinet : MonoBehaviour
{
    [SerializeField] Vector3 spawnPosition = new Vector3(0, 0.045f, 0);
    [SerializeField] GameObject interactionObject;

    public void SpawnNameTag(SetClassCabinet _setClassCabinet, ClassCabinetSpawnItem _cabinetItem)
    {
        string _itemName = _cabinetItem.ToString();
        GameObject loadGo = FabManager.Instance.LoadPrefab(_itemName);
        if (loadGo == null)
            return;
        GameObject go = Instantiate(loadGo, transform);
        go.transform.localPosition = spawnPosition;
        go.GetComponent<InteractionCleanItem>().SetCabinet = _setClassCabinet;
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
