using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATypeStates
{
    public class Indifference : State<AType>
    {
        public override void Enter(AType entity){entity.SetAnimation(entity.CurrentType); }
        public override void Execute(AType entity){ entity.LookOriginal(); }
        public override void Exit(AType entity){ entity.isLookOrigin = true; }
    }
    public class Interaction : State<AType>
    {
        public override void Enter(AType entity){entity.SetAnimation(entity.CurrentType); }
        public override void Execute(AType entity){ entity.LookPlayer(); }
        public override void Exit(AType entity) { entity.isLookPlayer = true;  }
    }
    public class Speechless : State<AType>
    {
        public override void Enter(AType entity) { entity.SetAnimation(entity.CurrentType); }
        public override void Execute(AType entity) { entity.LookOriginal(); }
        public override void Exit(AType entity) { entity.isLookOrigin = true; }
    }
}

