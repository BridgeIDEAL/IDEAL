using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTypeStates
{
    public class Indifference : State<CType>
    {
        public override void Enter(CType entity) {  entity.SetAnimation(entity.CurrentType); }
        public override void Execute(CType entity) { entity.DetectPlayer(); entity.IndifferenceExecute(); }
        public override void Exit(CType entity){ entity.isLookOrigin = true; }
    }
    public class Watch : State<CType>
    {
        public override void Enter(CType entity){ entity.SetAnimation(entity.CurrentType); entity.StartTimer(); entity.WatchEnter(); }
        public override void Execute(CType entity){entity.LookPlayer(); entity.WatchExecute(); }
        public override void Exit(CType entity){ entity.EndTimer(); entity.isLookPlayer = true; }
    }

    public class Interaction : State<CType>
    {
        public override void Enter(CType entity) { entity.SetAnimation(entity.CurrentType);  }
        public override void Execute(CType entity) { entity.LookPlayer(); }
        public override void Exit(CType entity) { entity.isLookPlayer = true; }
    }
    public class Speechless : State<CType>
    {
        public override void Enter(CType entity) { entity.SetAnimation(entity.CurrentType); }
        public override void Execute(CType entity) { entity.IndifferenceExecute(); }
        public override void Exit(CType entity){ }
    }
}