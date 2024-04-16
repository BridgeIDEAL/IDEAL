using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTypeStates
{
    public class Indifference : State<CType>
    {
        public override void Enter(CType entity) {entity.IndifferenceEnter(); }
        public override void Execute(CType entity) { entity.IndifferenceExecute(); }
        public override void Exit(CType entity) { entity.IndifferenceExit(); }
    }
    public class Watch : State<CType>
    {
        public override void Enter(CType entity){ entity.WatchEnter(); }
        public override void Execute(CType entity){ entity.WatchExecute(); }
        public override void Exit(CType entity){ entity.WatchExit(); }
    }

    public class Interaction : State<CType>
    {
        public override void Enter(CType entity) { entity.InteractionEnter(); }
        public override void Execute(CType entity) { entity.InteractionExecute(); }
        public override void Exit(CType entity) { entity.InteractionExit(); }
    }
    public class Speechless : State<CType>
    {
        public override void Enter(CType entity) { entity.SpeechlessEnter(); }
        public override void Execute(CType entity) { entity.SpeechlessExecute(); }
        public override void Exit(CType entity){ entity.SpeechlessExit(); }
    }

    public class Penalty : State<CType>{
        public override void Enter(CType entity) { entity.PenaltyEnter(); }
        public override void Execute(CType entity) { entity.PenaltyExecute(); }
        public override void Exit(CType entity) { entity.PenaltyExit(); }
    }
}