public class EntityStates { }

namespace ImmovableEntityStates
{
    public class IdleState : EntityState<ImmovableEntity>
    {
        public override void Enter(ImmovableEntity _entity) { _entity.IdleEnter(); }
        public override void Execute(ImmovableEntity _entity) { _entity.IdleExecute(); }
        public override void Exit(ImmovableEntity _entity) { _entity.IdleExit(); }
    }
    public class TalkState : EntityState<ImmovableEntity>
    {
        public override void Enter(ImmovableEntity _entity) { _entity.TalkEnter(); }
        public override void Execute(ImmovableEntity _entity) { _entity.TalkExecute(); }
        public override void Exit(ImmovableEntity _entity) { _entity.TalkExit(); }
    }
    public class QuietState : EntityState<ImmovableEntity>
    {
        public override void Enter(ImmovableEntity _entity) { _entity.QuietEnter(); }
        public override void Execute(ImmovableEntity _entity) { _entity.QuietExecute(); }
        public override void Exit(ImmovableEntity _entity) { _entity.QuietExit(); }
    }
    public class PenaltyState : EntityState<ImmovableEntity>
    {
        public override void Enter(ImmovableEntity _entity) { _entity.PenaltyEnter(); }
        public override void Execute(ImmovableEntity _entity) { _entity.PenaltyExecute(); }
        public override void Exit(ImmovableEntity _entity) { _entity.PenaltyExit(); }
    }
}

namespace MovableEntityStates
{
    public class IdleState : EntityState<MovableEntity>
    {
        public override void Enter(MovableEntity _entity) { _entity.IdleEnter(); }
        public override void Execute(MovableEntity _entity) { _entity.IdleExecute(); }
        public override void Exit(MovableEntity _entity) { _entity.IdleExit(); }
    }
    public class TalkState : EntityState<MovableEntity>
    {
        public override void Enter(MovableEntity _entity) { _entity.TalkEnter(); }
        public override void Execute(MovableEntity _entity) { _entity.TalkExecute(); }
        public override void Exit(MovableEntity _entity) { _entity.TalkExit(); }
    }
    public class QuietState : EntityState<MovableEntity>
    {
        public override void Enter(MovableEntity _entity) { _entity.QuietEnter(); }
        public override void Execute(MovableEntity _entity) { _entity.QuietExecute(); }
        public override void Exit(MovableEntity _entity) { _entity.QuietExit(); }
    }
    public class PenaltyState : EntityState<MovableEntity>
    {
        public override void Enter(MovableEntity _entity) { _entity.PenaltyEnter(); }
        public override void Execute(MovableEntity _entity) { _entity.PenaltyExecute(); }
        public override void Exit(MovableEntity _entity) { _entity.PenaltyExit(); }
    }
    public class ChaseState : EntityState<MovableEntity>
    {
        public override void Enter(MovableEntity _entity) { _entity.ChaseEnter(); }
        public override void Execute(MovableEntity _entity) { _entity.ChaseExecute(); }
        public override void Exit(MovableEntity _entity) { _entity.ChaseExit(); }
    }
}
