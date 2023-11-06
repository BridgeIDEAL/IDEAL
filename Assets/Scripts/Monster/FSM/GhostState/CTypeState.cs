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

        public override void Execute(CType entity)
        {
            Debug.Log("계속 무관심하다.");
            if (entity.DetectPlayer())
                entity.ChangeState(CTypeEntityStates.Watch);
        }

        public override void Exit(CType entity)
        {
            Debug.Log("무관심 상태가 아니다.");
        }

        public override bool OnMessage(CType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }
    public class Watch : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("관찰 상태이다.");
            entity.transform.LookAt(entity.playerObject.transform);
        }

        public override void Execute(CType entity)
        {
            Debug.Log("계속 관찰한다.");
            if (!entity.CheckDistance())
                entity.ChangeState(CTypeEntityStates.Indifference);
        }

        public override void Exit(CType entity)
        {
            Debug.Log("관찰 상태가 아니다.");
        }
        public override bool OnMessage(CType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Chase : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("쫓는 상태이다.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(CType entity)
        {
            Debug.Log("계속 쫓는중이다.");
            entity.ChasePlayer();
        }

        public override void Exit(CType entity)
        {
            Debug.Log("더이상 쫓지 않는다.");
        }
        public override bool OnMessage(CType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Patrol : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("순찰 상태이다.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(CType entity)
        {
            Debug.Log("계속 순찰중이다.");
        }

        public override void Exit(CType entity)
        {
            Debug.Log("더이상 순찰 상태가 아니다.");
        }
        public override bool OnMessage(CType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }
}