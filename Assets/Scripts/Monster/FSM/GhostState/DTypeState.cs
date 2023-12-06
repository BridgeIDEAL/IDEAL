using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTypeStates
{
    public class Indifference : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("무관심");
            entity.SetAnimation(entity.CurrentType);
        }

        public override void Execute(DType entity)
        {
            if (!GameManager.EntityEvent.CanInteraction) { return; }
            if (entity.DetectPlayer()) { entity.ChangeState(DTypeEntityStates.Watch); }
        }

        public override void Exit(DType entity){ }
    }
    public class Watch : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("관심");
            entity.SetAnimation(entity.CurrentType);
        }

        public override void Execute(DType entity)
        {
            if (!GameManager.EntityEvent.CanInteraction) { entity.ChangeState(DTypeEntityStates.Indifference); return; }
            entity.WatchPlayer();
        }

        public override void Exit(DType entity){ }
    }
    public class Interaction : State<DType>
    {
        public override void Enter(DType entity)
        {
            entity.SetAnimation(entity.CurrentType);
        }

        public override void Execute(DType entity){ }

        public override void Exit(DType entity){ }
    }
    public class Aggressive : State<DType>
    {
        public override void Enter(DType entity)
        {
            entity.SetAnimation(entity.CurrentType);
        }

        public override void Execute(DType entity){ }

        public override void Exit(DType entity){ }
    }
    public class Chase : State<DType>
    {
        public override void Enter(DType entity)
        {
            entity.SetAnimation(entity.CurrentType);
        }

        public override void Execute(DType entity)
        {
            entity.ChasePlayer();
        }

        public override void Exit(DType entity){ }
    }
}