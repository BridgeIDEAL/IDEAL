using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATypeStates
{
    public class Indifference : State<AType>
    {
        public override void Enter(AType entity)
        {
            Debug.Log("무관심 상태이다.");
        }

        public override void Execute(AType entity){ }

        public override void Exit(AType entity)
        {
            Debug.Log("무관심 상태가 아니다.");
        }
    }
    public class Interaction : State<AType>
    {
        public override void Enter(AType entity)
        {
            Debug.Log("상호작용을 하고 있습니다.");
        }

        public override void Execute(AType entity){  }

        public override void Exit(AType entity)
        {
            Debug.Log("상호작용을 끝냈습니다.");
        }
    }
}

