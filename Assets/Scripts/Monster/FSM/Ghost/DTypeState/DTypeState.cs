using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTypeStates
{
    public class Indifference : State<DType>
    {
        public override void Enter(DType entity){ entity.IndifferenceEnter(); }
        public override void Execute(DType entity) { entity.IndifferenceExecute(); }
        public override void Exit(DType entity){ entity.IndifferenceExit(); }
    }
    public class Watch : State<DType>
    {
        public override void Enter(DType entity){ entity.WatchEnter(); }
        public override void Execute(DType entity){ entity.WatchExecute(); }
        public override void Exit(DType entity){ entity.WatchExit(); }
    }
    public class Interaction : State<DType>
    {
        public override void Enter(DType entity){ entity.InteractionEnter(); }
        public override void Execute(DType entity){ entity.InteractionExecute(); }
        public override void Exit(DType entity){ entity.InteractionExit(); }
    }
    public class Aggressive : State<DType>
    {
        public override void Enter(DType entity) { entity.AggressiveEnter(); }
        public override void Execute(DType entity){ entity.AggressiveExit(); }
        public override void Exit(DType entity){ entity.AggressiveExit(); }
    }
    public class Chase : State<DType>
    {
        public override void Enter(DType entity){ entity.ChaseEnter(); }
        public override void Execute(DType entity){ entity.ChaseExecute(); }
        public override void Exit(DType entity){ entity.ChaseExit(); }
    }
    public class Speechless : State<DType>
    {
        public override void Enter(DType entity) { entity.SpeechlessEnter(); }
        public override void Execute(DType entity) { entity.SpeechlessExecute(); }
        public override void Exit(DType entity) { entity.SpeechlessExit(); }
    }
}