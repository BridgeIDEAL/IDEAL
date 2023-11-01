using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiddleLevelStates
{
    public class Indifference : State<MiddleLv>
    {
        public override void Enter(MiddleLv entity)
        {
            Debug.Log("무관심 상태이다.");
        }

        public override void Execute(MiddleLv entity)
        {
            Debug.Log("계속 무관심하다.");
            if (entity.DetectPlayer())
                entity.ChangeState(EntityStates.Watch);
        }

        public override void Exit(MiddleLv entity)
        {
            Debug.Log("무관심 상태가 아니다.");
        }
    }
    public class Watch : State<MiddleLv>
    {
        public override void Enter(MiddleLv entity)
        {
            Debug.Log("관찰 상태이다.");
            entity.transform.LookAt(entity.playerObject.transform);
        }

        public override void Execute(MiddleLv entity)
        {
            Debug.Log("계속 관찰한다.");
            if (!entity.CheckDistance())
                entity.ChangeState(EntityStates.Indifference);
        }

        public override void Exit(MiddleLv entity)
        {
            Debug.Log("관찰 상태가 아니다.");
        }
    }

    public class Chase : State<MiddleLv>
    {
        public override void Enter(MiddleLv entity)
        {
            Debug.Log("쫓는 상태이다.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(MiddleLv entity)
        {
            Debug.Log("계속 쫓는중이다.");
            entity.ChasePlayer();
        }

        public override void Exit(MiddleLv entity)
        {
            Debug.Log("더이상 쫓지 않는다.");
        }
    }

    public class Patrol : State<MiddleLv>
    {
        public override void Enter(MiddleLv entity)
        {
            Debug.Log("순찰 상태이다.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(MiddleLv entity)
        {
            Debug.Log("계속 순찰중이다.");
        }

        public override void Exit(MiddleLv entity)
        {
            Debug.Log("더이상 순찰 상태가 아니다.");
        }
    }
}

