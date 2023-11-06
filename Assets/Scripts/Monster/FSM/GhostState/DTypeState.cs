using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTypeStates
{
    public class Indifference : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(DType entity)
        {
            Debug.Log("��� �������ϴ�.");
            if (entity.DetectPlayer())
                entity.ChangeState(DTypeEntityStates.Watch);
        }

        public override void Exit(DType entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }

        public override bool OnMessage(DType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }
    public class Watch : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("���� �����̴�.");
            entity.transform.LookAt(entity.playerObject.transform);
        }

        public override void Execute(DType entity)
        {
            Debug.Log("��� �����Ѵ�.");
            if (!entity.CheckDistance())
                entity.ChangeState(DTypeEntityStates.Indifference);
        }

        public override void Exit(DType entity)
        {
            Debug.Log("���� ���°� �ƴϴ�.");
        }
        public override bool OnMessage(DType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Chase : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("�Ѵ� �����̴�.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(DType entity)
        {
            Debug.Log("��� �Ѵ����̴�.");
            entity.ChasePlayer();
        }

        public override void Exit(DType entity)
        {
            Debug.Log("���̻� ���� �ʴ´�.");
        }
        public override bool OnMessage(DType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Patrol : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("���� �����̴�.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(DType entity)
        {
            Debug.Log("��� �������̴�.");
        }

        public override void Exit(DType entity)
        {
            Debug.Log("���̻� ���� ���°� �ƴϴ�.");
        }
        public override bool OnMessage(DType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }
}