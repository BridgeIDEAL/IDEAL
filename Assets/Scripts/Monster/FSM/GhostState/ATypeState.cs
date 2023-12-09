using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATypeStates
{
    public class Indifference : State<AType>
    {
        public override void Enter(AType entity){entity.SetAnimation(entity.CurrentType);}
        public override void Execute(AType entity){ }
        public override void Exit(AType entity){ }
    }
    public class Interaction : State<AType>
    {
        public override void Enter(AType entity){entity.SetAnimation(entity.CurrentType); }
        public override void Execute(AType entity){  }
        public override void Exit(AType entity) { }
    }
}

