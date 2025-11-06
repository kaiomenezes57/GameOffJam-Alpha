namespace Game.Core.StateMachine
{
    public interface IState
    {
        IState[] InvalidNextStates { get; }
        void Enter(IStateMachine stateMachine);
        void Tick(IStateMachine stateMachine);
        void Exit(IStateMachine stateMachine);
    }
}
