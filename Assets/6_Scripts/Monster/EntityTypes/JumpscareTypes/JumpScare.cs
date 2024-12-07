using Cinemachine;
using UnityEngine;

public abstract class JumpScare : MonoBehaviour
{
    [SerializeField, Header("Death Scene Last Camera Position")] protected Transform jumpscareCamTransform;
    [SerializeField] protected GameObject jumpscareCharacter;
    protected CinemachineVirtualCamera virtualCam = null;
    [SerializeField, Header("Disable Mesh")] GameObject[] entityMeshObjests;

    public virtual void GameOver(int deathIndex) 
    {
        /************* Chan hee ***********************/
        /***** Put GameOver Camera Effect ********/
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOverWithVHSEffect(deathIndex);
    }

    /// <summary>
    /// Principal : Must Init PlayerTransform
    /// </summary>
    public virtual void ActiveJumpScare()
    {
        TurnOffUIs();
        FindFollowCameraNRelease();
        InActiveMeshObjects();
        SetCameraSetting();
    }

    public virtual void TurnOffUIs()
    {
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIManager.OnJumpScare();
    }

    public void FindFollowCameraNRelease()
    {
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.thirdPersonController.MoveLock = true;
        Camera.main.GetComponent<MainCamEffect>().SetActiveLightObject();

        if (virtualCam == null)
        {
            virtualCam = Camera.main.GetComponent<MainCamEffect>().GetFollowCam;
            //CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
            //if (brain != null && brain.ActiveVirtualCamera is CinemachineVirtualCamera vCam)
            //{
            //    virtualCam = vCam;
            //}
        }
        // Release Follow Cam
        if (virtualCam != null) 
        {
            virtualCam.Follow = null;
        }

        // Inactive Entities
        EntityDataManager.Instance.Controller.InActiveExceptOne(this.gameObject);
    }

    public abstract void SetCameraSetting();
    public virtual void InActiveMeshObjects()
    {
        int cnt = entityMeshObjests.Length;
        for(int i=0; i<cnt; i++)
        {
            entityMeshObjests[i].SetActive(false);
        }
    } 
}