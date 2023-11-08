using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTypeStates
{
    public class Indifference : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("무관심 상태이다.");
            entity.SetAnimation(entity.CurrentType);
        }

        public override void Execute(DType entity)
        {
            if (!entity.CanInteraction) { return; }
            if (entity.DetectPlayer()) { entity.ChangeState(DTypeEntityStates.Watch); }
        }

        public override void Exit(DType entity)
        {
            Debug.Log("무관심 상태가 아니다.");
        }
    }
    public class Watch : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("관찰 상태이다.");
            entity.SetAnimation(entity.CurrentType);
        }

        public override void Execute(DType entity)
        {
            if (!entity.CanInteraction) { entity.ChangeState(DTypeEntityStates.Indifference); return; }
            entity.WatchPlayer();
        }

        public override void Exit(DType entity)
        {
            Debug.Log("관찰 상태가 아니다.");
        }
    }
    public class Interaction : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("상호작용 상태이다.");
            entity.SetAnimation(entity.CurrentType);
        }

        public override void Execute(DType entity)
        {
  
        }

        public override void Exit(DType entity)
        {
            Debug.Log("상호작용 상태가 아니다。");
        }
    }
    public class Aggressive : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("공격 대기 상태이다.");
            entity.SetAnimation(entity.CurrentType);
        }

        public override void Execute(DType entity)
        {
            if (!entity.CanInteraction) { return; }
        }

        public override void Exit(DType entity)
        {
            Debug.Log("공격 상태가 된다。");
        }
    }
    public class Chase : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("쫓는 상태이다.");
            entity.SetAnimation(entity.CurrentType);
        }

        public override void Execute(DType entity)
        {
            entity.ChasePlayer();
        }

        public override void Exit(DType entity)
        {
            Debug.Log("더이상 쫓지 않는다.");
        }
    }
}