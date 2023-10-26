using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowerLevelStates
{
    public class Indifference : State<LowerLv>
    {
        public override void Enter(LowerLv entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(LowerLv entity)
        {
            Debug.Log("��� �������ϴ�.");
            if (entity.DetectPlayer())
                entity.ChangeState(EntityStates.Watch);
        }

        public override void Exit(LowerLv entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
    public class Watch : State<LowerLv>
    {
        public override void Enter(LowerLv entity)
        {
            Debug.Log("���� �����̴�.");
            entity.transform.LookAt(entity.playerObject.transform);
        }

        public override void Execute(LowerLv entity)
        {
            entity.WatchPlayer();
            Debug.Log("��� �����Ѵ�.");
            if (!entity.CheckDistance())
                entity.ChangeState(EntityStates.Chase);
        }

        public override void Exit(LowerLv entity)
        {
            Debug.Log("���� ���°� �ƴϴ�.");
            entity.ExitWatchPlayer();
        }
    }

    public class Chase : State<LowerLv>
    {
        public override void Enter(LowerLv entity)
        {
            Debug.Log("�Ѵ� �����̴�.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(LowerLv entity)
        {
            Debug.Log("��� �Ѵ����̴�.");
            entity.ChasePlayer();
        }

        public override void Exit(LowerLv entity)
        {
            Debug.Log("���̻� ���� �ʴ´�.");
        }
    }

    public class Patrol : State<LowerLv>
    {
        public override void Enter(LowerLv entity)
        {
            Debug.Log("���� �����̴�.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(LowerLv entity)
        {
            Debug.Log("��� �������̴�.");
        }

        public override void Exit(LowerLv entity)
        {
            Debug.Log("���̻� ���� ���°� �ƴϴ�.");
        }
    }
}

