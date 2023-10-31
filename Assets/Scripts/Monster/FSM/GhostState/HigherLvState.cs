using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HigherLevelStates
{
    public class Indifference : State<HigherLv>
    {
        public override void Enter(HigherLv entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(HigherLv entity)
        {
            Debug.Log("��� �������ϴ�.");
            if (entity.DetectPlayer())
                entity.ChangeState(EntityStates.Watch);
        }

        public override void Exit(HigherLv entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
    public class Watch : State<HigherLv>
    {
        public override void Enter(HigherLv entity)
        {
            Debug.Log("���� �����̴�.");
            entity.transform.LookAt(entity.playerObject.transform);
        }

        public override void Execute(HigherLv entity)
        {
            Debug.Log("��� �����Ѵ�.");
            if (!entity.CheckDistance())
                entity.ChangeState(EntityStates.Indifference);
        }

        public override void Exit(HigherLv entity)
        {
            Debug.Log("���� ���°� �ƴϴ�.");
        }
    }

    public class Chase : State<HigherLv>
    {
        public override void Enter(HigherLv entity)
        {
            Debug.Log("�Ѵ� �����̴�.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(HigherLv entity)
        {
            Debug.Log("��� �Ѵ����̴�.");
            entity.ChasePlayer();
        }

        public override void Exit(HigherLv entity)
        {
            Debug.Log("���̻� ���� �ʴ´�.");
        }
    }

    public class Patrol : State<HigherLv>
    {
        public override void Enter(HigherLv entity)
        {
            Debug.Log("���� �����̴�.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(HigherLv entity)
        {
            Debug.Log("��� �������̴�.");
        }

        public override void Exit(HigherLv entity)
        {
            Debug.Log("���̻� ���� ���°� �ƴϴ�.");
        }
    }
}