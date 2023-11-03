using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTypeStates
{
    public class Indifference : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(CType entity)
        {
            Debug.Log("��� �������ϴ�.");
            if (entity.DetectPlayer())
                entity.ChangeState(EntityStates.Watch);
        }

        public override void Exit(CType entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
    public class Watch : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("���� �����̴�.");
            entity.transform.LookAt(entity.playerObject.transform);
        }

        public override void Execute(CType entity)
        {
            Debug.Log("��� �����Ѵ�.");
            if (!entity.CheckDistance())
                entity.ChangeState(EntityStates.Indifference);
        }

        public override void Exit(CType entity)
        {
            Debug.Log("���� ���°� �ƴϴ�.");
        }
    }

    public class Chase : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("�Ѵ� �����̴�.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(CType entity)
        {
            Debug.Log("��� �Ѵ����̴�.");
            entity.ChasePlayer();
        }

        public override void Exit(CType entity)
        {
            Debug.Log("���̻� ���� �ʴ´�.");
        }
    }

    public class Patrol : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("���� �����̴�.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(CType entity)
        {
            Debug.Log("��� �������̴�.");
        }

        public override void Exit(CType entity)
        {
            Debug.Log("���̻� ���� ���°� �ƴϴ�.");
        }
    }
}