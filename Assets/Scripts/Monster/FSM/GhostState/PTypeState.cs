using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PTypeStates
{
    public class Indifference : State<PType>
    {
        public override void Enter(PType entity) { }
        public override void Execute(PType entity) { }
        public override void Exit(PType entity) { }
    }
    public class Watch : State<PType>
    {
        public override void Enter(PType entity) { }
        public override void Execute(PType entity) { }
        public override void Exit(PType entity) { }
    }
    public class Interaction : State<PType>
    {
        public override void Enter(PType entity) { }
        public override void Execute(PType entity) { }
        public override void Exit(PType entity) { }
    }
    public class Speechless : State<PType>
    {
        public override void Enter(PType entity) { }
        public override void Execute(PType entity) { }
        public override void Exit(PType entity) { }
    }
}