using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastCutScene : LastObjects
{
    [SerializeField] GameObject timelineParent;
    [SerializeField] Camera cam;
    [SerializeField] CinemachineVirtualCamera virtualCam;
    [SerializeField] AudioListener listener;
    [SerializeField] GameObject front_light;
    private void Start()
    {
        if (isSameScene && EventDataManager.Instance.RingAfterSchoolBell)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EntityDataManager.Instance.Controller.InActiveInteractionEntities();
            EntityDataManager.Instance.Controller.DisableChaseGroup();

            if (SteamfeatureController.Instance.FeatureManager.Achievement06.isEnding == false)
            {
                SteamfeatureController.Instance.FeatureManager.Achievement06.isEnding = true;
                SteamfeatureController.Instance.FeatureManager.Achievement06.CheckAllConidtion();
            }

            SteamfeatureController.Instance.FeatureManager.EndingCreditData.isEnding = true;

            AudioSource[] sources = FindObjectsOfType<AudioSource>();
            int sourceCnt = sources.Length;
            for(int i=0; i<sourceCnt; i++)
            {
                sources[i].mute = true;
            }

            front_light.SetActive(true);

            IdealSceneManager.Instance.CurrentGameManager.scriptHub.thirdPersonController.MoveLock = true;
            

            cam.enabled = false;
            listener.enabled = false;
            virtualCam.enabled = false;

            timelineParent.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
