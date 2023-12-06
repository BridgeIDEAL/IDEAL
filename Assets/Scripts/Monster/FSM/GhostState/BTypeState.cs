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

        public override void Execute(BType entity){ }

        public override void Exit(BType entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
    public class Interaction : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("��ȣ�ۿ� �����̴�.");
        }

        public override void Execute(BType entity) {  }

        public override void Exit(BType entity)
        {
            Debug.Log("��ȣ�ۿ��� �����Ѵ�.");
        }
    }

    public class Aggressive : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("���� �����");
        }

        public override void Execute(BType entity){  }

        public override void Exit(BType entity)
        {
            Debug.Log("�߰��� �ϱ� ���� �غ� �Ϸ�� �����̴�.");
        }
    }

    public class Chase : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("�Ѵ� �����̴�.");
        }

        public override void Execute(BType entity) { entity.ChasePlayer(); }

        public override void Exit(BType entity)
        {
            Debug.Log("���̻� ���� �ʴ´�.");
        }
    }
}

