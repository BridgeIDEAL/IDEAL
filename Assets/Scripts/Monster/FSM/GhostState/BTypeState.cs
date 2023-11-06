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
        }

        public override void Exit(BType entity)
        {
            Debug.Log("무관심 상태가 아니다.");
        }

        public override bool OnMessage(BType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }
    public class Interaction : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("상호작용 상태이다.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(BType entity) { }

        public override void Exit(BType entity)
        {
            Debug.Log("상호작용을 종료한다.");
        }
        public override bool OnMessage(BType entity, bool interaction)
        {
            if (interaction)
            {
                Debug.Log("상호작용 성공!");
                entity.ChangeState(BTypeEntityStates.Indifference);
            }
            else
            {
                Debug.Log("상호작용 실패!");
                entity.ChangeState(BTypeEntityStates.Aggressive);
            }
            return interaction;
        }
    }

    public class Aggressive : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("공격 대기중");
        }

        public override void Execute(BType entity){ }

        public override void Exit(BType entity)
        {
            Debug.Log("추격을 하기 위한 준비가 완료된 상태이다.");
        }
        public override bool OnMessage(BType entity, bool interaction)
        {
            throw new System.NotImplementedException();
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
            entity.ChasePlayer();
        }

        public override void Exit(BType entity)
        {
            Debug.Log("더이상 쫓지 않는다.");
        }
        public override bool OnMessage(BType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }
}

