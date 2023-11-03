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
                entity.ChangeState(EntityStates.Watch);
        }

        public override void Exit(DType entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
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
                entity.ChangeState(EntityStates.Indifference);
        }

        public override void Exit(DType entity)
        {
            Debug.Log("���� ���°� �ƴϴ�.");
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
    }
}