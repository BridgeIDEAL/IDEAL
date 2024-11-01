using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Autodesk.Fbx.FbxAnimCurveDef;

public class GraffitiDeathTrigger : MonoBehaviour
{
    [SerializeField] int deathIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver(deathIndex);
        }
    }
}
