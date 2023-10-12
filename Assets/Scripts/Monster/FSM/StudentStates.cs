using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudentState
{
    public class Indifference : State<Student>
    {
        public override void Enter(Student entity)
        {
            Debug.Log("무관심 상태이다.");
        }

        public override void Execute(Student entity)
        {
            Debug.Log("계속 무관심하다.");
        }

        public override void Exit(Student entity)
        {
            Debug.Log("무관심 상태가 아니다.");
        }
    }
    public class Watch : State<Student>
    {
        public override void Enter(Student entity)
        {
            Debug.Log("무관심 상태이다.");
        }

        public override void Execute(Student entity)
        {
            Debug.Log("계속 무관심하다.");
        }

        public override void Exit(Student entity)
        {
            Debug.Log("무관심 상태가 아니다.");
        }
    }

    public class Chase : State<Student>
    {
        public override void Enter(Student entity)
        {
            Debug.Log("무관심 상태이다.");
        }

        public override void Execute(Student entity)
        {
            Debug.Log("계속 무관심하다.");
        }

        public override void Exit(Student entity)
        {
            Debug.Log("무관심 상태가 아니다.");
        }
    }

   
    public class Patrol : State<Student>
    {
        public override void Enter(Student entity)
        {
            Debug.Log("무관심 상태이다.");
        }

        public override void Execute(Student entity)
        {
            Debug.Log("계속 무관심하다.");
        }

        public override void Exit(Student entity)
        {
            Debug.Log("무관심 상태가 아니다.");
        }
    }
}

