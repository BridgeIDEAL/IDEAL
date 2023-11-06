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

        public override void Execute(BType entity)
        {
        }

        public override void Exit(BType entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }

        public override bool OnMessage(BType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }
    public class Interaction : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("��ȣ�ۿ� �����̴�.");
            entity.Speed = entity.patrolSpeed;
        }

        public override void Execute(BType entity) { }

        public override void Exit(BType entity)
        {
            Debug.Log("��ȣ�ۿ��� �����Ѵ�.");
        }
        public override bool OnMessage(BType entity, bool interaction)
        {
            if (interaction)
            {
                Debug.Log("��ȣ�ۿ� ����!");
                entity.ChangeState(BTypeEntityStates.Indifference);
            }
            else
            {
                Debug.Log("��ȣ�ۿ� ����!");
                entity.ChangeState(BTypeEntityStates.Aggressive);
            }
            return interaction;
        }
    }

    public class Aggressive : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("���� �����");
        }

        public override void Execute(BType entity){ }

        public override void Exit(BType entity)
        {
            Debug.Log("�߰��� �ϱ� ���� �غ� �Ϸ�� �����̴�.");
        }
        public override bool OnMessage(BType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Chase : State<BType>
    {
        public override void Enter(BType entity)
        {
            Debug.Log("�Ѵ� �����̴�.");
            entity.Speed = entity.chaseSpeed;
        }

        public override void Execute(BType entity)
        {
            entity.ChasePlayer();
        }

        public override void Exit(BType entity)
        {
            Debug.Log("���̻� ���� �ʴ´�.");
        }
        public override bool OnMessage(BType entity, bool interaction)
        {
            throw new System.NotImplementedException();
        }
    }
}

