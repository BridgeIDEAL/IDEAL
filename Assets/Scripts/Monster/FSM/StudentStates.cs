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
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(Student entity)
        {
            Debug.Log("��� �������ϴ�.");
        }

        public override void Exit(Student entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }

    public class Chase : State<Student>
    {
        public override void Enter(Student entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(Student entity)
        {
            Debug.Log("��� �������ϴ�.");
        }

        public override void Exit(Student entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }

   
    public class Patrol : State<Student>
    {
        public override void Enter(Student entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(Student entity)
        {
            Debug.Log("��� �������ϴ�.");
        }

        public override void Exit(Student entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
}

