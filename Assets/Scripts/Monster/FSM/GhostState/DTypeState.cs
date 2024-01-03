using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTypeStates
{
    public class Indifference : State<DType>
    {
        public override void Enter(DType entity){ entity.LookOriginal(); entity.SetAnimation(entity.CurrentType);}
        public override void Execute(DType entity) { entity.CheckNearPlayer(); }
        public override void Exit(DType entity){ }
    }
    public class Watch : State<DType>
    {
        public override void Enter(DType entity){entity.SetAnimation(entity.CurrentType); entity.StartTimer(); }
        public override void Execute(DType entity){ entity.LookPlayer(); entity.MaintainWatch(); }
        public override void Exit(DType entity){ entity.EndTimer(); }
    }
    public class Interaction : State<DType>
    {
        public override void Enter(DType entity){entity.SetAnimation(entity.CurrentType); entity.LookPlayer(); }
        public override void Execute(DType entity){ }
        public override void Exit(DType entity){ }
    }
    public class Aggressive : State<DType>
    {
        public override void Enter(DType entity) {entity.SetAnimation(entity.CurrentType);}
        public override void Execute(DType entity){ }
        public override void Exit(DType entity){ }
    }
    public class Chase : State<DType>
    {
        public override void Enter(DType entity){entity.SetAnimation(entity.CurrentType);}
        public override void Execute(DType entity){ entity.ChasePlayer();}
        public override void Exit(DType entity){ entity.ResetPosition(); }
    }
    public class Speechless : State<DType>
    {
        public override void Enter(DType entity) { entity.LookOriginal(); entity.SetAnimation(entity.CurrentType); }
        public override void Execute(DType entity) { }
        public override void Exit(DType entity) { }
    }
}