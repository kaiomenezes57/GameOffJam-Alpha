namespace Game.Core.StateMachine
{
    public abstract class BaseState : IState
    {
        public virtual IState[] InvalidNextStates { get; }
        public IState NextState { get; protected set; }

        public virtual void Enter(IStateMachine stateMachine) { }
        public virtual void Tick(IStateMachine stateMachine, float deltaTime) { }
        public virtual void Exit(IStateMachine stateMachine) { }
    }
}
