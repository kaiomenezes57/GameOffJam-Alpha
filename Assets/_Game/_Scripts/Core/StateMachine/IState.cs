namespace Game.Core.StateMachine
{
    public interface IState
    {
        IState[] CompatibleNextStates { get; }
        void Enter(IStateMachine stateMachine);
        void Tick(IStateMachine stateMachine);
        void Exit(IStateMachine stateMachine);
    }
}
