using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTypeStates
{
    public class Indifference : State<CType>
    {
        public override void Enter(CType entity) { entity.LookOriginal(); entity.SetAnimation(entity.CurrentType); }
        public override void Execute(CType entity) { entity.CheckNearPlayer(); entity.IndifferenceExecute(); }
        public override void Exit(CType entity){ }
    }
    public class Watch : State<CType>
    {
        public override void Enter(CType entity){ entity.SetAnimation(entity.CurrentType); entity.StartTimer(); entity.WatchEnter(); }
        public override void Execute(CType entity){entity.LookPlayer(); entity.MaintainWatch(); }
        public override void Exit(CType entity){ entity.EndTimer(); }
    }

    public class Interaction : State<CType>
    {
        public override void Enter(CType entity) { entity.SetAnimation(entity.CurrentType); entity.LookPlayer(); }
        public override void Execute(CType entity) { }
        public override void Exit(CType entity) { }
    }
    public class Speechless : State<CType>
    {
        public override void Enter(CType entity){ entity.LookOriginal(); entity.SetAnimation(entity.CurrentType); }
        public override void Execute(CType entity) { entity.IndifferenceExecute(); }
        public override void Exit(CType entity){ }
    }
}