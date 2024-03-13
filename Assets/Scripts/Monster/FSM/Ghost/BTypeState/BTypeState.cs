using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTypeStates
{
    public class Indifference : State<BType>
    {
        public override void Enter(BType entity) { entity.IndifferenceEnter(); }
        public override void Execute(BType entity){ entity.IndifferenceExecute(); }
        public override void Exit(BType entity) { entity.IndifferenceExit(); }
    }
    public class Interaction : State<BType>
    {
        public override void Enter(BType entity) { entity.InteractionEnter(); }
        public override void Execute(BType entity) { entity.InteractionExecute(); }
        public override void Exit(BType entity) { entity.InteractionExit(); }
    }

    public class Aggressive : State<BType>
    {
        public override void Enter(BType entity) { entity.AggressiveEnter(); }
        public override void Execute(BType entity){ entity.AggressiveExecute(); }
        public override void Exit(BType entity) { entity.AggressiveExit(); }
    }

    public class Chase : State<BType>
    {
        public override void Enter(BType entity) { entity.ChaseEnter(); }
        public override void Execute(BType entity) { entity.ChaseExecute(); }
        public override void Exit(BType entity) { entity.ChaseExit(); }
    }
    public class Speechless : State<BType>
    {
        public override void Enter(BType entity) { entity.SpeechlessEnter(); }
        public override void Execute(BType entity) { entity.SpeechlessExecute(); } 
        public override void Exit(BType entity) { entity.SpeechlessExit(); }
    }
}

