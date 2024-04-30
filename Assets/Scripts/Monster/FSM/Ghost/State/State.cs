public abstract class State<T> where T : BaseEntity
{
    public abstract void Enter(T _entity); 
    public abstract void Execute(T _entity); 
    public abstract void Exit(T _entity); 
}
