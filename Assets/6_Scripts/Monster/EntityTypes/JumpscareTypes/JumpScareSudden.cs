using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SuddenType
{
    FemaleTeacher,
    BoyStudent
}

public class JumpScareSudden : JumpScare
{
    [SerializeField] SuddenType type;
    public override void SetCameraSetting()
    {
        virtualCam.transform.rotation = jumpscareCamTransform.rotation;
        virtualCam.transform.position = jumpscareCamTransform.position;
        jumpscareCharacter.gameObject.SetActive(true);
    }

    public void SetPosition(Vector3 setPosition, Quaternion rotation)
    {
        jumpscareCharacter.transform.position = setPosition;
        jumpscareCharacter.transform.rotation = rotation;

        switch(type)
        {
            case SuddenType.FemaleTeacher:
                if (SteamfeatureController.Instance.FeatureManager.Achievement03.isOnGuardDeath_F == false)
                {
                    SteamfeatureController.Instance.FeatureManager.Achievement03.isOnGuardDeath_F = true;
                    SteamfeatureController.Instance.FeatureManager.Achievement03.CheckAllConidtion();
                }
                break;
            case SuddenType.BoyStudent:
                if (SteamfeatureController.Instance.FeatureManager.Achievement03.isOnGuardDeath_B == false)
                {
                    SteamfeatureController.Instance.FeatureManager.Achievement03.isOnGuardDeath_B = true;
                    SteamfeatureController.Instance.FeatureManager.Achievement03.CheckAllConidtion();
                }
                break;
        }
    }
}
