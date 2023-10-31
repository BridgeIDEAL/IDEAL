using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiddleLevelStates
{
    public class Indifference : State<MiddleLv>
    {
        public override void Enter(MiddleLv entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(MiddleLv entity)
        {
            Debug.Log("��� �������ϴ�.");
            if (entity.DetectPlayer())
                entity.ChangeState(EntityStates.Watch);
        }

        public override void Exit(MiddleLv entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
    public class Watch : State<MiddleLv>
    {
        public override void Enter(MiddleLv entity)
        {
            Debug.Log("���� �����̴�.");
            entity.transform.LookAt(entity.playerObject.transform);
        }

        public override void Execute(MiddleLv entity)
        {
            Debug.Log("��� �����Ѵ�.");
            if (!entity.CheckDistance())
                entity.ChangeState(EntityStates.Indifference);
        }

        public override void Exit(MiddleLv entity)
        {
            Debug.Log("���� ���°� �ƴϴ�.");
        }
    }

    public class Chase : State<MiddleLv>
    {
        public override void Enter(MiddleLv entity)
        {
            Debug.Log("�Ѵ� �����̴�.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(MiddleLv entity)
        {
            Debug.Log("��� �Ѵ����̴�.");
            entity.ChasePlayer();
        }

        public override void Exit(MiddleLv entity)
        {
            Debug.Log("���̻� ���� �ʴ´�.");
        }
    }

    public class Patrol : State<MiddleLv>
    {
        public override void Enter(MiddleLv entity)
        {
            Debug.Log("���� �����̴�.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(MiddleLv entity)
        {
            Debug.Log("��� �������̴�.");
        }

        public override void Exit(MiddleLv entity)
        {
            Debug.Log("���̻� ���� ���°� �ƴϴ�.");
        }
    }
}

