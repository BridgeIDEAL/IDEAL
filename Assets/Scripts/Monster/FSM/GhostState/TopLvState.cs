using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopLevelStates
{
    public class Indifference : State<TopLv>
    {
        public override void Enter(TopLv entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(TopLv entity)
        {
            Debug.Log("��� �������ϴ�.");
            if (entity.DetectPlayer())
                entity.ChangeState(EntityStates.Watch);
        }

        public override void Exit(TopLv entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
    public class Watch : State<TopLv>
    {
        public override void Enter(TopLv entity)
        {
            Debug.Log("���� �����̴�.");
            entity.transform.LookAt(entity.playerObject.transform);
        }

        public override void Execute(TopLv entity)
        {
            Debug.Log("��� �����Ѵ�.");
            if (!entity.CheckDistance())
                entity.ChangeState(EntityStates.Indifference);
        }

        public override void Exit(TopLv entity)
        {
            Debug.Log("���� ���°� �ƴϴ�.");
        }
    }

    public class Chase : State<TopLv>
    {
        public override void Enter(TopLv entity)
        {
            Debug.Log("�Ѵ� �����̴�.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(TopLv entity)
        {
            Debug.Log("��� �Ѵ����̴�.");
            entity.ChasePlayer();
        }

        public override void Exit(TopLv entity)
        {
            Debug.Log("���̻� ���� �ʴ´�.");
        }
    }

    public class Patrol : State<TopLv>
    {
        public override void Enter(TopLv entity)
        {
            Debug.Log("���� �����̴�.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(TopLv entity)
        {
            Debug.Log("��� �������̴�.");
        }

        public override void Exit(TopLv entity)
        {
            Debug.Log("���̻� ���� ���°� �ƴϴ�.");
        }
    }
}