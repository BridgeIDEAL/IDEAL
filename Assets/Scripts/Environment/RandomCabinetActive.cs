using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCabinetActive : MonoBehaviour
{
    public GameObject interactionGo;
    public Transform textBookTransform;
    public void ActiveInteraction()
    {
        interactionGo.SetActive(true);
    }
}
