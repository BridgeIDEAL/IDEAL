using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTypeStates
{
    public class Indifference : State<BType>
    {
        public override void Enter(BType entity) {entity.SetAnimation(entity.CurrentType);}
        public override void Execute(BType entity){ }
        public override void Exit(BType entity) { }
    }
    public class Interaction : State<BType>
    {
        public override void Enter(BType entity) { entity.SetAnimation(entity.CurrentType);}
        public override void Execute(BType entity) {  }
        public override void Exit(BType entity) { }
    }

    public class Aggressive : State<BType>
    {
        public override void Enter(BType entity) {entity.SetAnimation(entity.CurrentType); }
        public override void Execute(BType entity){  }
        public override void Exit(BType entity) { }
    }

    public class Chase : State<BType>
    {
        public override void Enter(BType entity) {entity.SetAnimation(entity.CurrentType);}
        public override void Execute(BType entity) { entity.ChasePlayer(); }
        public override void Exit(BType entity){ }
    }
    public class Speechless : State<BType>
    {
        public override void Enter(BType entity) { entity.SetAnimation(entity.CurrentType); }
        public override void Execute(BType entity) { }
        public override void Exit(BType entity) { }
    }
}

