using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATypeStates
{
    public class Indifference : State<AType>
    {
        public override void Enter(AType entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(AType entity)
        {
            Debug.Log("��� �������ϴ�.");
            if (entity.DetectPlayer())
                entity.ChangeState(EntityStates.Watch);
        }

        public override void Exit(AType entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
    public class Watch : State<AType>
    {
        public override void Enter(AType entity)
        {
            Debug.Log("���� �����̴�.");
            entity.transform.LookAt(entity.playerObject.transform);
        }

        public override void Execute(AType entity)
        {
            Debug.Log("��� �����Ѵ�.");
            if (!entity.CheckDistance())
                entity.ChangeState(EntityStates.Indifference);
        }

        public override void Exit(AType entity)
        {
            Debug.Log("���� ���°� �ƴϴ�.");
        }
    }

    public class Chase : State<AType>
    {
        public override void Enter(AType entity)
        {
            Debug.Log("�Ѵ� �����̴�.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(AType entity)
        {
            Debug.Log("��� �Ѵ����̴�.");
            entity.ChasePlayer();   
        }

        public override void Exit(AType entity)
        {
            Debug.Log("���̻� ���� �ʴ´�.");
        }
    }

    public class Patrol : State<AType>
    {
        public override void Enter(AType entity)
        {
            Debug.Log("���� �����̴�.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(AType entity)
        {
            Debug.Log("��� �������̴�.");
        }

        public override void Exit(AType entity)
        {
            Debug.Log("���̻� ���� ���°� �ƴϴ�.");
        }
    }
}

