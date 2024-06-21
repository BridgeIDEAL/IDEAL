using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomCabinet : MonoBehaviour
{
    [SerializeField] Vector3 spawnPosition = new Vector3(0, 0.045f, 0);
    public void SpawnNameTag(GameObject _go)
    {
        GameObject go = Instantiate(_go, transform);
        go.transform.localPosition = spawnPosition;
    }
}
