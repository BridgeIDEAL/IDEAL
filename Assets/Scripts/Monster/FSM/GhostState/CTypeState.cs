using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTypeStates
{
    public class Indifference : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("������ �����̴�.");
        }

        public override void Execute(CType entity){
            if (!entity.CanInteraction) { return; }
            if (entity.DetectPlayer()) { entity.ChangeState(CTypeEntityStates.Watch); } 
        }

        public override void Exit(CType entity)
        {
            Debug.Log("������ ���°� �ƴϴ�.");
        }
    }
    public class Watch : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("���� �����̴�.");
            entity.StartTimer();
        }

        public override void Execute(CType entity)
        {
            if (!entity.CanInteraction) { entity.ChangeState(CTypeEntityStates.Indifference); return; }
            entity.WatchPlayer();
        }

        public override void Exit(CType entity)
        {
            Debug.Log("���� ���°� �ƴϴ�.");
        } 
    }

    public class Interaction : State<CType>
    {
        public override void Enter(CType entity)
        {
            Debug.Log("��ȣ�ۿ��� �����մϴ�.");
        }

        public override void Execute(CType entity)
        {
        }

        public override void Exit(CType entity)
        {
            Debug.Log("��ȣ�ۿ��� �����մϴ�.");
        }
    }
}