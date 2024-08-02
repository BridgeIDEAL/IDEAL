using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGuardEntity : ImmovableEntity
{
    [SerializeField] float thresholdAngle= 90f;
    [SerializeField] DetectPlayer detectPlayer;
    [SerializeField] bool isOnGuard = true;
    public bool IsOnGuard { get { return isOnGuard; } set { isOnGuard = value; } }


    public override void Init(Transform _playerTransfrom)
    {
        base.Init(_playerTransfrom);
        detectPlayer = GetComponentInChildren<DetectPlayer>();
    }

    public override void Setup()
    {
        base.Setup();
        if (Entity_Data.speakIndex == -1)
            isOnGuard = false;
        else
            isOnGuard = true;
    }
    public override void IdleEnter() { SetAnimation(currentType, true); }
    public override void IdleExecute() { if (detectPlayer.DetectExecute() && isOnGuard) { ChangeState(EntityStateType.Penalty); } }
    public override void IdleExit() { SetAnimation(currentType, false); }
    public override void TalkEnter() { SetAnimation(currentType, true); isOnGuard = false; }
    public override void TalkExecute() { }
    public override void TalkExit() { SetAnimation(currentType, false); lookPlayer.GazeFront(); }
    public override void QuietEnter() { SetAnimation(currentType, true); }
    public override void QuietExecute() { }
    public override void QuietExit() { SetAnimation(currentType, false); }
    public override void PenaltyEnter() { SetAnimation(currentType, true); lookPlayer.GazePlayer(controller.lookTransform); }
    public override void PenaltyExecute() { if (!detectPlayer.DetectExecute() && onceSpawn) { SpawnFrontPlayer();} }
    public override void PenaltyExit() { SetAnimation(currentType, false); }

    [Header("Immediately Info")]
    public GameObject immediatelyDeathObject;
    public Vector3 spawnPosition;
    bool onceSpawn = true;
    public void SpawnFrontPlayer()
    {
        onceSpawn = false;
        GameObject spawnEntity = Instantiate(immediatelyDeathObject);
        spawnEntity.transform.position = spawnPosition;
    }
}
