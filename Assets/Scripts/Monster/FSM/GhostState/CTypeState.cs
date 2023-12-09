using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTypeStates
{
    public class Indifference : State<CType>
    {
        public override void Enter(CType entity) {entity.SetAnimation(entity.CurrentType); }
        public override void Execute(CType entity) { }
        public override void Exit(CType entity){ }
    }
    public class Watch : State<CType>
    {
        public override void Enter(CType entity){ entity.SetAnimation(entity.CurrentType);}
        public override void Execute(CType entity){entity.WatchPlayer();}
        public override void Exit(CType entity){ }
    }

    public class Interaction : State<CType>
    {
        public override void Enter(CType entity){entity.SetAnimation(entity.CurrentType); }
        public override void Execute(CType entity){ }
        public override void Exit(CType entity){ }
    }
}