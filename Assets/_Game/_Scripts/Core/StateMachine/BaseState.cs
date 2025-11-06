namespace Game.Core.StateMachine
{
    public abstract class BaseState : IState
    {
        public abstract IState[] CompatibleNextStates { get; }
        
        public abstract void Enter(IStateMachine stateMachine);
        public virtual void Tick(IStateMachine stateMachine) { }
        public virtual void Exit(IStateMachine stateMachine) { }
    }
}
