public class EntityStates { }

namespace NonChaseEntitySpace
{
    public class IdleState : State<NonChaseEntity>
    {
        public override void Enter(NonChaseEntity _entity) { _entity.IdleEnter(); }
        public override void Execute(NonChaseEntity _entity) { _entity.IdleExecute(); }
        public override void Exit(NonChaseEntity _entity) { _entity.IdleExit(); }
    }
    public class TalkState : State<NonChaseEntity>
    {
        public override void Enter(NonChaseEntity _entity) { _entity.TalkEnter(); }
        public override void Execute(NonChaseEntity _entity) { _entity.TalkExecute(); }
        public override void Exit(NonChaseEntity _entity) { _entity.TalkExit(); }
    }
    public class QuietState : State<NonChaseEntity>
    {
        public override void Enter(NonChaseEntity _entity) { _entity.QuietEnter(); }
        public override void Execute(NonChaseEntity _entity) { _entity.QuietExecute(); }
        public override void Exit(NonChaseEntity _entity) { _entity.QuietExit(); }
    }
    public class PenaltyState : State<NonChaseEntity>
    {
        public override void Enter(NonChaseEntity _entity) { _entity.PenaltyEnter(); }
        public override void Execute(NonChaseEntity _entity) { _entity.PenaltyExecute(); }
        public override void Exit(NonChaseEntity _entity) { _entity.PenaltyExit(); }
    }
    public class ExtraState : State<NonChaseEntity>
    {
        public override void Enter(NonChaseEntity _entity) { _entity.ExtraEnter(); }
        public override void Execute(NonChaseEntity _entity) { _entity.ExtraExecute(); }
        public override void Exit(NonChaseEntity _entity) { _entity.ExtraExit(); }
    }
}

namespace ChaseEntitySpace
{
    public class IdleState : State<ChaseEntity>
    {
        public override void Enter(ChaseEntity _entity) { _entity.IdleEnter(); }
        public override void Execute(ChaseEntity _entity) { _entity.IdleExecute(); }
        public override void Exit(ChaseEntity _entity) { _entity.IdleExit(); }
    }
    public class TalkState : State<ChaseEntity>
    {
        public override void Enter(ChaseEntity _entity) { _entity.TalkEnter(); }
        public override void Execute(ChaseEntity _entity) { _entity.TalkExecute(); }
        public override void Exit(ChaseEntity _entity) { _entity.TalkExit(); }
    }
    public class QuietState : State<ChaseEntity>
    {
        public override void Enter(ChaseEntity _entity) { _entity.QuietEnter(); }
        public override void Execute(ChaseEntity _entity) { _entity.QuietExecute(); }
        public override void Exit(ChaseEntity _entity) { _entity.QuietExit(); }
    }
    public class PenaltyState : State<ChaseEntity>
    {
        public override void Enter(ChaseEntity _entity) { _entity.PenaltyEnter(); }
        public override void Execute(ChaseEntity _entity) { _entity.PenaltyExecute(); }
        public override void Exit(ChaseEntity _entity) { _entity.PenaltyExit(); }
    }
    public class ChaseState : State<ChaseEntity>
    {
        public override void Enter(ChaseEntity _entity) { _entity.ChaseEnter(); }
        public override void Execute(ChaseEntity _entity) { _entity.ChaseExecute(); }
        public override void Exit(ChaseEntity _entity) { _entity.ChaseExit(); }
    }
    public class ExtraState : State<ChaseEntity>
    {
        public override void Enter(ChaseEntity _entity) { _entity.ExtraEnter(); }
        public override void Execute(ChaseEntity _entity) { _entity.ExtraExecute(); }
        public override void Exit(ChaseEntity _entity) { _entity.ExtraExit(); }
    }
}
