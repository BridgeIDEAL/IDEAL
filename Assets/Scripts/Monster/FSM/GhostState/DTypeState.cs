using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTypeStates
{
    public class Indifference : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(DType entity)
        {
            if (!entity.CanInteraction) { return; }
            if (entity.DetectPlayer()) { entity.ChangeState(DTypeEntityStates.Watch); }
        }

        public override void Exit(DType entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
    public class Watch : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("���� �����̴�.");
        }

        public override void Execute(DType entity)
        {
            if (!entity.CanInteraction) { entity.ChangeState(DTypeEntityStates.Indifference); return; }
            entity.WatchPlayer();
        }

        public override void Exit(DType entity)
        {
            Debug.Log("���� ���°� �ƴϴ�.");
        }
    }
    public class Interaction : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("��ȣ�ۿ� �����̴�.");
        }

        public override void Execute(DType entity)
        {
  
        }

        public override void Exit(DType entity)
        {
            Debug.Log("��ȣ�ۿ� ���°� �ƴϴ١�");
        }
    }
    public class Aggressive : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("���� ��� �����̴�.");
        }

        public override void Execute(DType entity)
        {
            if (!entity.CanInteraction) { entity.ChangeState(DTypeEntityStates.Indifference); }
        }

        public override void Exit(DType entity)
        {
            Debug.Log("���� ���°� �ȴ١�");
        }
    }
    public class Chase : State<DType>
    {
        public override void Enter(DType entity)
        {
            Debug.Log("�Ѵ� �����̴�.");
        }

        public override void Execute(DType entity)
        {
            entity.ChasePlayer();
        }

        public override void Exit(DType entity)
        {
            Debug.Log("���̻� ���� �ʴ´�.");
        }
    }
}