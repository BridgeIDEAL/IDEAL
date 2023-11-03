using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTypeStates
{
    public class Indifference : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("무관심 상태이다.");
        }

        public override void Execute(BType entity)
        {
            //Debug.Log("계속 무관심하다.");
            if (entity.DetectPlayer())
                entity.ChangeState(EntityStates.Watch);
        }

        public override void Exit(BType entity)
        {
            Debug.Log("무관심 상태가 아니다.");
        }
    }
    public class Watch : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("관찰 상태이다.");
            entity.transform.LookAt(entity.playerObject.transform);
        }

        public override void Execute(BType entity)
        {
            //Debug.Log("계속 관찰한다.");
            if (!entity.CheckDistance())
                entity.ChangeState(EntityStates.Chase);
        }

        public override void Exit(BType entity)
        {
            Debug.Log("관찰 상태가 아니다.");
        }
    }

    public class Chase : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("쫓는 상태이다.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(BType entity)
        {
            //Debug.Log("계속 쫓는중이다.");
            entity.ChasePlayer();
        }

        public override void Exit(BType entity)
        {
            Debug.Log("더이상 쫓지 않는다.");
        }
    }

    public class Patrol : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("순찰 상태이다.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(BType entity)
        {
            //Debug.Log("계속 순찰중이다.");
        }

        public override void Exit(BType entity)
        {
            Debug.Log("더이상 순찰 상태가 아니다.");
        }
    }
}

