using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudentState
{
    public class Indifference : State<Student>
    {
        public override void Enter(Student entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(Student entity)
        {
            Debug.Log("��� �������ϴ�.");
            if (entity.DetectPlayer())
                entity.ChangeState(EntityStates.Watch);
        }

        public override void Exit(Student entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
    public class Watch : State<Student>
    {
        public override void Enter(Student entity)
        {
            Debug.Log("���� �����̴�.");
        }

        public override void Execute(Student entity)
        {
            Debug.Log("��� �����Ѵ�.");
            if(!entity.DetectPlayer())
                entity.ChangeState(EntityStates.Chase);
        }

        public override void Exit(Student entity)
        {
            Debug.Log("���� ���°� �ƴϴ�.");
        }
    }

    public class Chase : State<Student>
    {
        public override void Enter(Student entity)
        {
            Debug.Log("�Ѵ� �����̴�.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(Student entity)
        {
            Debug.Log("��� �Ѵ����̴�.");
            entity.ChasePlayer();
        }

        public override void Exit(Student entity)
        {
            Debug.Log("���̻� ���� �ʴ´�.");
        }
    }

   
    public class Patrol : State<Student>
    {
        public override void Enter(Student entity)
        {
            Debug.Log("���� �����̴�.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(Student entity)
        {
            Debug.Log("��� �������̴�.");
        }

        public override void Exit(Student entity)
        {
            Debug.Log("���̻� ���� ���°� �ƴϴ�.");
        }
    }
}

