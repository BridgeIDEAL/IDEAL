using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTypeStates
{
    public class Indifference : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("무관심 상태이다.");
        }

        public override void Execute(CType entity){
            if (!entity.CanInteraction) { return; }
            if (entity.DetectPlayer()) { entity.ChangeState(CTypeEntityStates.Watch); } 
        }

        public override void Exit(CType entity)
        {
            Debug.Log("무관심 상태가 아니다.");
        }
    }
    public class Watch : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("관찰 상태이다.");
            entity.StartTimer();
        }

        public override void Execute(CType entity)
        {
            if (!entity.CanInteraction) { entity.ChangeState(CTypeEntityStates.Indifference); return; }
            entity.WatchPlayer();
        }

        public override void Exit(CType entity)
        {
            Debug.Log("관찰 상태가 아니다.");
        } 
    }

    public class Interaction : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("상호작용을 시작합니다.");
        }

        public override void Execute(CType entity)
        {
        }

        public override void Exit(CType entity)
        {
            Debug.Log("상호작용을 종료합니다.");
        }
    }
}