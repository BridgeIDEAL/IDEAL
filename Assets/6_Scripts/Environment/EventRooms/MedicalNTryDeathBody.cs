using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalNTryDeathBody : MonoBehaviour
{
    enum DeathBody
    {
        NotExist,
        Exist
    }
    [SerializeField, Tooltip("0: Not, 1: Exist")] GameObject[] mannequins;

    private void Start()
    {
        if (CountAttempts.Instance.GetAttemptCount() == 1)
        {
            mannequins[(int)DeathBody.NotExist].gameObject.SetActive(true);
            mannequins[(int)DeathBody.Exist].gameObject.SetActive(false);
        }
        else
        {
            mannequins[(int)DeathBody.NotExist].gameObject.SetActive(false);
            mannequins[(int)DeathBody.Exist].gameObject.SetActive(true);
        }
    }
}
