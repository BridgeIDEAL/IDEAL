using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCabinetSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] cabinets;
    [SerializeField] GameObject textBook;
    int cabinetCount = 0;
    private void Awake()
    {
        cabinetCount = cabinets.Length;
        int randomInt = Random.Range(0, cabinetCount);
        Instantiate(textBook);
        textBook.transform.parent = cabinets[randomInt].transform;
        textBook.transform.position = new Vector3(0f, 0.05f, 0f);
    }
}
