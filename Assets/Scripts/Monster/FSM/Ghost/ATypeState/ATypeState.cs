using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATypeStates
{
    public class Indifference : State<AType>
    {
        public override void Enter(AType entity){ entity.IndifferenceEnter(); }
        public override void Execute(AType entity){ entity.IndifferenceExecute(); }
        public override void Exit(AType entity){ entity.IndifferenceExit(); }
    }
    public class Interaction : State<AType>
    {
        public override void Enter(AType entity){ entity.InteractionEnter(); }
        public override void Execute(AType entity){ entity.InteractionExecute(); }
        public override void Exit(AType entity){ entity.InteractionExit(); }
    }
    public class Speechless : State<AType>
    {
        public override void Enter(AType entity) { entity.SpeechlessEnter(); }
        public override void Execute(AType entity) { entity.SpeechlessExecute(); }
        public override void Exit(AType entity) { entity.SpeechlessExit(); }
    }
}

