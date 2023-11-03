using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTypeStates
{
    public class Indifference : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(BType entity)
        {
            //Debug.Log("��� �������ϴ�.");
            if (entity.DetectPlayer())
                entity.ChangeState(EntityStates.Watch);
        }

        public override void Exit(BType entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
    public class Watch : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("���� �����̴�.");
            entity.transform.LookAt(entity.playerObject.transform);
        }

        public override void Execute(BType entity)
        {
            //Debug.Log("��� �����Ѵ�.");
            if (!entity.CheckDistance())
                entity.ChangeState(EntityStates.Chase);
        }

        public override void Exit(BType entity)
        {
            Debug.Log("���� ���°� �ƴϴ�.");
        }
    }

    public class Chase : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("�Ѵ� �����̴�.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(BType entity)
        {
            //Debug.Log("��� �Ѵ����̴�.");
            entity.ChasePlayer();
        }

        public override void Exit(BType entity)
        {
            Debug.Log("���̻� ���� �ʴ´�.");
        }
    }

    public class Patrol : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("���� �����̴�.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(BType entity)
        {
            //Debug.Log("��� �������̴�.");
        }

        public override void Exit(BType entity)
        {
            Debug.Log("���̻� ���� ���°� �ƴϴ�.");
        }
    }
}

