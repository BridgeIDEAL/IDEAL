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

        public override void Execute(AType entity){ }

        public override void Exit(AType entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
    public class Interaction : State<AType>
    {
        public override void Enter(AType entity)
        {
            Debug.Log("��ȣ�ۿ��� �ϰ� �ֽ��ϴ�.");
        }

        public override void Execute(AType entity){  }

        public override void Exit(AType entity)
        {
            Debug.Log("��ȣ�ۿ��� ���½��ϴ�.");
        }
    }
}

